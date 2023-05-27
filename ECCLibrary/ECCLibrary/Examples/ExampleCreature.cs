using ECCLibrary.Data;

namespace ECCLibrary.Examples;

internal class ExampleCreature : CreatureAsset
{
    public ExampleCreature(PrefabInfo prefabInfo) : base(prefabInfo)
    {
    }

    protected override CreatureTemplate CreateTemplate()
    {
        var template = new CreatureTemplate(GetModel(), BehaviourType.SmallFish, EcoTargetType.SmallFish, 160f);
        CreatureTemplateUtils.SetCreatureDataEssentials(template, LargeWorldEntity.CellLevel.Medium, 50f, default, );
        CreatureTemplateUtils.SetCreatureEssentials(template, LargeWorldEntity.CellLevel.Medium, new SwimRandomData(0.2f, 3f, new Vector3(20, 5, 20)), new StayAtLeashData(0.6f, 6f, 14f), 600f);
        template.SetCreatureComponentType<ExampleCreatureComponent>();
        template.AvoidObstaclesData = new AvoidObstaclesData(1f, 3f, false, 5f, 5f);
        template.Mass = 50;
        CreatureTemplateUtils.SetPreyEssentials(template, 5f, new PickupableFishData(true, TechType.Peeper, "WM", "VM"), new EdibleData(true, 69f, 1337f, false, 2f));
        template.SizeDistribution = new AnimationCurve(new Keyframe(0, 0.5f), new Keyframe(1, 1f));
        template.AnimateByVelocityData = new AnimateByVelocityData(6f);
        template.SetWaterParkCreatureData(new WaterParkCreatureDataStruct(0.1f, 0.5f, 1f, 1.5f, true, true, ClassID));
        template.SwimInSchoolData = new SwimInSchoolData(0.5f, 3f, 2f, 0.5f, 1f, 0.1f, 25f);
        return template;
    }

    public static GameObject GetModel()
    {
        var model = new GameObject("CreatureModel");
        model.SetActive(false);

        var worldModel = new GameObject("WM");

        var cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.transform.parent = worldModel.transform;
        var sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphere.transform.parent = worldModel.transform;
        sphere.transform.localPosition = Vector3.forward * 0.5f;

        var viewModel = Object.Instantiate(worldModel);
        viewModel.name = "VM";

        worldModel.transform.parent = model.transform;
        viewModel.transform.parent = model.transform;

        viewModel.SetActive(false);
        viewModel.transform.localScale = Vector3.one * 0.2f;

        worldModel.AddComponent<Animator>();
        viewModel.AddComponent<Animator>();

        foreach (var col in model.GetComponentsInChildren<Collider>(true))
        {
            Object.DestroyImmediate(col);
        }

        model.gameObject.AddComponent<SphereCollider>();

        Object.DontDestroyOnLoad(model);

        return model;
    }

    protected override IEnumerator ModifyPrefab(GameObject prefab, CreatureComponents components)
    {
        CreaturePrefabUtils.AddDamageModifier(prefab, DamageType.Heat, 0f);
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