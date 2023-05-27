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
    /// <param name="template">The creature template to modify.</param>
    /// <param name="cellLevel">Roughly determines how far this creature can be loaded in.</param>
    /// <param name="mass">Mass in kg. Ranges from about 1.8f to 4050f. Default is 15kg.</param>
    /// <param name="eyeFov">The FOV is used for detecting things such as prey.
    /// SHOULD BE NEGATIVE! This value has an expected range of [-1, 0]. Is 0f by default. A value of -1 means a given object is ALWAYS in view.</param>
    /// <param name="behaviourLod">Determines the distance for which certain calculations (such as Trail Managers) perform (or don't). It is recommended to increase these values for large creatures.</param>
    /// <param name="bioReactorCharge">Total power output of this creature. All ECC creatures can be put in the bioreactor as long as this value is greater than 0.</param>
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
    /// <param name="template">The creature template to modify.</param>
    /// <param name="swimRandom">Contains data pertaining to the <see cref="SwimRandom"/> action.</param>
    /// <param name="stayAtLeash">Contains data pertaining to the <see cref="StayAtLeashPosition"/> action. This component keeps creatures from wandering too far.</param>
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
    /// <param name="template">The creature template to modify.</param>
    /// <param name="fleeVelocity">The velocity in m/s at which the creature swims away from the player and other perceived threats.</param>
    /// <param name="pickupable">Contains data pertaining to picking up and/or holding fish in your hands.</param>
    /// <param name="edible">Contains data pertaining to the <see cref="Eatable"/> [sic] component.</param>
    public static void SetPreyEssentials(CreatureTemplate template, float fleeVelocity, PickupableFishData pickupable, EdibleData edible)
    {
        template.ScareableData = new ScareableData();
        template.FleeWhenScaredData = new FleeWhenScaredData(0.8f, fleeVelocity);
        template.PickupableFishData = pickupable;
        template.EdibleData = edible;
    }
}