using ECCLibrary.Data;
using ECCLibrary.Mono;
using System;

namespace ECCLibrary;

/// <summary>
/// Utilities related to creating a creature prefab object.
/// </summary>
public static partial class CreaturePrefabUtils
{
    /// <summary>
    /// Makes a given GameObject scannable with the scanner room, using the <see cref="ResourceTracker"/> component.
    /// </summary>
    /// <param name="gameObject"></param>
    /// <param name="updatePositionPeriodically">Whether to automatically update the position of this ResourceTracker or not (should always be true for creatures).</param>
    public static void MakeObjectScannerRoomScannable(GameObject gameObject, bool updatePositionPeriodically)
    {
        ResourceTracker resourceTracker = gameObject.AddComponent<ResourceTracker>();
        resourceTracker.prefabIdentifier = gameObject.GetComponent<PrefabIdentifier>();
        resourceTracker.rb = gameObject.GetComponent<Rigidbody>();
        if (updatePositionPeriodically == true)
        {
            gameObject.AddComponent<ResourceTrackerUpdater>();
        }
    }

    /// <summary>
    /// Multiplies damage of the given <paramref name="type"/> by <paramref name="multiplier"/>.
    /// </summary>
    public static DamageModifier AddDamageModifier(GameObject creature, DamageType type, float multiplier)
    {
        var modifier = creature.AddComponent<DamageModifier>();
        modifier.damageType = type;
        modifier.multiplier = multiplier;
        return modifier;
    }

    private static AnimationCurve maxRangeMultiplierCurve = new AnimationCurve(new(0, 1, 0, 0, .333f, .333f), new(0.5f, 0.5f, 0, 0, .333f, .333f), new(1, 1, 0, 0, .333f, .333f));
    private static AnimationCurve distanceAggressionMultiplierCurve = new AnimationCurve(new(0, 1, 0, 0, .333f, .333f), new(1, 0, -3, -3, .333f, .333f));

    /// <summary>
    /// Adds the <see cref="AggressiveWhenSeeTarget"/> component onto the object with the given <paramref name="data"/>.
    /// </summary>
    public static AggressiveWhenSeeTarget AddAggressiveWhenSeeTarget(GameObject creature, AggressiveWhenSeeTargetData data, LastTarget lastTarget, Creature creatureComponent)
    {
        var component = creature.AddComponent<AggressiveWhenSeeTarget>();
        component.maxRangeMultiplier = maxRangeMultiplierCurve;
        component.distanceAggressionMultiplier = distanceAggressionMultiplierCurve;
        component.lastTarget = lastTarget;
        component.creature = creatureComponent;
        component.targetType = data.targetType;
        component.aggressionPerSecond = data.aggressionPerSecond;
        component.maxRangeScalar = data.maxRangeScalar;
        component.maxSearchRings = data.maxSearchRings;
        component.ignoreSameKind = data.ignoreSameKind;
        component.targetShouldBeInfected = data.targetShouldBeInfected;
        component.minimumVelocity = data.minimumVelocity;
        component.hungerThreshold = data.hungerThreshold;
        return component;
    }

    #region Trail Managers

    /// <summary>
    /// Creates a <see cref="TrailManager"/>, which controls the procedural animations of tail-like objects.
    /// </summary>
    /// <param name="trailParent">The root of the spine and object that the <see cref="TrailManager"/> is added to. The first child of this object and all children of the first child are used for the trail.</param>
    /// <param name="components">The CreatureComponents of this creature.</param>
    /// <param name="segmentSnapSpeed">How fast each segment snaps back into the default position. A higher value gives a more rigid appearance.</param>
    /// <param name="maxSegmentOffset">How far each segment can be from the original position.</param>
    /// <param name="multiplier">The total strength of the movement. A value too low or too high will break the trail completely.</param>
    [Obsolete("See ECCLibrary.Data.TrailManagerBuilder instead.")]
    public static TrailManager CreateTrailManagerWithAllChildren(GameObject trailParent, CreatureComponents components, float segmentSnapSpeed, float maxSegmentOffset = -1f, float multiplier = 1f)
    {
        return CreateTrailManagerWithAllChildren(trailParent, components.BehaviourLOD, components.Creature.transform, segmentSnapSpeed, maxSegmentOffset, multiplier);
    }

