using UnityEngine.AddressableAssets;

namespace ECCLibrary.Data;

/// <summary>
/// Contains data pertaining to the <see cref="WaterParkCreatureData"/> ScriptableObject. If assigned, allows a creature to be released in Alien Containment.
/// </summary>
public struct WaterParkCreatureDataStruct
{
    /// <summary>
    /// The size of this creature at birth. Typically a fraction fo the max size.
    /// </summary>
    public float initialSize;

    /// <summary>
    /// The maximum size of this creature when fully grown.
    /// </summary>
    public float maxSize;

    /// <summary>
    /// The size of this creature when released outside. Typically 1f for small creatures and lower for larger creatures.
    /// </summary>
    public float outsideSize;
    
    /// <summary>
    /// How many in-game days it takes for this creature to reach its maximum size. Typically takes on a value of 1 to 1.5.
    /// </summary>
    public float daysToGrow;

    /// <summary>
    /// Should be true for creatures that are typically pickupable, and should be FALSE for anything else.
    /// </summary>
    public bool isPickupableOutside;

    /// <summary>
    /// If false, this creature cannot breed regardless of age. True for most creatures besides pets.
    /// </summary>
    public bool canBreed;

    /// <summary>
    /// The prefab for either the egg or the child version of the creature (which can be the adult object).
    /// </summary>
    public CustomGameObjectReference eggOrChildPrefab;

    /// <summary>
    /// The prefab for the adult creature GameObject.
    /// </summary>
    public CustomGameObjectReference adultPrefab;

    /// <summary>
    /// Contains data pertaining to the <see cref="WaterParkCreatureData"/> ScriptableObject. If assigned, allows a creature to be released in Alien Containment.
    /// </summary>
    /// <param name="initialSize">The size of this creature at birth. Typically a fraction fo the max size.</param>
    /// <param name="maxSize">The maximum size of this creature when fully grown.</param>
    /// <param name="outsideSize">The size of this creature when released outside. Typically 1f for small creatures and lower for larger creatures.</param>
    /// <param name="daysToGrow">How many in-game days it takes for this creature to reach its maximum size. Typically takes on a value of 1 to 1.5.</param>
    /// <param name="isPickupableOutside">Should be true for creatures that are typically pickupable, and should be FALSE for anything else.</param>
    /// <param name="canBreed">If false, this creature cannot breed regardless of age. True for most creatures besides pets.</param>
    /// <param name="eggOrChildPrefab">The prefab for either the egg or the child version of the creature (which can be the adult object).</param>
    /// <param name="adultPrefab">The prefab for the adult creature GameObject, for creatures with a juvenile form.</param>
    public WaterParkCreatureDataStruct(float initialSize, float maxSize, float outsideSize, float daysToGrow, bool isPickupableOutside, bool canBreed, CustomGameObjectReference eggOrChildPrefab, CustomGameObjectReference adultPrefab = null)
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

    /// <summary>
    /// Contains data pertaining to the <see cref="WaterParkCreatureData"/> ScriptableObject. If assigned, allows a creature to be released in Alien Containment.
    /// </summary>
    /// <param name="initialSize">The size of this creature at birth. Typically a fraction fo the max size.</param>
    /// <param name="maxSize">The maximum size of this creature when fully grown.</param>
    /// <param name="outsideSize">The size of this creature when released outside. Typically 1f for small creatures and lower for larger creatures.</param>
    /// <param name="daysToGrow">How many in-game days it takes for this creature to reach its maximum size. Typically takes on a value of 1 to 1.5.</param>
    /// <param name="isPickupableOutside">Should be true for creatures that are typically pickupable, and should be FALSE for anything else.</param>
    /// <param name="canBreed">If false, this creature cannot breed regardless of age. True for most creatures besides pets.</param>
    /// <param name="eggOrChildPrefabClassId">ClassID / TechType of the prefab for either the egg or the child version of the creature (which can be the adult object).</param>
    /// <param name="adultPrefabClassId">ClassID of the prefab for the adult creature GameObject, for creatures with a juvenile form.</param>
    public WaterParkCreatureDataStruct(float initialSize, float maxSize, float outsideSize, float daysToGrow, bool isPickupableOutside, bool canBreed, string eggOrChildPrefabClassId, string adultPrefabClassId = null)
    {
        this.initialSize = initialSize;
        this.maxSize = maxSize;
        this.outsideSize = outsideSize;
        this.daysToGrow = daysToGrow;
        this.isPickupableOutside = isPickupableOutside;
        this.canBreed = canBreed;
        if (!string.IsNullOrEmpty(eggOrChildPrefabClassId)) eggOrChildPrefab = new CustomGameObjectReference(eggOrChildPrefabClassId);
        else eggOrChildPrefab = null;
        if (!string.IsNullOrEmpty(adultPrefabClassId)) adultPrefab = new CustomGameObjectReference(adultPrefabClassId);
        else adultPrefab = null;
    }
}
