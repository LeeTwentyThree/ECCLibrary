using System;

namespace ECCLibrary.Data;

/// <summary>
/// This class contains all settings related to the registering and automatic prefab initialization of creatures by the library.
/// </summary>
public sealed class CreatureTemplate
{
    /// <summary>
    /// Creatures a new basic creature template instance. Beyond the constructor, this instance also contains several properties that can be modified.
    /// </summary>
    /// <param name="model">The model that is cloned to create the creature GameObject.</param>
    /// <param name="behaviourType">Goes hand in hand with the EcoTargetType. Please note the Player is a SHARK! Determines very few creature behaviours/interactions.</param>
    /// <param name="ecoTargetType">Goes hand in hand with the BehaviourType. Determines many interactions with creatures, specifically how this creature is "located" or "targeted" by other creatures</param>
    /// <param name="maxHealth">Maximum health of this creature.</param>
    public CreatureTemplate(GameObject model, BehaviourType behaviourType, EcoTargetType ecoTargetType, float maxHealth)
    {
        Model = model;
        BehaviourType = behaviourType;
        EcoTargetType = ecoTargetType;
        LiveMixinData = CreatureDataUtils.CreateLiveMixinData(maxHealth);
    }

    /// <summary>
    /// <para>The model that is cloned to create the creature GameObject.</para>
    /// <para>This object is NOT cached; for anything that isn't directly loaded from an asset bundle, it is recommended to use <see cref="UnityEngine.Object.DontDestroyOnLoad"/>.
    /// If you want to be even safer, add the <see cref="SceneCleanerPreserve"/> to your object.</para>
    /// </summary>
    public GameObject Model { get; set; }

    /// <summary>
    /// Physic material used for all colliders. If unassigned, will default to <see cref="ECCUtility.FrictionlessPhysicMaterial"/>.
    /// </summary>
    public PhysicMaterial PhysicMaterial { get; set; }

    /// <summary>
    /// Contains data pertaining to creating the <see cref="Locomotion"/> component.
    /// </summary>
    public LocomotionData LocomotionData { get; set; } = new LocomotionData();

    /// <summary>
    /// Contains data pertaining to creating the <see cref="SwimBehaviour"/> component.
    /// </summary>
    public SwimBehaviourData SwimBehaviourData { get; set; } = new SwimBehaviourData();

    /// <summary>
    /// Contains data pertaining to the <see cref="AnimateByVelocity"/> component. This component sets animation parameters based on the creature's direction &#38; velocity.
    /// <br/> Means the 'speed' parameter can be used in the creature's Animator.
    /// <br/> NOT assigned by default!
    /// </summary>
    public AnimateByVelocityData AnimateByVelocityData { get; set; } = null;

    /// <summary>
    /// Contains data pertaining to the <see cref="SwimRandom"/> action. Assigned a generic value by default, but can be changed or set to null.
    /// </summary>
    public SwimRandomData SwimRandomData { get; set; } = new SwimRandomData(0.2f, 3f, Vector3.one * 20f);

    /// <summary>
    /// Contains data pertaining to the <see cref="StayAtLeashPosition"/> action. This component keeps creatures from wandering too far. Not assigned by default.
    /// </summary>
    public StayAtLeashData StayAtLeashData { get; set; }

    /// <summary>
    /// Contains data pertaining to the <see cref="FleeWhenScared"/> action. Not assigned by default.
    /// </summary>
    public FleeWhenScaredData FleeWhenScaredData { get; set; }

    /// <summary>
    /// Contains data pertaining to the <see cref="FleeOnDamage"/> action. Assigned by default with default values and a priority of 0.8f.
    /// </summary>
    public FleeOnDamageData FleeOnDamageData { get; set; } = new FleeOnDamageData(0.8f);

    /// <summary>
    /// Contains data pertaining to the <see cref="Scareable"/> component. This component is what enables small fish to swim away from the player and potential predators. Not assigned by default.
    /// </summary>
    public ScareableData ScareableData { get; set; } = null;

    /// <summary>
    /// Contains data pertaining to picking up and/or holding fish in your hands. Not assigned by default.
    /// </summary>
    public PickupableFishData PickupableFishData { get; set; } = null;

