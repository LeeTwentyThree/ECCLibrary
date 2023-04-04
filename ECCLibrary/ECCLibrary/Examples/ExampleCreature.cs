using ECCLibrary.Data;

namespace ECCLibrary.Examples;

internal class ExampleCreature : CreatureAsset
{
    public ExampleCreature(PrefabInfo prefabInfo) : base(prefabInfo)
    {
    }

    protected override CreatureTemplate CreateTemplate()
    {
        var template = new CreatureTemplate(GetModel(), BehaviourType.SmallFish, EcoTargetType.SmallFish, CreatureDataUtils.CreateLiveMixinData(160f));
        template.CellLevel = LargeWorldEntity.CellLevel.Far;
        template.SetCreatureComponentType<ExampleCreatureComponent>();
        template.SwimRandomData = new SwimRandomData(0.2f, new Vector3(20, 20, 20), 3f);
        template.StayAtLeashData = new StayAtLeashData(0.6f, 14f, 6f);
        template.AcidImmune = true;
        template.BioReactorCharge = 600;
        template.Mass = 50;
        template.EyeFOV = 0.34f;
        template.FleeWhenScaredData = new FleeWhenScaredData(0.7f);
        return template;
    }

    private GameObject GetModel()
    {
        var model = new GameObject("CreatureModel");
        model.SetActive(false);

        var cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.transform.parent = model.transform;
        var sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphere.transform.parent = model.transform;
        sphere.transform.localPosition = Vector3.forward * 0.5f;
        cube.AddComponent<Animator>();

        Object.DontDestroyOnLoad(model);

        return model;
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