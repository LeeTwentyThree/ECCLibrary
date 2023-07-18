namespace ECCLibrary.Data;

/// <summary>
/// Contains data pertaining to the <see cref="Eatable"/> [sic] component.
/// </summary>
public class EdibleData
{
    /// <summary>
    /// The max amount of Food this item will give when eaten.
    /// </summary>
    public float foodAmount;
    /// <summary>
    /// The max amount of Water this item will give when eaten.
    /// </summary>
    public float waterAmount;
    /// <summary>
    /// Whether this item decomposes over time.
    /// </summary>
    public bool decomposes;
    /// <summary>
    /// How fast this item decomposes, relative to other items. Default value is 1f.
    /// </summary>
    public float decomposeSpeed;

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="foodAmount">The max amount of Food this item will give when eaten.</param>
    /// <param name="waterAmount">The max amount of Water this item will give when eaten.</param>
    /// <param name="decomposes">Whether this item decomposes over time.</param>
    public EdibleData(float foodAmount, float waterAmount, bool decomposes)
    {
        this.foodAmount = foodAmount;
        this.waterAmount = waterAmount;
        this.decomposes = decomposes;
        decomposeSpeed = 1f;
    }

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="foodAmount">The max amount of Food this item will give when eaten.</param>
    /// <param name="waterAmount">The max amount of Water this item will give when eaten.</param>
    /// <param name="decomposes">Whether this item decomposes over time.</param>
    /// <param name="decomposeSpeed">How fast this item decomposes, relative to other items. Default value is 1f.</param>
    public EdibleData(float foodAmount, float waterAmount, bool decomposes, float decomposeSpeed)
    {
        this.foodAmount = foodAmount;
        this.waterAmount = waterAmount;
        this.decomposes = decomposes;
        this.decomposeSpeed = decomposeSpeed;
    }
}