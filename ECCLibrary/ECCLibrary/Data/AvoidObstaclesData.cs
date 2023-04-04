namespace ECCLibrary.Data;

/// <summary>
/// Contains data pertaining to creating the <see cref="AvoidObstacles"/> CreatureAction. This component is used by most creatures (everything besides leviathans) to avoid objects and/or terrain.
/// </summary>
public class AvoidObstaclesData
{
    /// <summary>
    /// The priority for this <see cref="CreatureAction"/>, expected to be in the range [0, 1].
    /// </summary>
    public float evaluatePriority;
    /// <summary>
    /// If true, the creature will only avoid terrain. Otherwise, all solid objects will be avoided. Typically true for larger creatures.
    /// </summary>
    public bool avoidTerrainOnly;
    /// <summary>
    /// How fast this creature swims away from obstacles (m/s). Should generally match the swim speed.
    /// </summary>
    public float swimVelocity;
    /// <summary>
    /// How much empty space there must be in any direction around the creature for it to choose that path as "clear". Typically 5f for small fish, up to 10f for larger fish. Large enough values may disable this behaviour entirely.
    /// </summary>
    public float avoidanceDistance;
    /// <summary>
    /// The action will only perform if there is an obstacle within this many meters in front of the creature (or around the creature if scanRadius > 0). Typically similar to the avoidance distance.
    /// </summary>
    public float scanDistance;
    /// <summary>
    /// How long this creature will continue to swim away from the terrain. Almost always equal to 2f.
    /// </summary>
    public float avoidanceDuration;
    /// <summary>
    /// How long between each "scan". Typically 1f but lower values may be needed for faster creatures.
    /// </summary>
    public float scanInterval;
    /// <summary>
    /// If equal to 0f (as done by most creatures), the creature will look for obstacles directly in front of it. Otherwise it will perform a SphereCast with this starting radius.
    /// </summary>
    public float scanRadius;
    /// <summary>
    /// The maximum number of times a random direction is evaluated to check for a clear path. If all "clear checks" fail, the action will be canceled and the creature will ignore the obstacle. Almost always 10.
    /// </summary>
    public int avoidanceIterations;

    /// <summary>
    /// Contains data pertaining to creating the <see cref="AvoidObstacles"/> CreatureAction. This component is used by most creatures (everything besides leviathans) to avoid objects and/or terrain.
    /// </summary>
    /// <param name="evaluatePriority">The priority for this <see cref="CreatureAction"/>, expected to be in the range [0, 1].</param>
    /// <param name="avoidTerrainOnly">If true, the creature will only avoid terrain. Otherwise, all solid objects will be avoided. Typically true for larger creatures.</param>
    /// <param name="swimVelocity">How fast this creature swims away from obstacles (m/s). Should generally match the swim speed.</param>
    /// <param name="avoidanceDistance">How much empty space there must be in any direction around the creature for it to choose that path as "clear". Typically 5f for small fish, up to 10f for larger fish. Large enough values may disable this behaviour entirely.</param>
    /// <param name="scanDistance">The action will only perform if there is an obstacle within this many meters in front of the creature (or around the creature if scanRadius > 0). Typically similar to the avoidance distance.</param>
    /// <param name="avoidanceDuration">How long this creature will continue to swim away from the terrain. Almost always equal to 2f.</param>
    /// <param name="scanInterval">How long between each "scan". Typically 1f but lower values may be needed for faster creatures.</param>
    /// <param name="scanRadius">If equal to 0f (as done by most creatures), the creature will look for obstacles directly in front of it. Otherwise it will perform a SphereCast with this starting radius.</param>
    /// <param name="avoidanceIterations">The maximum number of times a random direction is evaluated to check for a clear path. If all "clear checks" fail, the action will be canceled and the creature will ignore the obstacle. Almost always 10.</param>
    public AvoidObstaclesData(float evaluatePriority, float swimVelocity, bool avoidTerrainOnly, float avoidanceDistance, float scanDistance, float avoidanceDuration = 2f, float scanInterval = 1f, float scanRadius = 0f, int avoidanceIterations = 10)
    {
        this.evaluatePriority = evaluatePriority;
        this.avoidTerrainOnly = avoidTerrainOnly;
        this.swimVelocity = swimVelocity;
        this.avoidanceDistance = avoidanceDistance;
        this.scanDistance = scanDistance;
        this.avoidanceDuration = avoidanceDuration;
        this.scanInterval = scanInterval;
        this.scanRadius = scanRadius;
        this.avoidanceIterations = avoidanceIterations;
    }
}
