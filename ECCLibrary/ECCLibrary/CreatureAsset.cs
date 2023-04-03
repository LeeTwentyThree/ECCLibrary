using UnityEngine;
using static CraftData;
using static LargeWorldEntity;
using static PlaceTool;
using static UWE.FreezeTime;

namespace ECCLibrary;

internal abstract class CreatureAsset
{
    protected abstract CreatureTemplate CreateTemplate();

    protected abstract IEnumerator ModifyPrefab(GameObject obj, CreatureComponents components);

    private CreatureTemplate template;

    private GameObject cached;

    public PrefabInfo PrefabInfo
    {
        get
        {
            if (template != null && template.PrefabInfo != null && !string.IsNullOrEmpty(template.PrefabInfo.ClassID)) return template.PrefabInfo;
            ECCPlugin.logger.LogError("Attempting to access PrefabInfo property before creature has been initialized!");
            return new PrefabInfo();
        }
    }

    public TechType TechType
    {
        get
        {
            if (template != null && template.PrefabInfo != null) return template.PrefabInfo.TechType;
            ECCPlugin.logger.LogError("Attempting to access TechType property before creature has been initialized!");
            return TechType.None;
        }
    }

    public void Register()
    {
        template = CreateTemplate();

        // check validity of essentials

        if (template.PrefabInfo == null || template.PrefabInfo.TechType == TechType.None)
        {
            ECCPlugin.logger.LogError("Creature initialized with invalid PrefabInfo!");
            return;
        }

        if (!SanityChecking.CanRegisterTechTypeSafely(template.PrefabInfo.TechType))
        {
            ECCPlugin.logger.LogError("Initializing multiple creatures with the same TechType!");
            return;
        }

        var prefabInfo = template.PrefabInfo;
        var techType = template.PrefabInfo.TechType;

        // assign patch-time data

        if (template.Sprite != null) prefabInfo.WithIcon(template.Sprite);
        if (template.AcidImmune) ECCUtility.MakeAcidImmune(techType);
        if (template.BioReactorCharge >= 0f) BaseBioReactor.charge.Add(techType, template.BioReactorCharge);
        ECCUtility.PatchBehaviorType(techType, template.BehaviourType);
        ECCUtility.PatchItemSounds(techType, template.ItemSoundsType);

        // register custom prefab

        CustomPrefab prefab = new CustomPrefab(prefabInfo);
        prefab.SetGameObject(GetGameObject);
        prefab.Register();

        PostRegister();
    }

    protected virtual void PostRegister() { }

    private IEnumerator GetGameObject(IOut<GameObject> gameObject)
    {
        if (cached != null)
        {
            gameObject.Set(cached);
            yield break;
        }

        var prefab = new GameObject(PrefabInfo.ClassID);
        prefab.SetActive(false);

        var components = AddComponents(prefab);

        yield return ModifyPrefab(prefab, components);

        ApplyMaterials(prefab);

        gameObject.Set(prefab);
        cached = prefab;
        yield break;
    }

    protected virtual void ApplyMaterials(GameObject prefab)
    {
        MaterialUtils.ApplySNShaders(prefab);
    }

