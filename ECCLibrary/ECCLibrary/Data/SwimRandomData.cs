namespace ECCLibrary.Data;

/// <summary>
/// Contains data pertaining to creating the <see cref="SwimRandom"/> CreatureAction.
/// </summary>
public class SwimRandomData
{
    /// <summary>
    /// The distance this creature wanders in each direction, every time the action is played.
    /// </summary>
    public Vector3 swimRadius;
    /// <summary>
    /// Swim speed for this action (m/s).
    /// </summary>
    public float swimVelocity;
    /// <summary>
    /// The time in seconds between each change in direction.
    /// </summary>
    public float swimInterval;
    /// <summary>
    /// The priority for this <see cref="CreatureAction"/>.
    /// </summary>
    public float evaluatePriority;
    /// <summary>
    /// If enabled, the creature will always swim to a point at the maximum distance from itself (as defined by <see cref="swimRadius"/>). Otherwise, the distance will be random.
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
    /// <param name="swimRadius">The distance this creature wanders in each direction, every time the action is played.</param>
    /// <param name="swimVelocity">Swim speed for this action (m/s).</param>
    /// <param name="swimInterval">The time in seconds between each change in direction.</param>
    /// <param name="onSphere">If enabled, the creature will always swim to a point at the maximum distance from itself (as defined by <see cref="swimRadius"/>). Otherwise, the distance will be random.</param>
    /// <param name="swimForward">The higher this value, the more this creature tends to continue swimming in the same direction. Default value is 0.5f. Typically higher (1.0f-1.2f) for larger creatures.</param>
    public SwimRandomData(float evaluatePriority, Vector3 swimRadius, float swimVelocity, float swimInterval = 5f, float swimForward = 0.5f, bool onSphere = false)
    {
        this.swimRadius = swimRadius;
        this.swimVelocity = swimVelocity;
        this.swimInterval = swimInterval;
        this.evaluatePriority = evaluatePriority;
        this.onSphere = onSphere;
        this.swimForward = swimForward;
    }
}
