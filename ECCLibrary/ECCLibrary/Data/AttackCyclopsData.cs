namespace ECCLibrary.Data;

/// <summary>
/// Contains data pertaining to creating the <see cref="AttackCyclops"/> CreatureAction.
/// </summary>
public class AttackCyclopsData
{
    /// <summary>
    /// The priority for this <see cref="CreatureAction"/>, expected to be in the range [0, 1].
    /// </summary>
    public float evaluatePriority;
    /// <summary>
    /// Swim speed when swimming toward a Cyclops.
    /// </summary>
    public float swimVelocity;
    /// <summary>
    /// If the Cyclops is this many meters or more away from the creature's spawn point (leash position), it will be ignored. Generally has a value of 100f or more.
    /// </summary>
    public float maxDistToLeash;
    /// <summary>
    /// The amount of aggression gained per second while aware of a Cyclops. Note that this aggression is linked to '<see cref="AttackCyclops.aggressiveToNoise"/>', NOT '<see cref="Creature.Aggression"/>'.
    /// <br/>Generally 0.2f for smaller creatures and 0.4f for a leviathan.
    /// </summary>
    public float aggressPerSecond;
    /// <summary>
    /// Minimum amount of time between each "attack". Generally 6 seconds for smaller creatures and 3 seconds for a leviathan.
    /// </summary>
    public float attackPause;
    /// <summary>
    /// How fast the aggression to noise decreases. Typically 0.08f for smaller creatures and 0.01f for leviathans.
    /// </summary>
    public float aggressionFalloff;
    /// <summary>
    /// Minimum aggression to attack. All vanilla creatures use a value of 0.75f.
    /// </summary>
    public float attackAggressionThreshold;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="evaluatePriority">The priority for this <see cref="CreatureAction"/>, expected to be in the range [0, 1].</param>
    /// <param name="swimVelocity">Swim speed when swimming toward a Cyclops.</param>
    /// <param name="maxDistToLeash">If the Cyclops is this many meters or more away from the creature's spawn point (leash position), it will be ignored. Generally has a value of 100f or more.</param>
    /// <param name="aggressPerSecond">The amount of aggression gained per second while aware of a Cyclops. Note that this aggression is linked to '<see cref="AttackCyclops.aggressiveToNoise"/>', NOT '<see cref="Creature.Aggression"/>'.
    /// <br/>Generally 0.2f for smaller creatures and 0.4f for a leviathan.</param>
    /// <param name="attackPause">Minimum amount of time between each "attack". Generally 6 seconds for smaller creatures and 3 seconds for a leviathan.</param>
    /// <param name="aggressionFalloff">How fast the aggression to noise decreases. Typically 0.08f for smaller creatures and 0.01f for leviathans.</param>
    /// <param name="attackAggressionThreshold">Minimum aggression to attack. All vanilla creatures use a value of 0.75f.</param>
    public AttackCyclopsData(float evaluatePriority, float swimVelocity, float maxDistToLeash, float aggressPerSecond, float attackPause, float aggressionFalloff, float attackAggressionThreshold = 0.75f)
    {
        this.evaluatePriority = evaluatePriority;
        this.swimVelocity = swimVelocity;
        this.maxDistToLeash = maxDistToLeash;
        this.aggressPerSecond = aggressPerSecond;
        this.attackPause = attackPause;
        this.aggressionFalloff = aggressionFalloff;
        this.attackAggressionThreshold = attackAggressionThreshold;
    }
}