namespace ECCLibrary.Data;

/// <summary>
/// <para>Contains data pertaining to adding the <see cref="SwimInSchool"/> CreatureAction.</para>
/// <para>Each schooling creature chooses a single "leader" larger than itself (and of the same TechType) to follow. Therefore, the <see cref="CreatureTemplate.SizeDistribution"/> property should be defined for this action to function properly.</para>
/// </summary>
public class SwimInSchoolData
{
    /// <summary>
    /// The priority for this <see cref="CreatureAction"/>, expected to be in the range [0, 1].
    /// </summary>
    public float evaluatePriority;
    /// <summary>
    /// Swim speed for this action (m/s). Should match speed defined in the <see cref="SwimRandomData"/>. Typically 2-3 m/s for small fish, 4-8 m/s for medium fish and sharks, and 15-20 m/s for aggressive leviathans.
    /// </summary>
    public float swimVelocity;
    /// <summary>
    /// Maximum distance this fish will swim away from its "leader". Default value is 2 meters.
    /// </summary>
    public float schoolSize;
    /// <summary>
    /// Number of seconds between each time the creature repositions itself. Default value is 1 second.
    /// </summary>
    public float swimInterval;
    /// <summary>
    /// If the creature leaves this radius (in meters) of its leader, it will stop following. Default value is 20 meters.
    /// </summary>
    public float breakDistance;
    /// <summary>
    /// <para>Value with expected range of [0.0, 1.0]. Default value is 0.5f.</para>
    /// <para>Every 2 seconds, a schooling creature checks if it should begin schooling. This value determines the chance of forming a school. A value of 0f means it will never school, while 1f means it always will attempt.</para>
    /// </summary>
    public float percentFindLeaderRespond;
    /// <summary>
    /// <para>Value with expected range of [0.0, 1.0]. Default value is 0.1f.</para>
    /// <para>Every 2 seconds, a schooling creature checks if it should stop schooling. This value determines the chance of breaking off. A value of 0f means it will never break off, while 1f disables the behaviour.</para>
    /// </summary>
    public float chanceLoseLeader;

    /// <summary>
    /// <para>Contains data pertaining to adding the <see cref="SwimInSchool"/> CreatureAction.</para>
    /// <para>Each schooling creature chooses a single "leader" larger than itself (and of the same TechType) to follow. Therefore, the <see cref="CreatureTemplate.SizeDistribution"/> property should be defined for this action to function properly.</para>
    /// </summary>
    /// <param name="evaluatePriority">The priority for this <see cref="CreatureAction"/>, expected to be in the range [0, 1].</param>
    /// <param name="swimVelocity">Swim speed for this action (m/s). Should match speed defined in the <see cref="SwimRandomData"/>. Typically 2-3 m/s for small fish, 4-8 m/s for medium fish and sharks, and 15-20 m/s for aggressive leviathans.</param>
    /// <param name="schoolSize">Maximum distance this fish will swim away from its "leader". Default value is 2 meters.</param>
    /// <param name="swimInterval">Number of seconds between each time the creature repositions itself. Default value is 1 second.</param>
    /// <param name="breakDistance">If the creature leaves this radius (in meters) of its leader, it will stop following. Default value is 20 meters.</param>
    /// <param name="percentFindLeaderRespond">
    /// <para>Value with expected range of [0.0, 1.0]. Default value is 0.5f.</para>
    /// <para>Every 2 seconds, a schooling creature checks if it should begin schooling. This value determines the chance of forming a school. A value of 0f means it will never school, while 1f means it always will attempt.</para>
    /// </param>
    /// <param name="chanceLoseLeader">
    /// /// <para>Value with expected range of [0.0, 1.0]. Default value is 0.1f.</para>
    /// <para>Every 2 seconds, a schooling creature checks if it should stop schooling. This value determines the chance of breaking off. A value of 0f means it will never break off, while 1f disables the behaviour.</para>
    /// </param>
    public SwimInSchoolData(float evaluatePriority, float swimVelocity, float schoolSize, float swimInterval, float percentFindLeaderRespond = 0.5f, float chanceLoseLeader = 0.1f, float breakDistance = 20f)
    {
        this.evaluatePriority = evaluatePriority;
        this.swimVelocity = swimVelocity;
        this.schoolSize = schoolSize;
        this.swimInterval = swimInterval;
        this.breakDistance = breakDistance;
        this.percentFindLeaderRespond = percentFindLeaderRespond;
        this.chanceLoseLeader = chanceLoseLeader;
    }
}
