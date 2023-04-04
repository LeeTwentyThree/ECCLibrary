using ECCLibrary.Data;
using System.Net.Configuration;

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
        CreatureTemplateUtils.SetupPreyEssentials(template, new PickupableFishData(true, TechType.Peeper, "WM", "VM"), new EdibleData(true, 69f, 1337f, false, 2f));
        template.SizeDistribution = new AnimationCurve(new Keyframe(0, 0.5f), new Keyframe(1, 1f));
        template.AnimateByVelocityData = new AnimateByVelocityData(6f);
        return template;
    }

    private GameObject GetModel()
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