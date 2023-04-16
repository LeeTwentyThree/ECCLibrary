namespace ECCLibrary.Data;

/// <summary>
/// Contains data pertaining to creating the <see cref="FleeWhenScared"/> CreatureAction.
/// </summary>
public class FleeWhenScaredData
{
    /// <summary>
    /// The priority for this <see cref="CreatureAction"/>, expected to be in the range [0, 1].
    /// </summary>
    public float evaluatePriority;
    /// <summary>
    /// Default swim speed when swimming.
    /// </summary>
    public float swimVelocity;
    /// <summary>
    /// Interval between swimming.
    /// </summary>
    public float swimInterval;
    /// <summary>
    /// The quality of the terrain avoidance.
    /// </summary>
    public int avoidanceIterations;
    /// <summary>
    /// How tired this creature gets while fleeing, each second.
    /// </summary>
    public float swimTiredness;
    /// <summary>
    /// The velocity when tired.
    /// </summary>
    public float tiredVelocity;
    /// <summary>
    /// How exhausted the creature gets while fleeing, each second.
    /// </summary>
    public float swimExhaustion;
    /// <summary>
    /// The velocity when exhausted (after fleeing too long).
    /// </summary>
    public float exhaustedVelocity;

    /// <summary>
    /// Contains data pertaining to creating the <see cref="FleeWhenScared"/> CreatureAction.
    /// </summary>
    /// <param name="evaluatePriority">The priority for this <see cref="CreatureAction"/>, expected to be in the range [0, 1].</param>
    /// <param name="swimVelocity">Default swim speed when swimming.</param>
    /// <param name="swimInterval">Interval between swimming.</param>
    /// <param name="avoidanceIterations">The quality of the terrain avoidance.</param>
    /// <param name="swimTiredness">How tired this creature gets while fleeing, each second.</param>
    /// <param name="tiredVelocity">The velocity when tired.</param>
    /// <param name="swimExhaustion">How exhausted the creature gets while fleeing, each second.</param>
    /// <param name="exhaustedVelocity">The velocity when exhausted (after fleeing too long).</param>
    public FleeWhenScaredData(float evaluatePriority, float swimVelocity, float swimInterval = 1f, int avoidanceIterations = 10, float swimTiredness = 0.2f, float tiredVelocity = 3f, float swimExhaustion = 0.25f, float exhaustedVelocity = 1f)
    {
        this.evaluatePriority = evaluatePriority;
        this.swimVelocity = swimVelocity;
        this.swimInterval = swimInterval;
        this.avoidanceIterations = avoidanceIterations;
        this.swimTiredness = swimTiredness;
        this.tiredVelocity = tiredVelocity;
        this.swimExhaustion = swimExhaustion;
        this.exhaustedVelocity = exhaustedVelocity;
    }
}