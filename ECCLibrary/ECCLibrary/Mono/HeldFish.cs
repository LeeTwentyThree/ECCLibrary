namespace ECCLibrary.Mono;

public class HeldFish : DropTool
{
    public string animationName;

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
}