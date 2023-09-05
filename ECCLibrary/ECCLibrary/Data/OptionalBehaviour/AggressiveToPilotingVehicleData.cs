namespace ECCLibrary.Data;

/// <summary>
/// Contains data pertaining to the <see cref="AggressiveToPilotingVehicle"/> component, which encourages creatures to target any small vehicle that the player may be piloting
/// (this includes ANY vehicle that inherits from the <see cref="Vehicle"/> component i.e. the Seamoth or Prawn Suit). Not many creatures use this component, but ones that do
/// will be VERY aggressive (Boneshark levels of aggression!).
/// </summary>
public class AggressiveToPilotingVehicleData
{
    /// <summary>
    /// The maximum range in meters from which the vehicle is noticed and targeted. Any piloted vehicle in this range WILL be targeted.
    /// Values around 40m are typical, but anything is valid.
    /// </summary>
    public float range;

    /// <summary>
    /// The aggression that is added to this creature every second that the vehicle is in range. Default value is 0.5, which is quite high.
    /// </summary>
    public float aggressionPerSecond;

    /// <summary>
    /// The amount of time between each distance check in seconds. This value is typically one second and there is VERY little reason to change this.
    /// </summary>
    public float updateAggressionInterval;

    /// <summary>
    /// Contains data pertaining to the <see cref="AggressiveToPilotingVehicle"/> component, which encourages creatures to target any small vehicle that the player may be piloting
    /// (this includes ANY vehicle that inherits from the <see cref="Vehicle"/> component i.e. the Seamoth or Prawn Suit). Not many creatures use this component, but ones that do
    /// will be VERY aggressive (Boneshark levels of aggression!).
    /// </summary>
    /// <param name="range">The maximum range in meters from which the vehicle is noticed and targeted. Any piloted vehicle in this range WILL be targeted.
    /// Values around 40m are typical, but anything is valid.</param>
    /// <param name="aggressionPerSecond">The aggression that is added to this creature every second that the vehicle is in range. Default value is 0.5, which is quite high.</param>
    /// <param name="updateAggressionInterval">The amount of time between each distance check in seconds. This value is typically one second and there is VERY little reason to change this.</param>
    public AggressiveToPilotingVehicleData(float range, float aggressionPerSecond, float updateAggressionInterval = 1f)
    {
        this.range = range;
        this.aggressionPerSecond = aggressionPerSecond;
        this.updateAggressionInterval = updateAggressionInterval;
    }
}