    /// <summary>
    /// Creates a <see cref="TrailManager"/>, which controls the procedural animations of tail-like objects.
    /// </summary>
    /// <param name="trailParent">The root of the spine and object that the <see cref="TrailManager"/> is added to. The first child of this object and all children of the first child are used for the trail.</param>
    /// <param name="behaviourLOD">The BehaviourLOD of this creature.</param>
    /// <param name="creatureRoot">The creature's uppermost Transform.</param>
    /// <param name="segmentSnapSpeed">How fast each segment snaps back into the default position. A higher value gives a more rigid appearance.</param>
    /// <param name="maxSegmentOffset">How far each segment can be from the original position.</param>
    /// <param name="multiplier">The total strength of the movement. A value too low or too high will break the trail completely.</param>
    [Obsolete("See ECCLibrary.Data.TrailManagerBuilder instead.")]
    public static TrailManager CreateTrailManagerWithAllChildren(GameObject trailParent, BehaviourLOD behaviourLOD, Transform creatureRoot, float segmentSnapSpeed, float maxSegmentOffset = -1f, float multiplier = 1f)
    {
        trailParent.gameObject.SetActive(false);

        TrailManager tm = trailParent.AddComponent<TrailManager>();
        tm.trails = trailParent.transform.GetChild(0).GetComponentsInChildren<Transform>();
        tm.rootTransform = creatureRoot;
        tm.rootSegment = tm.transform;
        tm.levelOfDetail = behaviourLOD;
        tm.segmentSnapSpeed = segmentSnapSpeed;
        tm.maxSegmentOffset = maxSegmentOffset;
        tm.allowDisableOnScreen = false;
        TrailManagerUtilities.SetAllMultiplierCurves(tm, TrailManagerUtilities.FlatMultiplierAnimationCurve);

        trailParent.gameObject.SetActive(true);
        return tm;
    }

    /// <summary>
    /// Creates a <see cref="TrailManager"/>, which controls the procedural animations of tail-like objects.
    /// </summary>
    /// <param name="trailRoot">The root of the spine and object that the <see cref="TrailManager"/> is added to.</param>
    /// <param name="components">The CreatureComponents of this creature.</param>
    /// <param name="trails">Any objects that are simulated. Should NOT include the <paramref name="trailRoot"/>'s transform.</param>
    /// <param name="segmentSnapSpeed">How fast each segment snaps back into the default position. A higher value gives a more rigid appearance.</param>
    /// <param name="maxSegmentOffset">How far each segment can be from the original position.</param>
    [Obsolete("See ECCLibrary.Data.TrailManagerBuilder instead.")]
    public static TrailManager CreateTrailManagerManually(GameObject trailRoot, CreatureComponents components, Transform[] trails, float segmentSnapSpeed, float maxSegmentOffset = -1f)
    {
        return CreateTrailManagerManually(trailRoot, components.BehaviourLOD, components.Creature.transform, trails, segmentSnapSpeed, maxSegmentOffset); 
    }

    /// <summary>
    /// Creates a <see cref="TrailManager"/>, which controls the procedural animations of tail-like objects.
    /// </summary>
    /// <param name="trailRoot">The root of the spine and object that the <see cref="TrailManager"/> is added to.</param>
    /// <param name="behaviourLOD">The BehaviourLOD of this creature.</param>
    /// <param name="creatureRoot">The creature's uppermost Transform.</param>
    /// <param name="trails">Any objects that are simulated. Should NOT include the <paramref name="trailRoot"/>'s transform.</param>
    /// <param name="segmentSnapSpeed">How fast each segment snaps back into the default position. A higher value gives a more rigid appearance.</param>
    /// <param name="maxSegmentOffset">How far each segment can be from the original position.</param>
    [Obsolete("See ECCLibrary.Data.TrailManagerBuilder instead.")]
    public static TrailManager CreateTrailManagerManually(GameObject trailRoot, BehaviourLOD behaviourLOD, Transform creatureRoot, Transform[] trails, float segmentSnapSpeed, float maxSegmentOffset = -1f)
    {
        trailRoot.gameObject.SetActive(false);

        TrailManager tm = trailRoot.AddComponent<TrailManager>();
        tm.trails = trails;
        tm.rootTransform = creatureRoot;
        tm.rootSegment = tm.transform;
        tm.levelOfDetail = behaviourLOD;
        tm.segmentSnapSpeed = segmentSnapSpeed;
        tm.maxSegmentOffset = maxSegmentOffset;
        tm.allowDisableOnScreen = false;
        TrailManagerUtilities.SetAllMultiplierCurves(tm, TrailManagerUtilities.FlatMultiplierAnimationCurve);

        trailRoot.gameObject.SetActive(true);
        return tm;
    }

    #endregion

    /// <summary>
    /// Adds an instance of the <see cref="OnTouch"/> component onto <paramref name="triggerObject"/> that calls the <paramref name="onTouchCallback"/> method.
    /// </summary>
    /// <param name="triggerObject">The object that holds the touch trigger, for example a creature's mouth collider. Must have Collider with <see cref="Collider.isTrigger"/> set to TRUE.</param>
    /// <param name="onTouchCallback">The action performed when the object is collided with.</param>
    /// <returns></returns>
    public static OnTouch AddOnTouchTrigger(GameObject triggerObject, Action<Collider> onTouchCallback)
    {
        triggerObject.EnsureComponent<VFXSurface>().surfaceType = VFXSurfaceTypes.organic;
        var collider = triggerObject.GetComponent<Collider>();
        if (collider == null) ECCPlugin.logger.LogError($"No Collider found on trigger object '{triggerObject}'. This WILL cause errors!");
        else if (!collider.isTrigger) ECCPlugin.logger.LogError($"Collider '{collider}' is not a trigger! This will NOT work!");
        var onTouch = triggerObject.EnsureComponent<OnTouch>();
        var setDelayed = triggerObject.EnsureComponent<SetOnTouchCallbackDelayed>();
        setDelayed.onTouch = onTouch;
        setDelayed.callback = onTouchCallback;
        return onTouch;
    }
}