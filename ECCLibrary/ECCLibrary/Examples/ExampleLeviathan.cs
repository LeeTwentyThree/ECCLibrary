using ECCLibrary.Data;
using System.Net.Configuration;

namespace ECCLibrary.Examples;

internal class ExampleLeviathan : CreatureAsset
{
    public ExampleLeviathan(PrefabInfo prefabInfo) : base(prefabInfo)
    {
    }

    protected override CreatureTemplate CreateTemplate()
    {
        var template = new CreatureTemplate(GetModel(), BehaviourType.Leviathan, EcoTargetType.Leviathan, CreatureDataUtils.CreateLiveMixinData(600f));
        template.CellLevel = LargeWorldEntity.CellLevel.Far;
        template.SwimRandomData = new SwimRandomData(0.2f, 10, new Vector3(20, 20, 20));
        template.StayAtLeashData = new StayAtLeashData(0.6f, 10f, 50f);
        template.AvoidTerrainData = new AvoidTerrainData(1f, 10f, 30f, 30f);
        template.AcidImmune = true;
        template.BioReactorCharge = 4000;
        template.Mass = 2000;
        template.EyeFOV = -0.4f;
        template.LocomotionData = new LocomotionData(15, 0.3f);
        template.SizeDistribution = new AnimationCurve(new Keyframe(0, 0.5f), new Keyframe(1, 1f));
        template.AnimateByVelocityData = new AnimateByVelocityData(6f);
        template.AddAggressiveWhenSeeTargetData(new AggressiveWhenSeeTargetData(EcoTargetType.Shark, 1, 50, 2));
        template.AddAggressiveWhenSeeTargetData(new AggressiveWhenSeeTargetData(EcoTargetType.MediumFish, 0.5f, 50, 2));
        template.AttackLastTargetData = new AttackLastTargetData(0.8f, 12f, 0.5f, 10f);
        return template;
    }

    private GameObject GetModel()
    {
        var model = new GameObject("CreatureModel");
        model.SetActive(false);

        var worldModel = new GameObject("Model");

        var cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.transform.parent = worldModel.transform;
        var sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphere.transform.parent = worldModel.transform;
        sphere.transform.localPosition = Vector3.forward * 0.5f;

        worldModel.transform.parent = model.transform;
        worldModel.AddComponent<Animator>();
        worldModel.transform.localScale = Vector3.one * 10f;

        foreach (var col in model.GetComponentsInChildren<Collider>(true))
        {
            Object.DestroyImmediate(col);
        }

        model.gameObject.AddComponent<BoxCollider>().size = Vector3.one * 10;

        Object.DontDestroyOnLoad(model);

        return model;
    }

    protected override IEnumerator ModifyPrefab(GameObject prefab, CreatureComponents components)
    {
        yield break;
    }
}