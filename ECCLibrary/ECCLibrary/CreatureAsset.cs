using ECCLibrary.Data;

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
    /// <param name="obj"></param>
    /// <param name="components"></param>
    /// <returns></returns>
    protected abstract IEnumerator ModifyPrefab(GameObject obj, CreatureComponents components);

    private IEnumerator GetGameObject(IOut<GameObject> gameObject)
    {
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

        if (template.StayAtLeashData != null)
        {
            CreaturePrefabUtils.AddStayAtLeashPosition(prefab, template.StayAtLeashData);
        }

        // livemixin

        ccs.liveMixin = CreaturePrefabUtils.AddLiveMixin(prefab, template.LiveMixinData);

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

        var deadAnimationOnEnable = prefab.AddComponent<DeadAnimationOnEnable>();
        deadAnimationOnEnable.enabled = false;
        deadAnimationOnEnable.animator = ccs.creature.GetAnimator();
        deadAnimationOnEnable.liveMixin = ccs.liveMixin;
        deadAnimationOnEnable.enabled = true;

        // extra

        if (template.ScannerRoomScannable)
        {
            CreaturePrefabUtils.MakeObjectScannerRoomScannable(prefab, true);
        }

        return ccs;
    }
}