using ECCLibrary.Data;
using ECCLibrary.Mono;

namespace ECCLibrary;

/// <summary>
/// Override this class to define a new creature. Call the <see cref="Register"/> method on an instance to add it to the game.
/// </summary>
public abstract class CreatureAsset
{
    internal CreatureTemplate Template { get; }

    /// <summary>
    /// Information for registering the prefab.
    /// </summary>
    public PrefabInfo PrefabInfo { get; }

    /// <summary>
    /// A reference to the custom prefab instance.
    /// </summary>
    public ICustomPrefab CustomPrefab => CustomPrefabInstance;

    private CustomPrefab CustomPrefabInstance { get; }

    /// <summary>
    /// Instantiates a Creature Asset with the given PrefabInfo. Call the Register method to add the creature to the game.
    /// </summary>
    /// <param name="prefabInfo">
    /// <para>Information required for spawning. Must be unique.</para>
    /// <para>An instance of this struct can be easily created by calling <see cref="PrefabInfo.WithTechType"/>.</para></param>
    public CreatureAsset(PrefabInfo prefabInfo)
    {
        PrefabInfo = prefabInfo;
        CustomPrefabInstance = new CustomPrefab(prefabInfo);
        Template = CreateTemplate();
    }

    /// <summary>
    /// The ClassID of this creature, sourced from the PrefabInfo property.
    /// </summary>
    public string ClassID
    {
        get
        {
            return PrefabInfo.ClassID;
        }
    }

    /// <summary>
    /// The TechType of this creature, sourced from the PrefabInfo property.
    /// </summary>
    public TechType TechType
    {
        get
        {
            return PrefabInfo.TechType;
        }
    }

    /// <summary>
    /// The EntityInfo for this creature, only assigned <i>after</i> <see cref="Register"/> is called.
    /// </summary>
    public UWE.WorldEntityInfo EntityInfo { get; private set; }

    /// <summary>
    /// Registers this creature into the game.
    /// </summary>
    public void Register()
    {
        // Check validity of essentials

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

        // Assign patch-time data

        if (Template.AcidImmune) CreatureDataUtils.SetAcidImmune(TechType);
        if (Template.BioReactorCharge > 0f) CreatureDataUtils.SetBioreactorCharge(TechType, Template.BioReactorCharge);
        if (Template.PickupableFishData != null && Template.PickupableFishData.CanBeHeld) CraftDataHandler.SetEquipmentType(TechType, EquipmentType.Hand);
        CreatureDataUtils.SetBehaviorType(TechType, Template.BehaviourType);
        CreatureDataUtils.SetItemSounds(TechType, Template.ItemSoundsType);
        EntityInfo = new UWE.WorldEntityInfo() { cellLevel = Template.CellLevel, classId = ClassID, localScale = Vector3.one, prefabZUp = false, slotType = EntitySlot.Type.Creature, techType = TechType };
        WorldEntityDatabaseHandler.AddCustomInfo(ClassID, EntityInfo);

        // Register the Custom Prefab

        CustomPrefabInstance.SetGameObject(GetGameObject);
        CustomPrefabInstance.Register();

        PostRegister();
    }

    /// <summary>
    /// An empty method that can be overriden to insert code that runs directly after the prefab is registered (runs at patch time).
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

        var prefab = Object.Instantiate(Template.Model);
        prefab.name = PrefabInfo.ClassID;
        prefab.tag = "Creature";
        prefab.SetActive(false);

        var components = AddComponents(prefab);

        yield return ModifyPrefab(prefab, components);

        ApplyMaterials(prefab);

        gameObject.Set(prefab);
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
        ccs.LargeWorldEntity.cellLevel = Template.CellLevel;

        ccs.EntityTag = prefab.EnsureComponent<EntityTag>();
        ccs.EntityTag.slotType = EntitySlot.Type.Creature;

        ccs.SkyApplier = prefab.EnsureComponent<SkyApplier>();
        ccs.SkyApplier.renderers = prefab.GetComponentsInChildren<Renderer>(true);

