namespace ECCLibrary.Data;

/// <summary>
/// Contains data pertaining to picking up and/or holding fish in your hands.
/// </summary>
public class PickupableFishData
{
    /// <summary>
    /// Can this fish be held in the hotbar?
    /// </summary>
    public bool CanBeHeld;
    /// <summary>
    /// If can be held: The TechType that is used to find the holding animation.
    /// </summary>
    public TechType ReferenceHoldingAnimation;
    /// <summary>
    /// If can be held: The name of the model used for the World View, which must be a child of the object.
    /// </summary>
    public string WorldModelName;
    /// <summary>
    /// If can be held: The name of the model used for the First Person View, which must be a child of the object.
    /// </summary>
    public string ViewModelName;

    /// <summary>
    /// Contains data pertaining to picking up and/or holding fish in your hands.
    /// </summary>
    /// <param name="canBeHeld">Can this fish be held in the hotbar?</param>
    /// <param name="referenceHoldingAnimation">If can be held: The TechType that is used to find the holding animation.</param>
    /// <param name="worldModelName">If can be held: The name of the model used for the World View, which must be a child of the object.</param>
    /// <param name="viewModelName">If can be held: The name of the model used for the First Person View, which must be a child of the object.</param>
    public PickupableFishData(bool canBeHeld, TechType referenceHoldingAnimation, string worldModelName, string viewModelName)
    {
        CanBeHeld = canBeHeld;
        ReferenceHoldingAnimation = referenceHoldingAnimation;
        WorldModelName = worldModelName;
        ViewModelName = viewModelName;
    }
}