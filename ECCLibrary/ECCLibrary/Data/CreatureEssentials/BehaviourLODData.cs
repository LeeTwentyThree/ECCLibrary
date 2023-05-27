namespace ECCLibrary.Data;

/// <summary>
/// Contains data pertaining to the <see cref="BehaviourLOD"/> component.
/// </summary>
public struct BehaviourLODData
{
    private float? veryClose;
    private float? close;
    private float? far;

    /// <summary>
    /// Beyond this distance some animations may be removed.
    /// </summary>
    public float VeryClose => veryClose ?? 10;

    /// <summary>
    /// Beyond this distance some functionalities may be less precise.
    /// </summary>
    public float Close => close ?? 50;

    /// <summary>
    /// Beyond this distance trail animations will no longer exist.
    /// </summary>
    public float Far => far ?? 500;

    /// <summary>
    /// Contains data pertaining to the <see cref="BehaviourLOD"/> component.
    /// </summary>
    /// <param name="veryClose">Beyond this distance some animations may be removed. 10f by default.</param>
    /// <param name="close">Beyond this distance some functionalities may be less precise, and any TrailManagers that have <see cref="TrailManager.allowDisableOnScreen"/> enabled (on by default) will stop functioning. 50f by default.</param>
    /// <param name="far">Beyond this distance all TrailManagers will cease to function. 500f by default.</param>
    public BehaviourLODData(float veryClose, float close, float far)
    {
        this.veryClose = veryClose;
        this.close = close;
        this.far = far;
    }
}
