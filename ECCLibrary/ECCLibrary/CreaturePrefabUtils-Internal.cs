using ECCLibrary.Data;

namespace ECCLibrary;

/// <summary>
/// Utilities related to creating a creature prefab object.
/// </summary>
public static partial class CreaturePrefabUtils
{
    internal static SwimBehaviour AddSwimBehaviour(GameObject creature, SwimBehaviourData data, SplineFollowing splineFollowing)
    {
        var component = creature.EnsureComponent<SwimBehaviour>();
        component.turnSpeed = data.turnSpeed;
        component.splineFollowing = splineFollowing;
        return component;
    }

    internal static SwimRandom AddSwimRandom(GameObject creature, SwimRandomData data)
    {
        var component = creature.EnsureComponent<SwimRandom>();
        component.swimRadius = data.swimRadius;
        component.swimForward = data.swimForward;
        component.swimVelocity = data.swimVelocity;
        component.swimInterval = data.swimInterval;
        component.onSphere = data.onSphere;
        return component;
    }

    internal static Locomotion AddLocomotion(GameObject creature, LocomotionData data, BehaviourLOD levelOfDetail, Rigidbody rigidbody)
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

    internal static SplineFollowing AddSplineFollowing(GameObject creature, Rigidbody rb, Locomotion locomotion, BehaviourLOD behaviourLOD)
    {
        var component = creature.EnsureComponent<SplineFollowing>();
        component.useRigidbody = rb;
        component.locomotion = locomotion;
        component.levelOfDetail = behaviourLOD;
        return component;

    }

    internal static LiveMixin AddLiveMixin(GameObject creature, LiveMixinData data)
    {
        var component = creature.EnsureComponent<LiveMixin>();
        component.data = data;
        return component;
    }

    internal static StayAtLeashPosition AddStayAtLeashPosition(GameObject creature, StayAtLeashData stayAtLeashData)
    {
        var component = creature.EnsureComponent<StayAtLeashPosition>();
        component.evaluatePriority = stayAtLeashData.evaluatePriority;
        component.leashDistance = stayAtLeashData.leashDistance;
        component.swimVelocity = stayAtLeashData.swimVelocity;
        component.swimInterval = stayAtLeashData.swimInterval;
        component.minSwimDuration = stayAtLeashData.minSwimDuration;
        return component;

    }
}