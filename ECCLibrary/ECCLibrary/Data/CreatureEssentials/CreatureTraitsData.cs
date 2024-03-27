namespace ECCLibrary.Data;

/// <summary>
/// Contains basic data pertaining to the <see cref="CreatureTrait"/>s of a creature.
/// </summary>
public struct CreatureTraitsData
{
    /// <summary>
    /// The rate at which the creature gets hungrier, per second. Predators often require higher levels of hunger to attack. A typical value for most predators is 0.01.
    /// </summary>
    public float hungerIncreaseRate;
    /// <summary>
    /// The rate at which this creature becomes passive while actively hunting, per second. A typical value for most predators is 0.05, around 0.1 if less aggressive.
    /// </summary>
    public float aggressionDecreaseRate;
    /// <summary>
    /// The rate at which this creature becomes less scared, per second. Used in very specific circumstances, most notably when taking damage.
    /// </summary>
    public float scaredDecreaseRate;

    /// <summary>
    /// Contains basic data pertaining to the <see cref="CreatureTrait"/>s of a creature.
    /// </summary>
    /// <param name="hungerIncreaseRate">The rate at which the creature gets hungrier, per second. Predators often require higher levels of hunger to attack. A typical value for most predators is 0.01.</param>
    /// <param name="aggressionDecreaseRate">The rate at which this creature becomes passive while actively hunting, per second. A typical value for most predators is 0.05, around 0.1 if less aggressive.</param>
    /// <param name="scaredDecreaseRate">The rate at which this creature becomes less scared, per second. Used in very specific circumstances, most notably when taking damage.</param>
    public CreatureTraitsData(float hungerIncreaseRate = 0.01f, float aggressionDecreaseRate = 0.05f, float scaredDecreaseRate = 0.1f)
    {
        this.hungerIncreaseRate = hungerIncreaseRate;
        this.aggressionDecreaseRate = aggressionDecreaseRate;
        this.scaredDecreaseRate = scaredDecreaseRate;
    }
}