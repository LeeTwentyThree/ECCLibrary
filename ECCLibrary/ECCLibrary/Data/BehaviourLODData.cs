﻿namespace ECCLibrary.Data;

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
    /// <param name="veryClose">Beyond this distance some animations may be removed. 10f by default.</param>
    /// <param name="close">Beyond this distance some functionalities may be less precise. 50f by default.</param>
    /// <param name="far">Beyond this distance trail animations will no longer exist. 500f by default.</param>
    public BehaviourLODData(float veryClose, float close, float far)
    {
        this.veryClose = veryClose;
        this.close = close;
        this.far = far;
    }
}