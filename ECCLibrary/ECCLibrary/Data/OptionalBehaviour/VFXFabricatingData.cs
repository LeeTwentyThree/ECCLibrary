namespace ECCLibrary.Data;

/// <summary>
/// Data that controls the creation of the VFXFabricating component on a prefab, in order to have a model in the Fabricator.
/// </summary>
public class VFXFabricatingData
{
    /// <summary>
    /// Leave as null or empty to point to the prefab root. Otherwise this is the path to the crafting model Transform, relative to the prefab's root Transform.
    /// For example, the Repair Tool's would be `welder_scaled/welder`.
    /// </summary>
    public string pathToModel;

    /// <summary>
    /// <para>The relative y position of where the ghost effect begins, in global coordinates relative to the model's center, taking the posOffset into account.</para>
    /// <para>Typically a negative value because the bottom of an object is below its center.
    /// You may need to adjust this at runtime with Subnautica Runtime Editor to get desired results.</para>
    /// </summary>
    public float minY;

    /// <summary>
    /// <para>The relative y position of where the ghost effect ends, in global coordinates relative to the model's center, taking the posOffset into account.</para>
    /// <para>Typically a positive value because the top of an object is above its center.
    /// You may need to adjust this at runtime with Subnautica Runtime Editor to get desired results.</para>
    /// </summary>
    public float maxY;

    /// <summary>
    /// The offset of the model when being crafted (in METERS). This is generally around zero, but the y value may be ajusted up or down a few millimeters to fix clipping/floating issues.
    /// </summary>
    public Vector3 posOffset;

    /// <summary>
    /// Rotational offset.
    /// </summary>
    public Vector3 eulerOffset;

    /// <summary>
    /// The relative scale of the model. Generally is 1x for most items.
    /// </summary>
    public float scaleFactor;

    /// <summary>
    /// Data that controls the creation of the VFXFabricating component on a prefab, in order to have a model in the Fabricator.
    /// </summary>
    /// <param name="pathToModel">Leave as null or empty to point to the prefab root. Otherwise this is the path to the crafting model Transform, relative to the prefab's root Transform.
    /// For example, the Repair Tool's would be `welder_scaled/welder`.</param>
    /// <param name="minY"><para>The relative y position of where the ghost effect begins, in global coordinates relative to the model's center, taking the posOffset into account.</para>
    /// <para>Typically a negative value because the bottom of an object is below its center.
    /// You may need to adjust this at runtime with Subnautica Runtime Editor to get desired results.</para></param>
    /// <param name="maxY"><para>The relative y position of where the ghost effect ends, in global coordinates relative to the model's center, taking the posOffset into account.</para>
    /// <para>Typically a positive value because the top of an object is above its center.
    /// You may need to adjust this at runtime with Subnautica Runtime Editor to get desired results.</para></param>
    /// <param name="posOffset">The offset of the model when being crafted (in METERS). This is generally around zero, but the y value may be ajusted up or down a few millimeters to fix clipping/floating issues.</param>
    /// <param name="scaleFactor">The relative scale of the model. Generally is 1x for most items.</param>
    /// <param name="eulerOffset">Rotational offset.</param>
    public VFXFabricatingData(string pathToModel, float minY, float maxY, Vector3 posOffset = default, float scaleFactor = 1, Vector3 eulerOffset = default)
    {
        this.pathToModel = pathToModel;
        this.minY = minY;
        this.maxY = maxY;
        this.posOffset = posOffset;
        this.eulerOffset = eulerOffset;
        this.scaleFactor = scaleFactor;
    }
}
