namespace ECCLibrary.Data;

/// <summary>
/// Contains data pertaining to the <see cref="AnimateByVelocity"/> component. This component sets animation parameters based on the creature's direction &#38; velocity.
/// </summary>
public class AnimateByVelocityData
{
    /// <summary>
    /// At this speed, the Animator's "speed" parameter will be at its maximum of 1f. This should roughly match the creature's maximum velocity.
    /// </summary>
    public float animationMoveMaxSpeed;
    /// <summary>
    /// Pitch can be described by looking up and down. The parameter for pitch 'pitch' and is always on a scale from -1 to 1. When the creature has rotated by a pitch of <see cref="animationMaxPitch"/> in one way, it will equal 1. If it rotated the opposite direction the same amount, it would equal -1.
    /// </summary>
    public float animationMaxPitch;
    /// <summary>
    /// In this case, tilt is rotating left and right. This parameter has the same rules, basically, as <see cref="animationMaxPitch"/>.
    /// </summary>
    public float animationMaxTilt;
    /// <summary>
    /// Strafe animation consists of Up, Down, Left, Right, Forward, and Backwards animations, always relative to the creature's current rotation. The parameters used by this are 'speed_x'. 'speed_y', and 'speed_z', on a scale from -1 to 1. False by default.
    /// </summary>
    public bool useStrafeAnimation;
    /// <summary>
    /// A longer damp time means it takes longer for these strafe, pitch, and tilt animations to take effect, but a more smooth transition.
    /// </summary>
    public float dampTime;

    /// <summary>
    /// Contains data pertaining to the <see cref="AnimateByVelocity"/> component. This component sets animation parameters based on the creature's direction &#38; velocity.
    /// </summary>
    /// <param name="animationMoveMaxSpeed">At this speed, the Animator's "speed" parameter will be at its maximum of 1f. This should roughly match the creature's maximum velocity.</param>
    /// <param name="animationMaxPitch">Pitch can be described by looking up and down. The parameter for pitch 'pitch' and is always on a scale from -1 to 1. When the creature has rotated by a pitch of <see cref="animationMaxPitch"/> in one way, it will equal 1. If it rotated the opposite direction the same amount, it would equal -1.</param>
    /// <param name="animationMaxTilt">In this case, tilt is rotating left and right. This parameter has the same rules, basically, as <see cref="animationMaxPitch"/>.</param>
    /// <param name="useStrafeAnimation">Strafe animation consists of Up, Down, Left, Right, Forward, and Backwards animations, always relative to the creature's current rotation. The parameters used by this are 'speed_x'. 'speed_y', and 'speed_z', on a scale from -1 to 1. False by default.</param>
    /// <param name="dampTime">A longer damp time means it takes longer for these strafe, pitch, and tilt animations to take effect, but a more smooth transition.</param>
    public AnimateByVelocityData(float animationMoveMaxSpeed, float animationMaxPitch = 30f, float animationMaxTilt = 45f, bool useStrafeAnimation = false, float dampTime = 0.5f)
    {
        this.animationMoveMaxSpeed = animationMoveMaxSpeed;
        this.animationMaxPitch = animationMaxPitch;
        this.animationMaxTilt = animationMaxTilt;
        this.useStrafeAnimation = useStrafeAnimation;
        this.dampTime = dampTime;
    }
}