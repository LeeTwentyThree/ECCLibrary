using ECCLibrary.Data;

namespace ECCLibrary;

/// <summary>
/// Utility methods related to using the <see cref="CreatureTemplate"/> class more efficiently. An alternative to setting all properties manually.
/// </summary>
public static class CreatureTemplateUtils
{
    /// <summary>
    /// <para>Sets the most commonly used properties relating to creature data.</para>
    /// <para>Assigns the following properties so that you don't have to:</para>
    /// <list type="bullet">
    /// <item><see cref="CreatureTemplate.CellLevel"/></item>
    /// <item><see cref="CreatureTemplate.Mass"/></item>
    /// <item><see cref="CreatureTemplate.EyeFOV"/></item>
    /// <item><see cref="CreatureTemplate.BehaviourLODData"/></item>
    /// <item><see cref="CreatureTemplate.BioReactorCharge"/></item>
    /// </list>
    /// </summary>
    public static void SetCreatureDataEssentials(CreatureTemplate template, LargeWorldEntity.CellLevel cellLevel, float mass, float eyeFov = 0f, BehaviourLODData behaviourLod = default, float bioReactorCharge = 200f)
    {
        template.CellLevel = cellLevel;
        template.BioReactorCharge = bioReactorCharge;
        template.Mass = mass;
        template.EyeFOV = eyeFov;
        template.BehaviourLODData = behaviourLod;
    }

    /// <summary>
    /// <para>Sets the most commonly used properties relating to creature motion.</para>
    /// <para>Assigns the following properties so that you don't have to:</para>
    /// <list type="bullet">
    /// <item><see cref="CreatureTemplate.SwimRandomData"/></item>
    /// <item><see cref="CreatureTemplate.StayAtLeashData"/></item>
    /// </list>
    /// </summary>
    public static void SetCreatureMotionEssentials(CreatureTemplate template, SwimRandomData swimRandom, StayAtLeashData stayAtLeash)
    {
        template.SwimRandomData = swimRandom;
        template.StayAtLeashData = stayAtLeash;
    }

    /// <summary>
    /// <para>Sets properties related to generic prey creatures. Adds basic fear elements and allows it to be picked up and eaten.</para>
    /// <para>Assigns the following properties so that you don't have to:</para>
    /// <list type="bullet">
    /// <item><see cref="CreatureTemplate.ScareableData"/></item>
    /// <item><see cref="CreatureTemplate.FleeWhenScaredData"/></item>
    /// <item><see cref="CreatureTemplate.PickupableFishData"/></item>
    /// <item><see cref="CreatureTemplate.EdibleData"/></item>
    /// </list>
    /// </summary>
    public static void SetPreyEssentials(CreatureTemplate template, float fleeVelocity, PickupableFishData pickupable, EdibleData edible)
    {
        template.ScareableData = new ScareableData();
        template.FleeWhenScaredData = new FleeWhenScaredData(0.8f, fleeVelocity);
        template.PickupableFishData = pickupable;
        template.EdibleData = edible;
    }
}