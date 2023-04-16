namespace ECCLibrary.Data;

/// <summary>
/// Contains data pertaining to the <see cref="Scareable"/> component. This component is what enables small fish to swim away from the player and potential predators.
/// </summary>
public class ScareableData
{
    /// <summary>
    /// The target type that scares this creature.
    /// </summary>
    public EcoTargetType targetType;
    /// <summary>
    /// How fast this creature gets scared per second (maximum "fear" is 1f).
    /// </summary>
    public float scarePerSecond;
    /// <summary>
    /// How far this creature can get scared.
    /// </summary>
    public float maxRangeScalar;
    /// <summary>
    /// A creature must have this much mass or more to evoke fear.
    /// </summary>
    public float minMass;
    /// <summary>
    /// Every <see cref="updateTargetInterval"/> seconds, this creature will scan the area looking for things to be scared of.
    /// </summary>
    public float updateTargetInterval;
    /// <summary>
    /// The creature will only be afraid if within this many meters of the player object.
    /// </summary>
    public float updateRange;

    /// <summary>
    /// Contains data pertaining to the <see cref="Scareable"/> component. This component is what enables small fish to swim away from the player and potential predators.
    /// </summary>
    /// <param name="targetType">The target type that scares this creature.</param>
    /// <param name="scarePerSecond">How fast this creature gets scared per second (maximum "fear" is 1f).</param>
    /// <param name="maxRangeScalar">How far this creature can get scared.</param>
    /// <param name="minMass">A creature must have this much mass or more to evoke fear.</param>
    /// <param name="updateTargetInterval">Every <see cref="updateTargetInterval"/> seconds, this creature will scan the area looking for things to be scared of.</param>
    /// <param name="updateRange">The creature will only be afraid if within this many meters of the player object.</param>
    public ScareableData(EcoTargetType targetType = EcoTargetType.Shark, float scarePerSecond = 4f, float maxRangeScalar = 10f, float minMass = 50f, float updateTargetInterval = 1f, float updateRange = 100f)
    {
        this.targetType = targetType;
        this.scarePerSecond = scarePerSecond;
        this.maxRangeScalar = maxRangeScalar;
        this.minMass = minMass;
        this.updateTargetInterval = updateTargetInterval;
        this.updateRange = updateRange;
    }
}
