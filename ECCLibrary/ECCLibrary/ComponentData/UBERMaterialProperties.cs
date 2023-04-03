namespace ECCLibrary;

internal struct UBERMaterialProperties
{
    /// <summary>
    /// How smooth the material appears. Recommended range: 1-8.
    /// </summary>
    public float shininess;
    /// <summary>
    /// How bright the reflection is. Recommended range: 1-10.
    /// </summary>
    public float specularInt;
    /// <summary>
    /// How bright the emission/illum is. Recommended range: 0-5.
    /// </summary>
    public float emissionScale;

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="shininess">How smooth the material appears. Recommended range: 1-8.</param>
    /// <param name="specularInt">How bright the reflection is. Recommended range: 1-10.</param>
    /// <param name="emissionScale">How bright the emission/illum is. Recommended range: 0-5.</param>
    public UBERMaterialProperties(float shininess, float specularInt = 1f, float emissionScale = 1f)
    {
        this.shininess = shininess;
        this.specularInt = specularInt;
        this.emissionScale = emissionScale;
    }
}