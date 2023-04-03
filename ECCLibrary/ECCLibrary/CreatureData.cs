namespace ECCLibrary;

/// <summary>
/// Contains most of the data related to registering a creature into the game.
/// </summary>
public sealed class CreatureData
{
    /// <summary>
    /// Contains most of the data related to registering a creature into the game.
    /// </summary>
    /// <param name="prefabInfo">Information for registering the prefab. MUST be unique to each creature.</param>
    /// <param name="cellLevel">Roughly determines how far this creature can be loaded in.</param>
    /// <param name="behaviourType">Goes hand in hand with the EcoTargetType. Please note the Player is a SHARK! Determines very few creature behaviours/interactions.</param>
    /// <param name="ecoTargetType">Goes hand in hand with the BehaviourType. Determines many interactions with creatures, specifically how this creature is "located" or "targeted" by other creatures</param>
    /// <param name="liveMixinData">Controls health and damage-taking aspects of this creature.</param>
    public CreatureData(PrefabInfo prefabInfo, LargeWorldEntity.CellLevel cellLevel, BehaviourType behaviourType, EcoTargetType ecoTargetType, LiveMixinData liveMixinData)
    {
        PrefabInfo = prefabInfo;
        CellLevel = cellLevel;
        BehaviourType = behaviourType;
        EcoTargetType = ecoTargetType;
        LiveMixinData = liveMixinData;
    }

    /// <summary>
    /// Information for registering the prefab. MUST be unique to each creature.
    /// </summary>
    public PrefabInfo PrefabInfo { get; set; }

    /// <summary>
    /// Physic material used for all colliders. If unassigned, will default to <see cref="ECCUtility.FrictionlessPhysicMaterial"/>.
    /// </summary>
    public PhysicMaterial PhysicMaterial { get; set; }

    /// <summary>
    /// Contains data pertaining to creating the <see cref="Locomotion"/> component.
    /// </summary>
    public LocomotionData LocomotionData { get; set; } = new LocomotionData();

    /// <summary>
    /// Mass in kg. Ranges from about 1.8f to 4050f.
    /// </summary>
    public float Mass { get; set; } = 15f;

    /// <summary>
    /// Determines the distance for which certain calculations (such as Trail Managers) perform (or don't). It is recommended to increase these values for large creatures.
    /// </summary>
    public BehaviourLODData BehaviourLODData { get; set; } = new BehaviourLODData();

    /// <summary>
    /// The FOV is used for detecting things such as prey. This value has an expected range of [0f, 1f]. Is 0.25f by default. A value of -1 means a given object is ALWAYS in view.
    /// </summary>
    public float EyeFOV { get; set; } = 0.25f;

    /// <summary>
    /// Whether the creature is immune to brine or not. False by default. Mainly used for Lost River creatures.
    /// </summary>
    public bool AcidImmune { get; set; }

    /// <summary>
    /// The Surface Type applied to the main collider.
    /// </summary>
    public VFXSurfaceTypes SurfaceType { get; set; }

    /// <summary>
    /// Roughly determines how far this creature can be loaded in.
    /// </summary>
    public LargeWorldEntity.CellLevel CellLevel { get; set; }

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
    /// Sprite asset for this creature.
    /// </summary>
    public Sprite Sprite { get; set; }

    /// <summary>
    /// Settings for growth in Alien Containment.
    /// </summary>
    public WaterParkCreatureData WaterParkCreatureData { get; set; }
}