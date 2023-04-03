namespace ECCLibrary.Data;

/// <summary>
/// Contains data pertaining to the <see cref="BehaviourLOD"/> component.
/// </summary>
public struct BehaviourLODData
{
    /// <summary>
    /// Beyond this distance some animations may be removed
    /// </summary>
    public float veryClose;
    /// <summary>
    /// Beyond this distance some functionalities may be less precise
    /// </summary>
    public float close;
    /// <summary>
    /// Beyond this distance trail animations will no longer exist
    /// </summary>
    public float far;

    /// <summary>
    /// Contains data pertaining to the <see cref="BehaviourLOD"/> component.
    /// </summary>
    /// <param name="veryClose">Beyond this distance some animations may be removed.</param>
    /// <param name="close">Beyond this distance some functionalities may be less precise.</param>
    /// <param name="far">Beyond this distance trail animations will no longer exist.</param>
    public BehaviourLODData(float veryClose = 10, float close = 50, float far = 500)
    {
        this.veryClose = veryClose;
        this.close = close;
        this.far = far;
    }
}
