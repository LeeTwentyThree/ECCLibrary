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
    public static void SetupPreyBehaviour(CreatureTemplate template)
    {
        template.ScareableData = new ScareableData();
        template.FleeWhenScaredData = new FleeWhenScaredData(0.8f);
    }
}