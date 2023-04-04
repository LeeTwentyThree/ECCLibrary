﻿using ECCLibrary.Data;
using ECCLibrary.Mono;
using SMLHelper.Handlers;

namespace ECCLibrary;

/// <summary>
/// Override this class to define a new creature. Call the <see cref="Register"/> method on an instance to add it to the game.
/// </summary>
public abstract class CreatureAsset
{
    private CreatureTemplate template;

    private GameObject cachedPrefab;

    /// <summary>
    /// Information for registering the prefab. MUST be unique to each creature.
    /// </summary>
    public PrefabInfo PrefabInfo { get; private set; }

    /// <summary>
    /// Creates a CreatureAsset with the given PrefabInfo.
    /// </summary>
    /// <param name="prefabInfo"></param>
    public CreatureAsset(PrefabInfo prefabInfo)
    {
        PrefabInfo = prefabInfo;
    }

    /// <summary>
    /// The TechType of this creature. MUST be unique to each creature.
    /// </summary>
    public TechType TechType
    {
        get
        {
            return PrefabInfo.TechType;
        }
    }

    /// <summary>
    /// Registers this creature into the game.
    /// </summary>
    public void Register()
    {
        template = CreateTemplate();

        // check validity of essentials

        if (PrefabInfo.TechType == TechType.None)
        {
            ECCPlugin.logger.LogError($"Attempting to register creature '{this}' without a valid TechType! Exiting early.");
            return;
        }

        if (!SanityChecking.CanRegisterTechTypeSafely(TechType))
        {
            ECCPlugin.logger.LogError("Initializing multiple creatures with the same TechType!");
            return;
        }

        // assign patch-time data

        if (template.AcidImmune) CreatureDataUtils.MakeAcidImmune(TechType);
        if (template.BioReactorCharge > 0f) CreatureDataUtils.SetBioreactorCharge(TechType, template.BioReactorCharge);
        if (template.PickupableFishData != null && template.PickupableFishData.CanBeHeld) CraftDataHandler.SetEquipmentType(TechType, EquipmentType.Hand);
        CreatureDataUtils.PatchBehaviorType(TechType, template.BehaviourType);
        CreatureDataUtils.PatchItemSounds(TechType, template.ItemSoundsType);

        // register custom prefab

        CustomPrefab prefab = new CustomPrefab(PrefabInfo);
        prefab.SetGameObject(GetGameObject);
        prefab.Register();

        PostRegister();
    }

    /// <summary>
    /// An empty method that can be overriden to insert code that runs directly after the prefab is registered.
    /// </summary>
    protected virtual void PostRegister() { }

    /// <summary>
    /// The majority of the data for each creature should be assigned through this call.
    /// </summary>
    /// <returns></returns>
    protected abstract CreatureTemplate CreateTemplate();

    /// <summary>
    /// Changes to the prefab can be applied here.
    /// </summary>
    /// <param name="prefab"></param>
    /// <param name="components"></param>
    /// <returns></returns>
    protected abstract IEnumerator ModifyPrefab(GameObject prefab, CreatureComponents components);

    private IEnumerator GetGameObject(IOut<GameObject> gameObject)
    {
        if (!ObjectReferences.Done)
        {
            yield return ObjectReferences.SetReferences();
        }

        if (cachedPrefab != null)
        {
            gameObject.Set(cachedPrefab);
            yield break;
        }

        var prefab = Object.Instantiate(template.Model);
        prefab.name = PrefabInfo.ClassID;
        prefab.SetActive(false);

        var components = AddComponents(prefab);

        yield return ModifyPrefab(prefab, components);

        ApplyMaterials(prefab);

        gameObject.Set(prefab);
        cachedPrefab = prefab;
        yield break;
    }

    /// <summary>
    /// By default calls <see cref="MaterialUtils.ApplySNShaders"/> to convert the materials of the entire prefab. Can be overriden to have more control over the process.
    /// </summary>
    /// <param name="prefab"></param>
    protected virtual void ApplyMaterials(GameObject prefab)
    {
        MaterialUtils.ApplySNShaders(prefab);
    }

