using ECCLibrary.Data;

namespace ECCLibrary.Examples;

internal static class ExamplePatcher
{
    public static void PatchExampleCreatures()
    {
        var exampleCreature = new ExampleCreature(PrefabInfo.WithTechType("ExampleCreature", "Example Creature", "An example prey fish."));
        exampleCreature.PrefabInfo.WithIcon(SpriteManager.Get(TechType.LavaBoomerang));
        exampleCreature.Register();

        var exampleLeviathan = new ExampleLeviathan(PrefabInfo.WithTechType("ExampleLeviathan", "Example Leviathan", "An example leviathan."));
        exampleLeviathan.Register();

        var exampleCreature2 = new SealedCreatureAsset(
            PrefabInfo.WithTechType("ExampleCreature2", "Example Creature 2", "A second example creature."),
            new CreatureTemplate(ExampleCreature.GetModel(), BehaviourType.Shark, EcoTargetType.Shark, 200f)
            {
                CellLevel = LargeWorldEntity.CellLevel.Medium,
                SwimRandomData = new SwimRandomData(0.2f, 6f, new Vector3(20, 20, 20)),
                StayAtLeashData = new StayAtLeashData(0.6f, 6f, 14f),
                AvoidObstaclesData = new AvoidObstaclesData(1f, 6f, false, 5f, 5f),
                BioReactorCharge = 600,
                Mass = 100,
                SizeDistribution = new AnimationCurve(new Keyframe(0, 0.5f), new Keyframe(1, 1f)),
                AnimateByVelocityData = new AnimateByVelocityData(8f),
                FleeOnDamageData = new FleeOnDamageData(0.8f, 8f),
                AggressiveWhenSeeTargetList = new List<AggressiveWhenSeeTargetData> { new(EcoTargetType.Shark, 1, 20, 1) },
                AttackLastTargetData = new AttackLastTargetData(0.7f, 8f, 0.5f, 7f)
            },
            ModifyExampleCreature2
        );
        exampleCreature2.Register();
    }

    private static IEnumerator ModifyExampleCreature2(GameObject obj1, CreatureComponents obj2)
    {
        obj1.transform.GetChild(0).localScale = Vector3.one * 5;
        obj1.transform.GetChild(1).gameObject.SetActive(false);
        yield break;
    }
}
