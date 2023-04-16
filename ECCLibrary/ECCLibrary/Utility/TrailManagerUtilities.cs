namespace ECCLibrary;

/// <summary>
/// Utilities related to the <see cref="TrailManager"/> class.
/// </summary>
public static class TrailManagerUtilities
{
    /// <summary>
    /// Animation curve that is flat with a value of 1 all across.
    /// </summary>
    public static AnimationCurve FlatMultiplierAnimationCurve { get; } = new AnimationCurve(new(0, 1), new(1, 1));

    /// <summary>
    /// Returns an animation curve that represents a straight line from <paramref name="left"/> to <paramref name="right"/>.
    /// </summary>
    /// <param name="left"></param>
    /// <param name="right"></param>
    /// <returns></returns>
    public static AnimationCurve GetLinearAnimationCurve(float left, float right)
    {
        return new AnimationCurve(new(0, left), new(1, right));
    }

    /// <summary>
    /// Sets the pitch, roll, and yaw multipliers of a given TrailManager to a single value.
    /// </summary>
    /// <param name="trailManager">The TrailManager to modify.</param>
    /// <param name="curve">The curve to assign to each field.</param>
    /// <returns>The same TrailManager (for fluent code).</returns>
    public static TrailManager SetAllMultiplierCurves(TrailManager trailManager, AnimationCurve curve)
    {
        trailManager.rollMultiplier = curve;
        trailManager.pitchMultiplier = curve;
        trailManager.yawMultiplier = curve;
        return trailManager;
    }
}