        ccs.EcoTarget = prefab.EnsureComponent<EcoTarget>();
        ccs.EcoTarget.type = Template.EcoTargetType;

        ccs.VfxSurface = prefab.EnsureComponent<VFXSurface>();
        ccs.VfxSurface.surfaceType = Template.SurfaceType;

        ccs.BehaviourLOD = prefab.EnsureComponent<BehaviourLOD>();
        ccs.BehaviourLOD.veryCloseThreshold = Template.BehaviourLODData.VeryClose;
        ccs.BehaviourLOD.closeThreshold = Template.BehaviourLODData.Close;
        ccs.BehaviourLOD.farThreshold = Template.BehaviourLODData.Far;

        ccs.Animator = prefab.GetComponentInChildren<Animator>();

        if (ccs.Animator == null) ECCPlugin.logger.LogError($"No Animator found on creature '{PrefabInfo.ClassID}'. This WILL cause errors!");

        // physics

        ccs.Rigidbody = prefab.EnsureComponent<Rigidbody>();
        ccs.Rigidbody.useGravity = false;
        ccs.Rigidbody.mass = Template.Mass;

        var physicMaterial = Template.PhysicMaterial;
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

        if (Template.AnimateByVelocityData != null)
        {
            ccs.AnimateByVelocity = CreaturePrefabUtils.AddAnimateByVelocity(prefab, Template.AnimateByVelocityData, ccs.Animator, ccs.Rigidbody, ccs.BehaviourLOD);
        }

        // basic swimming behaviour

        var locomotionData = Template.LocomotionData;
        if (locomotionData != null)
        {
            ccs.Locomotion = CreaturePrefabUtils.AddLocomotion(prefab, Template.LocomotionData, ccs.BehaviourLOD, ccs.Rigidbody);

            ccs.SplineFollowing = CreaturePrefabUtils.AddSplineFollowing(prefab, ccs.Rigidbody, ccs.Locomotion, ccs.BehaviourLOD);

            if (Template.SwimBehaviourData != null)
            {
                ccs.SwimBehaviour = CreaturePrefabUtils.AddSwimBehaviour(prefab, Template.SwimBehaviourData, ccs.SplineFollowing);
            }
        }

        if (Template.SwimRandomData != null)
        {
            ccs.SwimRandom = CreaturePrefabUtils.AddSwimRandom(prefab, Template.SwimRandomData);
        }

        if (Template.StayAtLeashData != null)
        {
            ccs.StayAtLeashPosition = CreaturePrefabUtils.AddStayAtLeashPosition(prefab, Template.StayAtLeashData);
        }

        // livemixin

        var lmd = Template.LiveMixinData;

        if (lmd.damageEffect == null) lmd.damageEffect = ObjectReferences.genericCreatureHit;
        if (lmd.deathEffect == null) lmd.deathEffect = ObjectReferences.genericCreatureHit;
        if (lmd.electricalDamageEffect == null) lmd.electricalDamageEffect = ObjectReferences.electrocutedEffect;
            
        ccs.LiveMixin = CreaturePrefabUtils.AddLiveMixin(prefab, lmd);

        // kharaa

        if (Template.CanBeInfected)
        {
            ccs.InfectedMixin = prefab.AddComponent<InfectedMixin>();
            ccs.InfectedMixin.renderers = prefab.GetComponentsInChildren<Renderer>(true);
        }

        // main 'creature' component

        ccs.Creature = prefab.AddComponent(Template.CreatureComponentType) as Creature;
        ccs.Creature.Aggression = new CreatureTrait(0f, Template.TraitsData.AggressionDecreaseRate);
        ccs.Creature.Hunger = new CreatureTrait(0f, -Template.TraitsData.HungerIncreaseRate);
        ccs.Creature.Scared = new CreatureTrait(0f, Template.TraitsData.ScaredDecreaseRate);
        ccs.Creature.liveMixin = ccs.LiveMixin;
        ccs.Creature.traitsAnimator = ccs.Animator;
        ccs.Creature.sizeDistribution = Template.SizeDistribution;
        ccs.Creature.eyeFOV = Template.EyeFOV;

