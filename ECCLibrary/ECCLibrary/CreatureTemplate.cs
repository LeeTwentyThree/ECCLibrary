﻿using ProtoBuf;
using System;

namespace ECCLibrary;

/// <summary>
/// Contains most of the data related to registering a creature into the game.
/// </summary>
public sealed class CreatureTemplate
{
    /// <summary>
    /// Contains most of the data related to registering a creature into the game.
    /// </summary>
    /// <param name="prefabInfo">Information for registering the prefab. MUST be unique to each creature.</param>
    /// <param name="model">The model that is cloned to create the creature GameObject.</param>
    /// <param name="behaviourType">Goes hand in hand with the EcoTargetType. Please note the Player is a SHARK! Determines very few creature behaviours/interactions.</param>
    /// <param name="ecoTargetType">Goes hand in hand with the BehaviourType. Determines many interactions with creatures, specifically how this creature is "located" or "targeted" by other creatures</param>
    /// <param name="liveMixinData">Controls health and damage-taking aspects of this creature.</param>
    public CreatureTemplate(PrefabInfo prefabInfo, GameObject model, BehaviourType behaviourType, EcoTargetType ecoTargetType, LiveMixinData liveMixinData)
    {
        PrefabInfo = prefabInfo;
        Model = model;
        BehaviourType = behaviourType;
        EcoTargetType = ecoTargetType;
        LiveMixinData = liveMixinData;
    }

    /// <summary>
    /// Information for registering the prefab. MUST be unique to each creature.
    /// </summary>
    public PrefabInfo PrefabInfo { get; set; }

    /// <summary>
    /// The model that is cloned to create the creature GameObject.
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
    /// Contains data pertaining to the <see cref="SwimRandom"/> action.
    /// </summary>
    public SwimRandomData SwimRandomData { get; set; } = new SwimRandomData(0.2f, Vector3.one * 20f, 3f);

    /// <summary>
    /// The Type of the main component that must inherit from <see cref="Creature"/>.
    /// </summary>
    public Type CreatureComponentType { get; private set; } = typeof(Creature);

    /// <summary>
    /// Sets the Type of the main component. Must inherit from <see cref="Creature"/>.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public void SetCreatureComponentType<T>() where T : Creature
    {
        CreatureComponentType = typeof(T);
    }

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
    /// Whether the creature is immune to brine or not. False by default. Typically useful for Lost River creatures.
    /// </summary>
    public bool AcidImmune { get; set; }

    /// <summary>
    /// Total power output of this creature. All ECC creatures can be put in the bioreactor unless this value is LESS than 0.
    /// </summary>
    public float BioReactorCharge { get; set; } = 200f;

    /// <summary>
    /// The Surface Type applied to the main collider.
    /// </summary>
    public VFXSurfaceTypes SurfaceType { get; set; }

    /// <summary>
    /// Settings that determine basic attributes of the creature.
    /// </summary>
    public CreatureTraitsData TraitsData { get; set; } = new CreatureTraitsData();

    /// <summary>
    /// Whether this creature can randomly spawn with Kharaa symptoms. True by default.
    /// </summary>
    public bool CanBeInfected { get; set; }

    /// <summary>
    /// If set to true, the Scanner Room can scan for this creature. False by default.
    /// </summary>
    public bool ScannerRoomScannable { get; set; }

    /// <summary>
    /// Possible sizes for this creature. Randomly picks a value in the range of 0 to 1. This value can not go above 1. Flat curve at 1 by default.
    /// </summary>
    public AnimationCurve SizeDistribution { get; set; } = new AnimationCurve(new Keyframe[] { new(0, 1), new(1, 1) });

    /// <summary>
    /// Roughly determines how far this creature can be loaded in.
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
    /// Sprite asset for this creature.
    /// </summary>
    public Sprite Sprite { get; set; }

    /// <summary>
    /// Settings for growth in Alien Containment.
    /// </summary>
    public WaterParkCreatureData WaterParkCreatureData { get; set; }

    /// <summary>
    /// Pickup sounds of the item.
    /// </summary>
    public ItemSoundsType ItemSoundsType { get; set; } = ItemSoundsType.Fish;
}