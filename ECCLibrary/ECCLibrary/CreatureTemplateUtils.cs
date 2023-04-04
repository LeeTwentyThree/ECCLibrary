using ECCLibrary.Data;

namespace ECCLibrary;

/// <summary>
/// Utilities for using the <see cref="CreatureTemplate"/> class more efficiently.
/// </summary>
public static class CreatureTemplateUtils
{
    /// <summary>
    /// Sets properties related to generic prey creatures.
    /// </summary>
    public static void SetupPreyEssentials(CreatureTemplate template, PickupableFishData pickupable, EdibleData edible)
    {
        template.ScareableData = new ScareableData();
        template.FleeWhenScaredData = new FleeWhenScaredData(0.8f);
        template.PickupableFishData = pickupable;
        template.EdibleData = edible;
    }
}