    /// <summary>
    /// Contains data pertaining to creating the <see cref="AvoidObstacles"/> CreatureAction. This component is used by most creatures (everything besides leviathans) to avoid objects and/or terrain. Not assigned by default.
    /// </summary>
    public AvoidObstaclesData AvoidObstaclesData { get; set; } = null;

    /// <summary>
    /// Contains data pertaining to creating the <see cref="AvoidTerrain"/> CreatureAction. This is a more advanced and expensive collision avoidance system used by leviathans.
    /// </summary>
    public AvoidTerrainData AvoidTerrainData { get; set; } = null;

    /// <summary>
    /// Contains data pertaining to the <see cref="CreatureDeath"/> component. Assigned by default to enable respawning. MUST be assigned!
    /// </summary>
    public RespawnData RespawnData { get; set; } = new RespawnData(true);

    /// <summary>
    /// Contains data pertaining to the <see cref="AggressiveToPilotingVehicle"/> component, which encourages creatures to target any small vehicle that the player may be piloting
    /// (this includes ANY vehicle that inherits from the <see cref="Vehicle"/> component i.e. the Seamoth or Prawn Suit). Not many creatures use this component, but ones that do
    /// will be VERY aggressive (Boneshark levels of aggression!).
    /// </summary>
    public AggressiveToPilotingVehicleData AggressiveToPilotingVehicleData { get; set; } = null;

    /// <summary>
    /// The Type of the main component that must inherit from <see cref="Creature"/>.
    /// </summary>
    public Type CreatureComponentType { get; private set; } = typeof(Creature);

    /// <summary>
    /// Sets the Type of the main component. Must inherit from <see cref="Creature"/>.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public CreatureTemplate SetCreatureComponentType<T>() where T : Creature
    {
        CreatureComponentType = typeof(T);
        return this;
    }

    /// <summary>
    /// A list of all data pertaining to the <see cref="AggressiveWhenSeeTarget"/> component, which enables the creature to become aggressive towards specific fauna/the player.
    /// </summary>
    public List<AggressiveWhenSeeTargetData> AggressiveWhenSeeTargetList { get; set; }

    /// <summary>
    /// Adds a single type of aggression to this creature. This method can be called MULTIPLE TIMES to add multiple types of aggression! Not functional without the <see cref="AttackLastTarget"/> component.
    /// </summary>
    /// <param name="data"></param>
    public CreatureTemplate AddAggressiveWhenSeeTargetData(AggressiveWhenSeeTargetData data)
    {
        AggressiveWhenSeeTargetList ??= new List<AggressiveWhenSeeTargetData>();
        AggressiveWhenSeeTargetList.Add(data);
        return this;
    }

    /// <summary>
    /// Contains data pertaining to creating the <see cref="AttackLastTarget"/> CreatureAction. Not assigned by default.
    /// </summary>
    public AttackLastTargetData AttackLastTargetData { get; set; } = null;

    /// <summary>
    /// Contains data pertaining to creating the <see cref="AttackCyclops"/> CreatureAction. Not assigned by default.
    /// </summary>
    public AttackCyclopsData AttackCyclopsData { get; set; } = null;

    /// <summary>
    /// <para>Contains data pertaining to adding the <see cref="SwimInSchool"/> CreatureAction.</para>
    /// <para>Each schooling creature chooses a single "leader" larger than itself (and of the same TechType) to follow. Therefore, the <see cref="CreatureTemplate.SizeDistribution"/> property should be defined for this action to function properly.</para>
    /// <para>Not assigned by default.</para>
    /// </summary>
    public SwimInSchoolData SwimInSchoolData { get; set; } = null;

    /// <summary>
    /// Mass in kg. Ranges from about 1.8f to 4050f. Default is 15kg.
    /// </summary>
    public float Mass { get; set; } = 10f;

    /// <summary>
    /// Determines the distance for which certain calculations (such as Trail Managers) perform (or don't). It is recommended to increase these values for large creatures.
    /// </summary>
    public BehaviourLODData BehaviourLODData { get; set; } = new BehaviourLODData();

    /// <summary>
    /// The FOV is used for detecting things such as prey. SHOULD BE NEGATIVE! This value has an expected range of [-1, 0]. Is 0f by default. A value of -1 means a given object is ALWAYS in view.
    /// </summary>
    public float EyeFOV { get; set; } = 0f;

