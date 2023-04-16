namespace ECCLibrary.Data;

/// <summary>
/// Contains data pertaining to creating the <see cref="StayAtLeashPosition"/> CreatureAction. This component keeps creatures from wandering too far.
/// </summary>
public class StayAtLeashData
{
    /// <summary>
    /// The priority for this <see cref="CreatureAction"/>, expected to be in the range [0, 1].
    /// </summary>
    public float evaluatePriority;
    /// <summary>
    /// How far the creature has to be from its spawn point to begin swimming back to it. For small and medium creatures usually has a value of 15-30 meters. Typically very high (upwards of 150m) for leviathans.
    /// </summary>
    public float leashDistance;
    /// <summary>
    /// How fast the creature swims back to its spawn point. Generally matches SwimRandom swim velocity, more or less.
    /// </summary>
    public float swimVelocity;
    /// <summary>
    /// The minimum amount of time between each "swim". Vanilla creatures always use a value of 1 second (there is very little reason to change this).
    /// </summary>
    public float swimInterval;
    /// <summary>
    /// How long the creature will continue to swim back to its spawn point during each "swim" (unless overriden by another action). Generally 3 seconds but sometimes (rarely) higher.
    /// </summary>
    public float minSwimDuration;

    /// <summary>
    /// Contains data pertaining to creating the <see cref="StayAtLeashPosition"/> CreatureAction.
    /// </summary>
    /// <param name="evaluatePriority">The priority for this <see cref="CreatureAction"/>, expected to be in the range [0, 1].</param>
    /// <param name="leashDistance">How far the creature has to be from its spawn point to begin swimming back to it. For small and medium creatures usually has a value of 15-30 meters. Typically very high (upwards of 150m) for leviathans.</param>
    /// <param name="swimVelocity">How fast the creature swims back to its spawn point. Generally matches SwimRandom swim velocity, more or less.</param>
    /// <param name="swimInterval">The minimum amount of time between each "swim". Vanilla creatures always use a value of 1 second (there is very little reason to change this).</param>
    /// <param name="minSwimDuration">How long the creature will continue to swim back to its spawn point during each "swim" (unless overriden by another action). Generally 3 seconds but sometimes (rarely) higher.</param>
    public StayAtLeashData(float evaluatePriority, float swimVelocity, float leashDistance, float swimInterval = 1f, float minSwimDuration = 3f)
    {
        this.evaluatePriority = evaluatePriority;
        this.leashDistance = leashDistance;
        this.swimVelocity = swimVelocity;
        this.swimInterval = swimInterval;
        this.minSwimDuration = minSwimDuration;
    }
}