        // eating

        if (Template.EdibleData != null)
        {
            ccs.Eatable = CreaturePrefabUtils.AddEatable(prefab, Template.EdibleData);
        }

        // death & damage

        ccs.CreatureDeath = prefab.AddComponent<CreatureDeath>();
        ccs.CreatureDeath.useRigidbody = ccs.Rigidbody;
        ccs.CreatureDeath.liveMixin = ccs.LiveMixin;
        ccs.CreatureDeath.respawnerPrefab = ObjectReferences.respawnerPrefab;
        ccs.CreatureDeath.eatable = ccs.Eatable;
        ccs.CreatureDeath.respawn = Template.RespawnData.respawn;
        ccs.CreatureDeath.respawnOnlyIfKilledByCreature = Template.RespawnData.respawnOnlyIfKilledByCreature;
        ccs.CreatureDeath.respawnInterval = Template.RespawnData.respawnInterval;

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

        if (Template.FleeWhenScaredData != null)
        {
            ccs.FleeWhenScared = CreaturePrefabUtils.AddFleeWhenScared(prefab, Template.FleeWhenScaredData, ccs.CreatureFear);
        }

        if (Template.FleeOnDamageData != null)
        {
            ccs.FleeOnDamage = CreaturePrefabUtils.AddFleeOnDamage(prefab, Template.FleeOnDamageData);
        }

        if (Template.ScareableData != null)
        {
            ccs.Scareable = CreaturePrefabUtils.AddScareable(prefab, Template.ScareableData, ccs.CreatureFear, ccs.Creature, ccs.FleeWhenScared);
        }

        // aggression

        ccs.LastTarget = prefab.AddComponent<LastTarget>();

        if (Template.AttackLastTargetData != null)
        {
            ccs.AttackLastTarget = CreaturePrefabUtils.AddAttackLastTargetData(prefab, Template.AttackLastTargetData, ccs.LastTarget);
        }

        if (Template.AggressiveWhenSeeTargetList != null)
        {
            foreach (var aggression in Template.AggressiveWhenSeeTargetList)
            {
                CreaturePrefabUtils.AddAggressiveWhenSeeTarget(prefab, aggression, ccs.LastTarget, ccs.Creature);
            }
        }

        if (Template.AttackCyclopsData != null)
        {
            ccs.AttackCyclops = CreaturePrefabUtils.AddAttackCyclops(prefab, Template.AttackCyclopsData, ccs.LastTarget);
        }

        // picking up and/or holding

        var pickupableData = Template.PickupableFishData;
        if (pickupableData != null)
        {
            ccs.Pickupable = prefab.AddComponent<Pickupable>();

            if (pickupableData.CanBeHeld)
            {
                HeldFish heldFish = prefab.EnsureComponent<HeldFish>();
                heldFish.SetAnimationTechTypeReference(pickupableData.ReferenceHoldingAnimation);
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

        if (Template.AvoidObstaclesData != null)
        {
            CreaturePrefabUtils.AddAvoidObstacles(prefab, Template.AvoidObstaclesData, ccs.LastTarget);
        }

        if (Template.AvoidTerrainData != null)
        {
            CreaturePrefabUtils.AddAvoidTerrain(prefab, Template.AvoidTerrainData);
        }

        // misc behaviour

        if (Template.SwimInSchoolData != null)
        {
            CreaturePrefabUtils.AddSwimInSchool(prefab, Template.SwimInSchoolData);
        }

        // alien containment (water park)

        if (Template.WaterParkCreatureData != null)
        {
            CreaturePrefabUtils.AddWaterParkCreature(prefab, Template.WaterParkCreatureData);
        }

        // extra

        if (Template.ScannerRoomScannable)
        {
            CreaturePrefabUtils.MakeObjectScannerRoomScannable(prefab, true);
        }

        return ccs;
    }
}