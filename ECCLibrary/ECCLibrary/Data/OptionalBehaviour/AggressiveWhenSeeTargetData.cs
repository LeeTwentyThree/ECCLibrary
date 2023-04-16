namespace ECCLibrary.Data;

/// <summary>
/// Contains data pertaining to the <see cref="AggressiveWhenSeeTarget"/> component, which enables the creature to become aggressive towards specific fauna/the player.
/// </summary>
public class AggressiveWhenSeeTargetData
{
    /// <summary>
    /// The targeted EcoTargetType of this specific component. Every creature can have multiple <see cref="AggressiveWhenSeeTarget"/> components. Remember, the Player is <see cref="EcoTargetType.Shark"/>!
    /// </summary>
    public EcoTargetType targetType;
    /// <summary>
    /// The amount of the aggression trait added each second while targeting a creature. Generally 1f-2f.
    /// </summary>
    public float aggressionPerSecond;
    /// <summary>
    /// Creatures beyond this distance from the creature will not be targeted. Ranges from 15f for smaller predators to 150f for the Ghost Leviathan, however larger values are allowed.
    /// </summary>
    public float maxRangeScalar;
    /// <summary>
    /// An arbitrary distance scale that influences how far creatures can be targeted from. Typically 1, but should be larger (3+) if the maxRangeScalar is higher.
    /// </summary>
    public int maxSearchRings;
    /// <summary>
    /// If false, this creature will attack other creatures with the same TechType.
    /// </summary>
    public bool ignoreSameKind;
    /// <summary>
    /// Only applicable for creatures similar to the Warper.
    /// </summary>
    public bool targetShouldBeInfected;
    /// <summary>
    /// The minimum velocity required to see a target. Generally 0f and therefore unset, but the Crashfish for example has this value at 0.2f.
    /// </summary>
    public float minimumVelocity;
    /// <summary>
    /// Minimum amount of hunger needed to become aggressive to a target. Generally 0f, and rarely exceeds 0.1f unless the creature actually can eat it. In that case use a value around 0.8f.
    /// </summary>
    public float hungerThreshold;

    /// <summary>
    /// Contains data pertaining to the <see cref="AggressiveWhenSeeTarget"/> component, which enables the creature to become aggressive towards specific fauna/the player.
    /// </summary>
    /// <param name="targetType">The targeted EcoTargetType of this specific component. Every creature can have multiple <see cref="AggressiveWhenSeeTarget"/> components. Remember, the Player is <see cref="EcoTargetType.Shark"/>!</param>
    /// <param name="aggressionPerSecond">The amount of the aggression trait added each second while targeting a creature. Generally 1f-2f.</param>
    /// <param name="maxRangeScalar">Creatures beyond this distance from the creature will not be targeted. Ranges from 15f for smaller predators to 150f for the Ghost Leviathan, however larger values are allowed.</param>
    /// <param name="maxSearchRings">An arbitrary distance scale that influences how far creatures can be targeted from. Typically 1, but should be larger (3+) if the maxRangeScalar is higher.</param>
    /// <param name="ignoreSameKind">If false, this creature will attack other creatures with the same TechType.</param>
    /// <param name="targetShouldBeInfected">Only applicable for creatures similar to the Warper.</param>
    /// <param name="hungerThreshold">Minimum amount of hunger needed to become aggressive to a target. Generally 0f, and rarely exceeds 0.1f unless the creature actually can eat it. In that case use a value around 0.8f.</param>
    /// <param name="minimumVelocity">The minimum velocity required to see a target. Generally 0f and therefore unset, but the Crashfish for example has this value at 0.2f.</param>
    public AggressiveWhenSeeTargetData(EcoTargetType targetType, float aggressionPerSecond, float maxRangeScalar, int maxSearchRings, bool ignoreSameKind = true, bool targetShouldBeInfected = false, float hungerThreshold = 0f, float minimumVelocity = 0f)
    {
        this.targetType = targetType;
        this.aggressionPerSecond = aggressionPerSecond;
        this.maxRangeScalar = maxRangeScalar;
        this.maxSearchRings = maxSearchRings;
        this.ignoreSameKind = ignoreSameKind;
        this.targetShouldBeInfected = targetShouldBeInfected;
        this.minimumVelocity = minimumVelocity;
        this.hungerThreshold = hungerThreshold;
    }
}