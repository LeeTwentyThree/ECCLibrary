namespace ECCLibrary.Mono;

/// <summary>
/// Component that inherits from DropTool for use in custom held fish. Allows reusing vanilla fish holding animations from the player.
/// </summary>
public class HeldFish : DropTool
{
    /// <summary>
    /// Name of the animation parameter.
    /// </summary>
    public string animationName;

    /// <summary>
    /// Overrides the original property.
    /// </summary>
    public override string animToolName
    {
        get
        {
            if (string.IsNullOrEmpty(animationName))
            {
                ECCPlugin.logger.LogError("Item {0} has an invalid ReferenceHoldingAnimation TechType");
            }
            return animationName;
        }
    }

    /// <summary>
    /// Sets the <see cref="animationName"/> field to match the holding animation of the fish defined by <paramref name="techType"/>.
    /// </summary>
    /// <param name="techType"></param>
    public void SetAnimationTechTypeReference(TechType techType)
    {
        animationName = techType.ToString().ToLower();
    }
}