namespace ECCLibrary.Data;

/// <summary>
/// Contains data pertaining to creating the <see cref="SwimRandom"/> CreatureAction.
/// </summary>
public class SwimRandomData
{
    /// <summary>
    /// The priority for this <see cref="CreatureAction"/>, expected to be in the range [0, 1].
    /// </summary>
    public float evaluatePriority;
    /// <summary>
    /// The distance this creature can wander in any direction every time the action is played. X and Z values should always be identical and are usually 10 - 30 meters. Typically the vertical (Y) range is only 20% - 25% of the horizontal range.
    /// </summary>
    public Vector3 swimRadius;
    /// <summary>
    /// Swim speed for this action (m/s). Typically 2-3 m/s for small fish, 4-8 m/s for medium fish and sharks, and 15-20 m/s for aggressive leviathans.
    /// </summary>
    public float swimVelocity;
    /// <summary>
    /// The time in seconds between each change in direction. Typically 5 seconds, but more sporadic creatures may have values as low as 2 seconds.
    /// </summary>
    public float swimInterval;
    /// <summary>
    /// In the vanilla game this value is typically set to 'true' for leviathans. If enabled, the creature will always swim towards a point at the maximum distance allowed by <see cref="swimRadius"/>. Otherwise, the distance will be random.
    /// </summary>
    public bool onSphere;
    /// <summary>
    /// The higher this value, the more this creature tends to continue swimming in the same direction. Default value is 0.5f. Typically higher (1.0f-1.2f) for larger creatures.
    /// </summary>
    public float swimForward;

    /// <summary>
    /// Contains data pertaining to creating the <see cref="SwimRandom"/> CreatureAction.
    /// </summary>
    /// <param name="evaluatePriority">The priority for this <see cref="CreatureAction"/>, expected to be in the range [0, 1].</param>
    /// <param name="swimRadius">The distance this creature can wander in any direction every time the action is played. X and Z values should always be identical and are usually 10 - 30 meters. Typically the vertical (Y) range is only 20% - 25% of the horizontal range.</param>
    /// <param name="swimVelocity">Swim speed for this action (m/s). Typically 2-3 m/s for small fish, 4-8 m/s for medium fish and sharks, and 15-20 m/s for aggressive leviathans.</param>
    /// <param name="swimInterval">The time in seconds between each change in direction. Typically 5 seconds, but more sporadic creatures may have values as low as 2 seconds.</param>
    /// <param name="onSphere">In the vanilla game this value is typically set to 'true' for leviathans. If enabled, the creature will always swim towards a point at the maximum distance allowed by <see cref="swimRadius"/>. Otherwise, the distance will be random.</param>
    /// <param name="swimForward">The higher this value, the more this creature tends to continue swimming in the same direction. Default value is 0.5f. Typically higher (1.0f-1.2f) for larger creatures.</param>
    public SwimRandomData(float evaluatePriority, float swimVelocity, Vector3 swimRadius, float swimInterval = 5f, float swimForward = 0.5f, bool onSphere = false)
    {
        this.swimRadius = swimRadius;
        this.swimVelocity = swimVelocity;
        this.swimInterval = swimInterval;
        this.evaluatePriority = evaluatePriority;
        this.onSphere = onSphere;
        this.swimForward = swimForward;
    }
}
