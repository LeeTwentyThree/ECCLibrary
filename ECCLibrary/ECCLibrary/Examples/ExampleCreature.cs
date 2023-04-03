namespace ECCLibrary.Examples;

internal class ExampleCreature : CreatureAsset
{

    protected override CreatureTemplate CreateTemplate()
    {
        var prefabInfo = PrefabInfo.WithTechType("ExampleCreature", "Example Creature", "Un ejemplo.");
        var liveMixinData = CreaturePrefabUtils.CreateLiveMixinData(160f);

        var model = new GameObject("CreatureModel");

        var cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.transform.parent = model.transform;
        var sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphere.transform.parent = model.transform;
        sphere.transform.localPosition = Vector3.forward * 0.5f;
        cube.AddComponent<Animator>();

        var template = new CreatureTemplate(prefabInfo, model, BehaviourType.SmallFish, EcoTargetType.SmallFish, liveMixinData);
        template.CellLevel = LargeWorldEntity.CellLevel.Far;
        template.SetCreatureComponentType<ExampleCreatureComponent>();
        return template;
    }

    protected override IEnumerator ModifyPrefab(GameObject obj, CreatureComponents components)
    {
        yield break;
    }
}

internal class ExampleCreatureComponent : Creature
{
    public override void Start()
    {
        base.Start();
        ErrorMessage.AddMessage("I'm an example creature!");
    }
}