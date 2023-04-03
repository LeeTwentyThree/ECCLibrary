namespace ECCLibrary.Data;

/// <summary>
/// Contains data pertaining to creating the <see cref="SwimBehaviour"/> component.
/// </summary>
public class SwimBehaviourData
{
    /// <summary>
    /// A less useful "turn speed" property, which vaguely determines the speed at which a creature can turn fully around. If in doubt, leave at its default of value of 1f.
    /// </summary>
    public float turnSpeed;

    /// <summary>
    /// Contains data pertaining to creating the <see cref="SwimBehaviour"/> component.
    /// </summary>
    /// <param name="turnSpeed">A less useful "turn speed" property, which vaguely determines the speed at which a creature can turn fully around. If in doubt, leave at its default of value of 1f.</param>
    public SwimBehaviourData(float turnSpeed = 1f)
    {
        this.turnSpeed = turnSpeed;
    }
}
