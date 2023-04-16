using ECCLibrary.Data;

namespace ECCLibrary;

/// <summary>
/// Utilities for using the <see cref="CreatureTemplate"/> class more efficiently.
/// </summary>
public static class CreatureTemplateUtils
{
    /// <summary>
    /// Sets properties related to generic prey creatures. Adds basic fear elements and allows it to be picked up and eaten.
    /// </summary>
    public static void SetupPreyEssentials(CreatureTemplate template, float fleeVelocity, PickupableFishData pickupable, EdibleData edible)
    {
        template.ScareableData = new ScareableData();
        template.FleeWhenScaredData = new FleeWhenScaredData(0.8f, fleeVelocity);
        template.PickupableFishData = pickupable;
        template.EdibleData = edible;
    }
}