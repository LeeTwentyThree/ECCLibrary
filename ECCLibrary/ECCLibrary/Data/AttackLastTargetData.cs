namespace ECCLibrary.Data;

/// <summary>
/// Contains data pertaining to creating the <see cref="AttackLastTarget"/> CreatureAction.
/// </summary>
public class AttackLastTargetData
{
    /// <summary>
    /// The priority for this <see cref="CreatureAction"/>, expected to be in the range [0, 1].
    /// </summary>
    public float evaluatePriority;
    /// <summary>
    /// How fast the creature swims while attacking.
    /// </summary>
    public float swimVelocity;
    /// <summary>
    /// Minimum amount of aggression needed to start an attack, generally 0.5f - 0.75f.
    /// </summary>
    public float aggressionThreshold;
    /// <summary>
    /// Maximum length of the attack. Generally ranges from 7 to 10 seconds.
    /// </summary>
    public float maxAttackDuration;
    /// <summary>
    /// Minimum number of seconds between attacks. Generally ranges from 10 to 20 seconds.
    /// </summary>
    public float pauseInterval;
    /// <summary>
    /// Minimum length of the attack. Almost always 3 seconds.
    /// </summary>
    public float minAttackDuration;
    /// <summary>
    /// How long the <see cref="LastTarget.target"/> can be recognized for after it was last set. For most creatures, this is only 5 seconds. Does not influence how long the attack will be.
    /// </summary>
    public float rememberTargetTime;
    /// <summary>
    /// Almost always true, meaning the creature is no longer aggressive after the time is up.
    /// </summary>
    public bool resetAggressionOnTime;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="evaluatePriority">The priority for this <see cref="CreatureAction"/>, expected to be in the range [0, 1].</param>
    /// <param name="swimVelocity">How fast the creature swims while attacking.</param>
    /// <param name="aggressionThreshold">Minimum amount of aggression needed to start an attack, generally 0.5f - 0.75f.</param>
    /// <param name="maxAttackDuration">Maximum length of the attack. Generally ranges from 7 to 10 seconds.</param>
    /// <param name="pauseInterval">Minimum number of seconds between attacks. Generally ranges from 10 to 20 seconds.</param>
    /// <param name="minAttackDuration">Minimum length of the attack. Almost always 3 seconds.</param>
    /// <param name="rememberTargetTime">How long the <see cref="LastTarget.target"/> can be recognized for after it was last set. For most creatures, this is only 5 seconds. Does not influence how long the attack will be.</param>
    /// <param name="resetAggressionOnTime">Almost always true, meaning the creature is no longer aggressive after the time is up.</param>
    public AttackLastTargetData(float evaluatePriority, float swimVelocity, float aggressionThreshold, float maxAttackDuration, float pauseInterval = 20f, float minAttackDuration = 3f, float rememberTargetTime = 5f, bool resetAggressionOnTime = true)
    {
        this.evaluatePriority = evaluatePriority;
        this.swimVelocity = swimVelocity;
        this.aggressionThreshold = aggressionThreshold;
        this.maxAttackDuration = maxAttackDuration;
        this.pauseInterval = pauseInterval;
        this.minAttackDuration = minAttackDuration;
        this.rememberTargetTime = rememberTargetTime;
        this.resetAggressionOnTime = resetAggressionOnTime;
    }
}
