namespace ECCLibrary.Data;

/// <summary>
/// Contains data pertaining to creating the <see cref="FleeOnDamage"/> CreatureAction.
/// </summary>
public class FleeOnDamageData
{
    /// <summary>
    /// The priority for this <see cref="CreatureAction"/>, expected to be in the range [0, 1].
    /// </summary>
    public float evaluatePriority;
    /// <summary>
    /// How much damage must be taken before the creature will begin to flee.
    /// </summary>
    public float damageThreshold;
    /// <summary>
    /// How long the creature flees for.
    /// </summary>
    public float fleeDuration;
    /// <summary>
    /// At least how far in meters the creature will flee when attacked.
    /// </summary>
    public float minFleeDistance;
    /// <summary>
    /// How fast the creature will flee in m/s.
    /// </summary>
    public float swimVelocity;
    /// <summary>
    /// How many seconds are between each "swim".
    /// </summary>
    public float swimInterval;

    /// <summary>
    /// Contains data pertaining to creating the <see cref="FleeOnDamage"/> CreatureAction.
    /// </summary>
    /// <param name="evaluatePriority">The priority for this <see cref="CreatureAction"/>, expected to be in the range [0, 1].</param>
    /// <param name="damageThreshold">How much damage must be taken before the creature will begin to flee.</param>
    /// <param name="fleeDuration">How long the creature flees for.</param>
    /// <param name="minFleeDistance">At least how far in meters the creature will flee when attacked.</param>
    /// <param name="swimVelocity">How fast the creature will flee in m/s.</param>
    /// <param name="swimInterval">How many seconds are between each "swim".</param>
    public FleeOnDamageData(float evaluatePriority, float damageThreshold = 10f, float fleeDuration = 2f, float minFleeDistance = 5f, float swimVelocity = 10f, float swimInterval = 1f)
    {
        this.evaluatePriority = evaluatePriority;
        this.damageThreshold = damageThreshold;
        this.fleeDuration = fleeDuration;
        this.minFleeDistance = minFleeDistance;
        this.swimVelocity = swimVelocity;
        this.swimInterval = swimInterval;
    }
}