    /// <summary>
    /// Whether the creature is immune to brine or not. False by default. Typically useful for Lost River creatures.
    /// </summary>
    public bool AcidImmune { get; set; } = false;

    /// <summary>
    /// Total power output of this creature. All ECC creatures can be put in the bioreactor as long as this value is greater than 0. Default value is 200.
    /// </summary>
    public float BioReactorCharge { get; set; } = 200f;

    /// <summary>
    /// The Surface Type applied to the main collider. Default is <see cref="VFXSurfaceTypes.organic"/>.
    /// </summary>
    public VFXSurfaceTypes SurfaceType { get; set; } = VFXSurfaceTypes.organic;

    /// <summary>
    /// Settings that determine basic attributes of the creature.
    /// </summary>
    public CreatureTraitsData TraitsData { get; set; } = new CreatureTraitsData(0.1f, 0.05f, 0.1f);

    /// <summary>
    /// Contains data pertaining to the <see cref="Eatable"/> [sic] component. Not assigned by default.
    /// </summary>
    public EdibleData EdibleData { get; set; } = null; 

    /// <summary>
    /// Whether this creature can randomly spawn with Kharaa symptoms. True by default.
    /// </summary>
    public bool CanBeInfected { get; set; } = true;

    /// <summary>
    /// If set to true, the Scanner Room can scan for this creature. False by default.
    /// </summary>
    public bool ScannerRoomScannable { get; set; } = false;

    /// <summary>
    /// Possible sizes for this creature. Randomly picks a value in the range of 0 to 1. This value can not go above 1. Flat curve at 1 by default.
    /// </summary>
    public AnimationCurve SizeDistribution { get; set; } = new AnimationCurve(new Keyframe[] { new(0, 1), new(1, 1) });

    /// <summary>
    /// Roughly determines how far this creature can be loaded in. Default value is <see cref="LargeWorldEntity.CellLevel.Medium"/>.
    /// </summary>
    public LargeWorldEntity.CellLevel CellLevel { get; set; } = LargeWorldEntity.CellLevel.Medium;

    /// <summary>
    /// Goes hand in hand with the EcoTargetType. Please note the Player is a SHARK! Determines very few creature behaviours/interactions.
    /// </summary>
    public BehaviourType BehaviourType { get; set; }

    /// <summary>
    /// Goes hand in hand with the BehaviourType. Determines many interactions with creatures, specifically how this creature is "located" or "targeted" by other creatures.
    /// </summary>
    public EcoTargetType EcoTargetType { get; set; }

    /// <summary>
    /// Controls health and damage-taking aspects of this creature.
    /// </summary>
    public LiveMixinData LiveMixinData { get; set; }

    /// <summary>
    /// Settings for growth in Alien Containment. Not assigned by default.
    /// </summary>
    public WaterParkCreatureData WaterParkCreatureData { get; private set; }

    /// <summary>
    /// Properly assigns values to the <see cref="WaterParkCreatureData"/> property, and creates a new instance of the ScriptableObject if null.
    /// </summary>
    /// <param name="dataStruct"></param>
    public CreatureTemplate SetWaterParkCreatureData(WaterParkCreatureDataStruct dataStruct)
    {
        if (WaterParkCreatureData == null)
        {
            WaterParkCreatureData = ScriptableObject.CreateInstance<WaterParkCreatureData>();
        }
        WaterParkCreatureData.initialSize = dataStruct.initialSize;
        WaterParkCreatureData.maxSize = dataStruct.maxSize;
        WaterParkCreatureData.outsideSize = dataStruct.outsideSize;
        WaterParkCreatureData.daysToGrow = dataStruct.daysToGrow;
        WaterParkCreatureData.isPickupableOutside = dataStruct.isPickupableOutside;
        WaterParkCreatureData.canBreed = dataStruct.canBreed;
        WaterParkCreatureData.eggOrChildPrefab = dataStruct.eggOrChildPrefab;
        WaterParkCreatureData.adultPrefab = dataStruct.adultPrefab;
        return this;
    }

    /// <summary>
    /// Pickup sounds of the item.
    /// </summary>
    public ItemSoundsType ItemSoundsType { get; set; } = ItemSoundsType.Fish;
}