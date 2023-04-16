namespace ECCLibrary.Data;

/// <summary>
/// Contains data pertaining to creating the <see cref="AvoidTerrain"/> CreatureAction. This is a more advanced and expensive collision avoidance system used by leviathans.
/// </summary>
public class AvoidTerrainData
{
    /// <summary>
    /// The priority for this <see cref="CreatureAction"/>, expected to be in the range [0, 1].
    /// </summary>
    public float evaluatePriority;
    /// <summary>
    /// How fast this creature swims when intentionally avoiding terrain.
    /// </summary>
    public float swimVelocity;
    /// <summary>
    /// How many meters long an "empty space" is required to be, in any given direction. Typically about 30 meters.
    /// </summary>
    public float avoidanceDistance;
    /// <summary>
    /// Similar to avoidanceDistance, defines how many meters long an "empty space" is required to be, in any given direction. Typically uses the same value as avoidanceDistance and is used for the cheaper "initial check" that determines whether or not the actual avoidance takes place.
    /// </summary>
    public float scanDistance;
    /// <summary>
    /// Must be in the range [0, 1]. The higher this value, the more it tends to only focus on points in front of it. Generally 0.5f, but the Reaper Leviathan uses a value of 1f.
    /// </summary>
    public float avoidanceForward;
    /// <summary>
    /// The quality of object avoidance. Almost always seen as 10 (the default value).
    /// </summary>
    public float avoidanceIterations;

    /// <summary>
    /// Contains data pertaining to creating the <see cref="AvoidTerrain"/> CreatureAction. This is a more advanced and expensive collision avoidance system used by leviathans.
    /// </summary>
    /// <param name="evaluatePriority">The priority for this <see cref="CreatureAction"/>, expected to be in the range [0, 1].</param>
    /// <param name="swimVelocity">How fast this creature swims when intentionally avoiding terrain.</param>
    /// <param name="avoidanceDistance">How many meters long an "empty space" is required to be, in any given direction. Typically about 30 meters.</param>
    /// <param name="scanDistance">Similar to avoidanceDistance, defines how many meters long an "empty space" is required to be, in any given direction. Typically uses the same value as avoidanceDistance and is used for the cheaper "initial check" that determines whether or not the actual avoidance takes place.</param>
    /// <param name="avoidanceForward">Must be in the range [0, 1]. The higher this value, the more it tends to only focus on points in front of it. Generally 0.5f, but the Reaper Leviathan uses a value of 1f.</param>
    /// <param name="avoidanceIterations">The quality of object avoidance. Almost always seen as 10 (the default value).</param>
    public AvoidTerrainData(float evaluatePriority, float swimVelocity, float avoidanceDistance, float scanDistance, float avoidanceForward = 0.5f, float avoidanceIterations = 10f)
    {
        this.evaluatePriority = evaluatePriority;
        this.swimVelocity = swimVelocity;
        this.avoidanceDistance = avoidanceDistance;
        this.scanDistance = scanDistance;
        this.avoidanceForward = avoidanceForward;
        this.avoidanceIterations = avoidanceIterations;
    }
}