    private CreatureComponents AddComponents(GameObject prefab)
    {
        CreatureComponents ccs = new();

        // essentials

        ccs.prefabIdentifier = prefab.EnsureComponent<PrefabIdentifier>();
        ccs.prefabIdentifier.ClassId = PrefabInfo.ClassID;

        ccs.techTag = prefab.EnsureComponent<TechTag>();
        ccs.techTag.type = TechType;

        ccs.largeWorldEntity = prefab.EnsureComponent<LargeWorldEntity>();
        ccs.largeWorldEntity.cellLevel = template.CellLevel;

        ccs.entityTag = prefab.EnsureComponent<EntityTag>();
        ccs.entityTag.slotType = EntitySlot.Type.Creature;

        ccs.skyApplier = prefab.AddComponent<SkyApplier>();
        ccs.skyApplier.renderers = prefab.GetComponentsInChildren<Renderer>(true);

        ccs.ecoTarget = prefab.AddComponent<EcoTarget>();
        ccs.ecoTarget.type = template.EcoTargetType;

        ccs.vfxSurface = prefab.EnsureComponent<VFXSurface>();
        ccs.vfxSurface.surfaceType = template.SurfaceType;

        ccs.behaviourLOD = prefab.EnsureComponent<BehaviourLOD>();
        ccs.behaviourLOD.veryCloseThreshold = template.BehaviourLODData.veryClose;
        ccs.behaviourLOD.closeThreshold = template.BehaviourLODData.close;
        ccs.behaviourLOD.farThreshold = template.BehaviourLODData.far;

        // physics

        ccs.rigidbody = prefab.EnsureComponent<Rigidbody>();
        ccs.rigidbody.useGravity = false;
        ccs.rigidbody.mass = template.Mass;

        var physicMaterial = template.PhysicMaterial;
        if (physicMaterial == null)
        {
            template.PhysicMaterial = ECCUtility.FrictionlessPhysicMaterial;
        }

        var colliders = prefab.GetComponentsInChildren<Collider>();
        foreach (var collider in colliders)
        {
            collider.sharedMaterial = physicMaterial;
        }

        ccs.worldForces = prefab.EnsureComponent<WorldForces>();
        ccs.worldForces.useRigidbody = ccs.rigidbody;
        ccs.worldForces.handleGravity = true;
        ccs.worldForces.underwaterGravity = 0;
        ccs.worldForces.aboveWaterGravity = 9.81f;
        ccs.worldForces.underwaterDrag = 0.1f;

        // behaviour

        var locomotionData = template.LocomotionData;
        if (locomotionData != null)
        {
            ccs.locomotion = CreaturePrefabUtils.AddLocomotion(prefab, template.LocomotionData, ccs.behaviourLOD, ccs.rigidbody);

            ccs.splineFollowing = CreaturePrefabUtils.AddSplineFollowing(prefab, ccs.rigidbody, ccs.locomotion, ccs.behaviourLOD);

            if (template.SwimBehaviourData != null)
            {
                ccs.swimBehaviour = CreaturePrefabUtils.AddSwimBehaviour(prefab, template.SwimBehaviourData, ccs.splineFollowing);
            }
        }

        if (template.SwimRandomData != null)
        {
            CreaturePrefabUtils.AddSwimRandom(prefab, template.SwimRandomData);
        }

        // livemixin

        ccs.liveMixin = prefab.EnsureComponent<LiveMixin>();
        ccs.liveMixin.data = template.LiveMixinData;
        ccs.liveMixin.health = template.LiveMixinData.maxHealth;

        ECCPlugin.logger.LogError("IMPLEMENT LIVEMIXIN VFX PLEASE!! SOON!??");

        // kharaa

        if (template.CanBeInfected)
        {
            ccs.infectedMixin = prefab.AddComponent<InfectedMixin>();
            ccs.infectedMixin.renderers = prefab.GetComponentsInChildren<Renderer>(true);
        }

        // main 'creature' component

        ccs.creature = prefab.AddComponent(template.CreatureComponentType) as Creature;
        ccs.creature.Aggression = new CreatureTrait(0f, template.TraitsData.AggressionDecreaseRate);
        ccs.creature.Hunger = new CreatureTrait(0f, -template.TraitsData.HungerIncreaseRate);
        ccs.creature.Scared = new CreatureTrait(0f, template.TraitsData.ScaredDecreaseRate);
        ccs.creature.liveMixin = ccs.liveMixin;
        ccs.creature.traitsAnimator = ccs.creature.GetComponentInChildren<Animator>();
        ccs.creature.sizeDistribution = template.SizeDistribution;
        ccs.creature.eyeFOV = template.EyeFOV;

        // death

        ccs.creatureDeath = prefab.AddComponent<CreatureDeath>();
        ccs.creatureDeath.useRigidbody = ccs.rigidbody;
        ccs.creatureDeath.liveMixin = ccs.liveMixin;
        // ccs.creatureDeath.eatable = eatable;
        // ccs.creatureDeath.respawnInterval = _respawnSettings.RespawnDelay;
        // ccs.creatureDeath.respawn = _respawnSettings.CanRespawn;

        return ccs;
    }
}