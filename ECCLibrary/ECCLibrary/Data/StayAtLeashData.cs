namespace ECCLibrary.Data;

/// <summary>
/// Contains data pertaining to creating the <see cref="StayAtLeashPosition"/> CreatureAction.
/// </summary>
public class StayAtLeashData
{
    /// <summary>
    /// The priority for this <see cref="CreatureAction"/>.
    /// </summary>
    public float evaluatePriority;
    /// <summary>
    /// How far the creature has to be from its spawn point to begin swiwmming back to it.
    /// </summary>
    public float leashDistance;
    /// <summary>
    /// How fast the creature swims back to its spawn point.
    /// </summary>
    public float swimVelocity;
    /// <summary>
    /// The minimum amount of time between each "swim".
    /// </summary>
    public float swimInterval;
    /// <summary>
    /// How long the creature will continue to swim back to its spawn point during each "swim" (unless overriden by another action).
    /// </summary>
    public float minSwimDuration;

    /// <summary>
    /// Contains data pertaining to creating the <see cref="StayAtLeashPosition"/> CreatureAction.
    /// </summary>
    /// <param name="evaluatePriority">The priority for this <see cref="CreatureAction"/>.</param>
    /// <param name="leashDistance">How far the creature has to be from its spawn point to begin swiwmming back to it.</param>
    /// <param name="swimVelocity">How fast the creature swims back to its spawn point.</param>
    /// <param name="swimInterval">The minimum amount of time between each "swim".</param>
    /// <param name="minSwimDuration">How long the creature will continue to swim back to its spawn point during each "swim" (unless overriden by another action).</param>
    public StayAtLeashData(float evaluatePriority, float leashDistance, float swimVelocity, float swimInterval = 1f, float minSwimDuration = 3f)
    {
        this.evaluatePriority = evaluatePriority;
        this.leashDistance = leashDistance;
        this.swimVelocity = swimVelocity;
        this.swimInterval = swimInterval;
        this.minSwimDuration = minSwimDuration;
    }
}
