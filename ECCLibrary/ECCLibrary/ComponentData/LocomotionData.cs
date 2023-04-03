namespace ECCLibrary;

/// <summary>
/// Contains data pertaining to creating the <see cref="Locomotion"/> component.
/// </summary>
public class LocomotionData
{
    /// <summary>
    /// How fast this creature accelerates while swimming in m/s/s. A value of 10f is used for most creatures, but 12f is used for the Reaper Leviathan &#38; Sea Dragon.
    /// </summary>
    public float maxAcceleration;
    /// <summary>
    /// Rotation speed when turning left/right. Generally has a value of 0.6f. Sometimes (but not always) has smaller values with larger creatures.
    /// </summary>
    public float forwardRotationSpeed;
    /// <summary>
    /// Rotation speed when turning up/down. Generally has a value of 3f, with few exceptions.
    /// </summary>
    public float upRotationSpeed;
    /// <summary>
    /// How much this creature tends to "drift", in the range [0f, 1.0f]. Most common value is 0.5f. Generally values (0.3f~) are used for smaller creatures and varies for leviathans.
    /// </summary>
    public float driftFactor;
    /// <summary>
    /// If true, this creature will not be able to look upwards/downwards (rotation about X axis will always be 0).
    /// </summary>
    public bool freezeHorizontalRotation;
    /// <summary>
    /// If enabled, the creature can move (swim and/or walk) above water.
    /// </summary>
    public bool canMoveAboveWater;
    /// <summary>
    /// Typically enabled for most land creatures.
    /// </summary>
    public bool canWalkOnSurface;

    /// <summary>
    /// Contains data pertaining to creating the <see cref="Locomotion"/> component.
    /// </summary>
    /// <param name="maxAcceleration">How fast this creature accelerates while swimming in m/s/s. A value of 10f is used for most creatures, but 12f is used for the Reaper Leviathan &#38; Sea Dragon.</param>
    /// <param name="forwardRotationSpeed">Rotation speed when turning left/right. Generally has a value of 0.6f. Sometimes (but not always) has smaller values with larger creatures.</param>
    /// <param name="upRotationSpeed">Rotation speed when turning up/down. Generally has a value of 3f, with few exceptions.</param>
    /// <param name="driftFactor">How much this creature tends to "drift", in the range [0f, 1.0f]. Most common value is 0.5f. Generally lower values (0.3f~) are used for smaller creatures and varies for leviathans</param>
    /// <param name="freezeHorizontalRotation">If true, this creature will not be able to look upwards/downwards (rotation about X axis will always be 0).</param>
    /// <param name="canMoveAboveWater">If enabled, the creature can move (swim and/or walk) above water.</param>
    /// <param name="canWalkOnSurface">Typically enabled for most land creatures.</param>
    public LocomotionData(float maxAcceleration = 10f, float forwardRotationSpeed = 0.6f, float upRotationSpeed = 3f, float driftFactor = 0.5f, bool freezeHorizontalRotation = false, bool canMoveAboveWater = false, bool canWalkOnSurface = false)
    {
        this.maxAcceleration = maxAcceleration;
        this.forwardRotationSpeed = forwardRotationSpeed;
        this.upRotationSpeed = upRotationSpeed;
        this.driftFactor = driftFactor;
        this.freezeHorizontalRotation = freezeHorizontalRotation;
        this.canMoveAboveWater = canMoveAboveWater;
        this.canWalkOnSurface = canWalkOnSurface;
    }
}
