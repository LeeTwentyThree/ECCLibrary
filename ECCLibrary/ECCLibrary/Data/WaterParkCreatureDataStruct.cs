using UnityEngine.AddressableAssets;

namespace ECCLibrary.Data;

/// <summary>
/// Contains data pertaining to the <see cref="WaterParkCreatureData"/> ScriptableObject.
/// </summary>
public struct WaterParkCreatureDataStruct
{
    /// <summary>
    /// The size of this creature at birth.
    /// </summary>
    public float initialSize;

    /// <summary>
    /// The maximum size of this creature when fully grown.
    /// </summary>
    public float maxSize;

    /// <summary>
    /// The size of this creature when released outside.
    /// </summary>
    public float outsideSize;
    
    /// <summary>
    /// How many in-game days it takes for this creature to reach its maximum size.
    /// </summary>
    public float daysToGrow;

    /// <summary>
    /// Should be true for creatures that are typically pickupable.
    /// </summary>
    public bool isPickupableOutside;

    /// <summary>
    /// If false, this creature cannot breed regardless of age.
    /// </summary>
    public bool canBreed;

    /// <summary>
    /// The prefab for either the egg or the child version of the creature
    /// </summary>
    public AssetReferenceGameObject eggOrChildPrefab;

    /// <summary>
    /// The prefab for the adult creature GameObject
    /// </summary>
    public AssetReferenceGameObject adultPrefab;

    /// <summary>
    /// Contains data pertaining to the <see cref="WaterParkCreatureData"/> ScriptableObject.
    /// </summary>
    /// <param name="initialSize">The size of this creature at birth.</param>
    /// <param name="maxSize">The maximum size of this creature when fully grown.</param>
    /// <param name="outsideSize">The size of this creature when released outside.</param>
    /// <param name="daysToGrow">How many in-game days it takes for this creature to reach its maximum size.</param>
    /// <param name="isPickupableOutside">Should be true for creatures that are typically pickupable.</param>
    /// <param name="canBreed">If false, this creature cannot breed regardless of age.</param>
    /// <param name="eggOrChildPrefab">IDK</param>
    /// <param name="adultPrefab">IDK</param>
    public WaterParkCreatureDataStruct(float initialSize, float maxSize, float outsideSize, float daysToGrow, bool isPickupableOutside, bool canBreed, AssetReferenceGameObject eggOrChildPrefab, AssetReferenceGameObject adultPrefab)
    {
        this.initialSize = initialSize;
        this.maxSize = maxSize;
        this.outsideSize = outsideSize;
        this.daysToGrow = daysToGrow;
        this.isPickupableOutside = isPickupableOutside;
        this.canBreed = canBreed;
        this.eggOrChildPrefab = eggOrChildPrefab;
        this.adultPrefab = adultPrefab;
    }
}
