namespace ECCLibrary;

/// <summary>
/// Contains basic data pertaining to the <see cref="CreatureTrait"/>s of a creature.
/// </summary>
public struct CreatureTraitsData
{
    /// <summary>
    /// The rate at which the creature gets hungrier. Predators often require higher levels of hunger to attack.
    /// </summary>
    public float HungerIncreaseRate;
    /// <summary>
    /// The rate at which this creature becomes passive while actively hunting.
    /// </summary>
    public float AggressionDecreaseRate;
    /// <summary>
    /// The rate at which this creature becomes less scared. Used in very specific circumstances, most notably when taking damage.
    /// </summary>
    public float ScaredDecreaseRate;

    /// <summary>
    /// Contains basic data pertaining to the <see cref="CreatureTrait"/>s of a creature.
    /// </summary>
    /// <param name="hungerIncreaseRate">The rate at which the creature gets hungrier. Predators often require higher levels of hunger to attack.</param>
    /// <param name="aggressionDecreaseRate">The rate at which this creature becomes passive while actively hunting.</param>
    /// <param name="scaredDecreaseRate">The rate at which this creature becomes less scared. Used in very specific circumstances, most notably when taking damage.</param>
    public CreatureTraitsData(float hungerIncreaseRate = 0.01f, float aggressionDecreaseRate = 0.05f, float scaredDecreaseRate = 0.1f)
    {
        HungerIncreaseRate = hungerIncreaseRate;
        AggressionDecreaseRate = aggressionDecreaseRate;
        ScaredDecreaseRate = scaredDecreaseRate;
    }
}