using ECCLibrary.Data;

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
}
