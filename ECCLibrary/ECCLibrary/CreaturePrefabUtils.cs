using SMLHelper;

namespace ECCLibrary;

/// <summary>
/// Utilities related to creating a creature prefab object.
/// </summary>
public class CreaturePrefabUtils
{
    /// <summary>
    /// Adds the <see cref="SwimBehaviour"/> component.
    /// </summary>
    /// <param name="creature"></param>
    /// <param name="data"></param>
    /// <param name="splineFollowing"></param>
    /// <returns></returns>
    public static SwimBehaviour AddSwimBehaviour(GameObject creature, SwimBehaviourData data, SplineFollowing splineFollowing)
    {
        var component = creature.EnsureComponent<SwimBehaviour>();
        component.turnSpeed = data.turnSpeed;
        component.splineFollowing = splineFollowing;
        return component;
    }

    /// <summary>
    /// Adds the <see cref="SwimRandom"/> component.
    /// </summary>
    /// <param name="creature"></param>
    /// <param name="data"></param>
    /// <returns></returns>
    public static SwimRandom AddSwimRandom(GameObject creature, SwimRandomData data)
    {
        var component = creature.EnsureComponent<SwimRandom>();
        component.swimRadius = data.swimRadius;
        component.swimForward = data.swimForward;
        component.swimVelocity = data.swimVelocity;
        component.swimInterval = data.swimInterval;
        component.onSphere = data.onSphere;
        return component;
    }

    /// <summary>
    /// Adds the <see cref="Locomotion"/> component.
    /// </summary>
    /// <param name="creature"></param>
    /// <param name="data"></param>
    /// <param name="levelOfDetail"></param>
    /// <param name="rigidbody"></param>
    /// <returns></returns>
    public static Locomotion AddLocomotion(GameObject creature, LocomotionData data, BehaviourLOD levelOfDetail, Rigidbody rigidbody)
    {
        var component = creature.EnsureComponent<Locomotion>();
        component.levelOfDetail = levelOfDetail;
        component.useRigidbody = rigidbody;

        component.maxAcceleration = data.maxAcceleration;
        component.forwardRotationSpeed = data.forwardRotationSpeed;
        component.upRotationSpeed = data.upRotationSpeed;
        component.driftFactor = data.driftFactor;
        component.canMoveAboveWater = data.canMoveAboveWater;
        component.canWalkOnSurface = data.canWalkOnSurface;
        component.freezeHorizontalRotation = data.freezeHorizontalRotation;
        return component;
    }

    /// <summary>
    /// Adds the <see cref="SplineFollowing"/> component.
    /// </summary>
    /// <param name="rb"></param>
    /// <param name="locomotion"></param>
    /// <param name="behaviourLOD"></param>
    /// <returns></returns>
    public static SplineFollowing AddSplineFollowing(GameObject creature, Rigidbody rb, Locomotion locomotion, BehaviourLOD behaviourLOD)
    {
        var component = creature.EnsureComponent<SplineFollowing>();
        component.useRigidbody = rb;
        component.locomotion = locomotion;
        component.levelOfDetail = behaviourLOD;
        return component;

    }

    /// <summary>
    /// Adds the <see cref="LiveMixin"/> component.
    /// </summary>
    /// <param name="creature"></param>
    /// <param name="data"></param>
    /// <returns></returns>
    public static LiveMixin AddLiveMixin(GameObject creature, LiveMixinData data)
    {
        var component = creature.EnsureComponent<LiveMixin>();
        component.data = data;
        return component;
    }

    /// <summary>
    /// Creates an instance of the <see cref="LiveMixinData"/> ScriptableObject.
    /// </summary>
    /// <param name="maxHealth"></param>
    /// <param name="weldable"></param>
    /// <param name="knifeable"></param>
    /// <param name="destroyOnDeath"></param>
    /// <returns></returns>
    public static LiveMixinData CreateLiveMixinData(float maxHealth, bool weldable = false, bool knifeable = true, bool destroyOnDeath = false)
    {
        var data = ScriptableObject.CreateInstance<LiveMixinData>();
        data.maxHealth = maxHealth;
        data.weldable = weldable;
        data.knifeable = knifeable;
        data.destroyOnDeath = destroyOnDeath;
        return data;
    }

    /// <summary>
    /// Creates an instance of the <see cref="WaterParkCreatureData"/> ScriptableObject.
    /// </summary>
    /// <param name="eggOrChildPrefab">IDFK</param>
    /// <param name="initialSize"></param>
    /// <param name="maxSize"></param>
    /// <param name="outsideSize"></param>
    /// <param name="daysToGrow"></param>
    /// <param name="isPickupableOutside"></param>
    /// <param name="canBreed"></param>
    /// <returns></returns>
    public static WaterParkCreatureData CreateWaterParkCreatureData(TechType eggOrChildPrefab, float initialSize = 0.1f, float maxSize = 0.6f, float outsideSize = 1f, float daysToGrow = 1f, bool isPickupableOutside = true, bool canBreed = true)
    {
        var data = ScriptableObject.CreateInstance<WaterParkCreatureData>();
        ECCPlugin.logger.LogError("PLEASE IMPLEMENT WATER PARK FUNCTIONALITY NOW!!!");
        return data;
    }

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
}