    private CreatureComponents AddComponents(GameObject prefab)
    {
        CreatureComponents ccs = new();

        // essentials

        ccs.PrefabIdentifier = prefab.EnsureComponent<PrefabIdentifier>();
        ccs.PrefabIdentifier.ClassId = PrefabInfo.ClassID;

        ccs.TechTag = prefab.EnsureComponent<TechTag>();
        ccs.TechTag.type = TechType;

        ccs.LargeWorldEntity = prefab.EnsureComponent<LargeWorldEntity>();
        ccs.LargeWorldEntity.cellLevel = template.CellLevel;

        ccs.EntityTag = prefab.EnsureComponent<EntityTag>();
        ccs.EntityTag.slotType = EntitySlot.Type.Creature;

        ccs.SkyApplier = prefab.AddComponent<SkyApplier>();
        ccs.SkyApplier.renderers = prefab.GetComponentsInChildren<Renderer>(true);

        ccs.EcoTarget = prefab.AddComponent<EcoTarget>();
        ccs.EcoTarget.type = template.EcoTargetType;

        ccs.VfxSurface = prefab.EnsureComponent<VFXSurface>();
        ccs.VfxSurface.surfaceType = template.SurfaceType;

        ccs.BehaviourLOD = prefab.EnsureComponent<BehaviourLOD>();
        ccs.BehaviourLOD.veryCloseThreshold = template.BehaviourLODData.veryClose;
        ccs.BehaviourLOD.closeThreshold = template.BehaviourLODData.close;
        ccs.BehaviourLOD.farThreshold = template.BehaviourLODData.far;

        ccs.Animator = prefab.GetComponentInChildren<Animator>();

        // physics

        ccs.Rigidbody = prefab.EnsureComponent<Rigidbody>();
        ccs.Rigidbody.useGravity = false;
        ccs.Rigidbody.mass = template.Mass;

        var physicMaterial = template.PhysicMaterial;
        if (physicMaterial == null)
        {
            physicMaterial = ECCUtility.FrictionlessPhysicMaterial;
        }

        var colliders = prefab.GetComponentsInChildren<Collider>(true);
        foreach (var collider in colliders)
        {
            collider.sharedMaterial = physicMaterial;
        }

        ccs.WorldForces = prefab.EnsureComponent<WorldForces>();
        ccs.WorldForces.useRigidbody = ccs.Rigidbody;
        ccs.WorldForces.handleGravity = true;
        ccs.WorldForces.underwaterGravity = 0;
        ccs.WorldForces.aboveWaterGravity = 9.81f;
        ccs.WorldForces.aboveWaterDrag = 0f;
        ccs.WorldForces.underwaterDrag = 0.1f;

        // animate by velocity

        if (template.AnimateByVelocityData != null)
        {
            ccs.AnimateByVelocity = CreaturePrefabUtils.AddAnimateByVelocity(prefab, template.AnimateByVelocityData, ccs.Animator, ccs.Rigidbody, ccs.BehaviourLOD);
        }

        // behaviour

        var locomotionData = template.LocomotionData;
        if (locomotionData != null)
        {
            ccs.Locomotion = CreaturePrefabUtils.AddLocomotion(prefab, template.LocomotionData, ccs.BehaviourLOD, ccs.Rigidbody);

            ccs.SplineFollowing = CreaturePrefabUtils.AddSplineFollowing(prefab, ccs.Rigidbody, ccs.Locomotion, ccs.BehaviourLOD);

            if (template.SwimBehaviourData != null)
            {
                ccs.SwimBehaviour = CreaturePrefabUtils.AddSwimBehaviour(prefab, template.SwimBehaviourData, ccs.SplineFollowing);
            }
        }

        if (template.SwimRandomData != null)
        {
            ccs.SwimRandom = CreaturePrefabUtils.AddSwimRandom(prefab, template.SwimRandomData);
        }

        if (template.StayAtLeashData != null)
        {
            ccs.StayAtLeashPosition = CreaturePrefabUtils.AddStayAtLeashPosition(prefab, template.StayAtLeashData);
        }

        // livemixin

        var lmd = template.LiveMixinData;

        if (lmd.damageEffect == null) lmd.damageEffect = ObjectReferences.genericCreatureHit;
        if (lmd.deathEffect == null) lmd.deathEffect = ObjectReferences.genericCreatureHit;
        if (lmd.electricalDamageEffect == null) lmd.electricalDamageEffect = ObjectReferences.electrocutedEffect;
            
        ccs.LiveMixin = CreaturePrefabUtils.AddLiveMixin(prefab, lmd);

        // kharaa

        if (template.CanBeInfected)
        {
            ccs.InfectedMixin = prefab.AddComponent<InfectedMixin>();
            ccs.InfectedMixin.renderers = prefab.GetComponentsInChildren<Renderer>(true);
        }

        // main 'creature' component

        ccs.Creature = prefab.AddComponent(template.CreatureComponentType) as Creature;
        ccs.Creature.Aggression = new CreatureTrait(0f, template.TraitsData.AggressionDecreaseRate);
        ccs.Creature.Hunger = new CreatureTrait(0f, -template.TraitsData.HungerIncreaseRate);
        ccs.Creature.Scared = new CreatureTrait(0f, template.TraitsData.ScaredDecreaseRate);
        ccs.Creature.liveMixin = ccs.LiveMixin;
        ccs.Creature.traitsAnimator = ccs.Animator;
        ccs.Creature.sizeDistribution = template.SizeDistribution;
        ccs.Creature.eyeFOV = template.EyeFOV;


        // eating

        if (template.EdibleData != null)
        {
            ccs.Eatable = CreaturePrefabUtils.AddEatableComponent(prefab, template.EdibleData);
        }

        // death & damage

        ccs.CreatureDeath = prefab.AddComponent<CreatureDeath>();
        ccs.CreatureDeath.useRigidbody = ccs.Rigidbody;
        ccs.CreatureDeath.liveMixin = ccs.LiveMixin;
        ccs.CreatureDeath.respawnerPrefab = ObjectReferences.respawnerPrefab;
        ccs.CreatureDeath.eatable = ccs.Eatable;
        ccs.CreatureDeath.respawn = template.RespawnData.respawn;
        ccs.CreatureDeath.respawnOnlyIfKilledByCreature = template.RespawnData.respawnOnlyIfKilledByCreature;
        ccs.CreatureDeath.respawnInterval = template.RespawnData.respawnInterval;

        ccs.DeadAnimationOnEnable = prefab.AddComponent<DeadAnimationOnEnable>();
        ccs.DeadAnimationOnEnable.enabled = false;
        ccs.DeadAnimationOnEnable.animator = ccs.Creature.GetAnimator();
        ccs.DeadAnimationOnEnable.liveMixin = ccs.LiveMixin;
        ccs.DeadAnimationOnEnable.enabled = true;

        ccs.CreatureFlinch = CreaturePrefabUtils.AddCreatureFlinch(prefab, ccs.Animator);

        prefab.AddComponent<RemoveSoundsOnKill>();

        ccs.SoundOnDamage = prefab.AddComponent<SoundOnDamage>();
        ccs.SoundOnDamage.damageType = DamageType.Collide;
        ccs.SoundOnDamage.sound = ECCSoundAssets.FishSplat;

        // fear

        ccs.CreatureFear = prefab.AddComponent<CreatureFear>();

        if (template.FleeWhenScaredData != null)
        {
            ccs.FleeWhenScared = CreaturePrefabUtils.AddFleeWhenScared(prefab, template.FleeWhenScaredData, ccs.CreatureFear);
        }

        if (template.FleeOnDamageData != null)
        {
            ccs.FleeOnDamage = CreaturePrefabUtils.AddFleeOnDamage(prefab, template.FleeOnDamageData);
        }

        if (template.ScareableData != null)
        {
            ccs.Scareable = CreaturePrefabUtils.AddScareable(prefab, template.ScareableData, ccs.CreatureFear, ccs.Creature, ccs.FleeWhenScared);
        }

        // aggression

        ccs.LastTarget = prefab.AddComponent<LastTarget>();

        // picking up and/or holding

        var pickupableData = template.PickupableFishData;
        if (pickupableData != null)
        {
            ccs.Pickupable = prefab.AddComponent<Pickupable>();

            if (pickupableData.CanBeHeld)
            {
                HeldFish heldFish = prefab.EnsureComponent<HeldFish>();
                heldFish.animationName = pickupableData.ReferenceHoldingAnimation.ToString().ToLower();
                heldFish.mainCollider = prefab.GetComponent<Collider>();
                heldFish.pickupable = ccs.Pickupable;
                heldFish.drawTime = 0f;
                heldFish.holsterTime = 0.1f;
                heldFish.dropTime = 1.4f;
                heldFish.ikAimRightArm = true;

                if (!string.IsNullOrEmpty(pickupableData.ViewModelName))
                {
                    var fpsModel = prefab.EnsureComponent<FPModel>();
                    fpsModel.propModel = prefab.SearchChild(pickupableData.WorldModelName);
                    if (fpsModel.propModel == null)
                    {
                        ECCPlugin.logger.LogError($"Error finding World model. No child of name {pickupableData.WorldModelName} exists in the hierarchy of item {TechType}.");
                    }
                    else
                    {
                        prefab.EnsureComponent<AquariumFish>().model = fpsModel.propModel;
                    }
                    fpsModel.viewModel = prefab.SearchChild(pickupableData.ViewModelName);
                    if (fpsModel.viewModel == null)
                    {
                        ECCPlugin.logger.LogError($"Error finding View model. No child of name {pickupableData.ViewModelName} exists in the hierarchy of item {TechType}.");
                    }
                }
            }
        }

        // object avoidance

        if (template.AvoidObstaclesData != null)
        {
            CreaturePrefabUtils.AddAvoidObstacles(prefab, template.AvoidObstaclesData, ccs.LastTarget);
        }

        if (template.AvoidTerrainData != null)
        {
            CreaturePrefabUtils.AddAvoidTerrain(prefab, template.AvoidTerrainData);
        }

        // extra

        if (template.ScannerRoomScannable)
        {
            CreaturePrefabUtils.MakeObjectScannerRoomScannable(prefab, true);
        }

        return ccs;
    }
}