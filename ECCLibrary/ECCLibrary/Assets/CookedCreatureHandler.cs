using ECCLibrary.Data;
using Ingredient = CraftData.Ingredient;

namespace ECCLibrary.Assets;

/// <summary>
/// Manages the creation of cooked and cured creatures.
/// </summary>
public static class CookedCreatureHandler
{
    /// <summary>
    /// Advanced method to create a cooked/cured creature.
    /// </summary>
    public static void RegisterCookedVariant(PrefabInfo prefabInfo, GameObject creatureModel, EdibleData edibleData, RecipeData recipe, string[] fabricatorSteps,
        VFXFabricatingData vfxFabricatingSettings = default)
    {
        var prefab = new CustomPrefab(prefabInfo);
        prefab.SetRecipe(recipe)
            .WithFabricatorType(CraftTree.Type.Fabricator)
            .WithStepsToFabricatorTab(fabricatorSteps);
        prefab.SetGameObject(new CookedCreatureTemplate(prefabInfo, creatureModel, edibleData, vfxFabricatingSettings));
        prefab.Register();
    }

    /// <summary>
    /// Simple method to create a cooked variant of the given creature.
    /// </summary>
    /// <param name="creature">The original uncooked creature. MUST have been registered already.</param>
    /// <param name="description">Description of the cooked fish item.</param>
    /// <param name="cookedData">Food values.</param>
    /// <param name="vfxFabricatingSettings">Fabricator settings. If not assigned, automatic values will be determined. Do NOT rely on those being perfect.</param>
    public static PrefabInfo RegisterCookedFish(CreatureAsset creature, string description, EdibleData cookedData, VFXFabricatingData vfxFabricatingSettings = default)
    {
        if (creature.Template == null)
        {
            ECCPlugin.logger.LogError("Attempting to call RegisterCookedFish with a creature that has not been registered yet.");
            return default;
        }
        var info = PrefabInfo.WithTechType($"Cooked{creature.ClassID}", $"Cooked {creature.ClassID}", description);
        RegisterCookedVariant(
            info,
            creature.Template.Model,
            cookedData,
            new RecipeData(new Ingredient(creature.TechType)),
            new string[] { "Survival/CookedFood" },
            vfxFabricatingSettings
        );
        return info;
    }

    /// <summary>
    /// Simple method to create a cured variant of the given creature.
    /// </summary>
    /// <param name="creature">The original uncooked creature. MUST have been registered already.</param>
    /// <param name="description">Description of the cured fish item.</param>
    /// <param name="curedData">Food values.</param>
    /// <param name="vfxFabricatingSettings">Fabricator settings. If not assigned, automatic values will be determined. Do NOT rely on those being perfect.</param>
    public static PrefabInfo RegisterCuredFish(CreatureAsset creature, string description, EdibleData curedData, VFXFabricatingData vfxFabricatingSettings = default)
    {
        if (creature.Template == null)
        {
            ECCPlugin.logger.LogError("Attempting to call RegisterCuredFish with a creature that has not been registered yet.");
            return default;
        }
        var info = PrefabInfo.WithTechType($"Cured{creature.ClassID}", $"Cured {creature.ClassID}", description);
        RegisterCookedVariant(
            info,
            creature.Template.Model,
            curedData,
            new RecipeData(new Ingredient(creature.TechType), new Ingredient(TechType.Salt)),
            new string[] { "Survival/CuredFood" },
            vfxFabricatingSettings
        );
        return info;
    }

    private class CookedCreatureTemplate : PrefabTemplate
    {
        private readonly GameObject _model;
        private readonly EdibleData _edible;
        private readonly VFXFabricatingData _vfxFabricatingData;

        public CookedCreatureTemplate(PrefabInfo info, GameObject creatureModel, EdibleData edibleData, VFXFabricatingData vfxFabricatingData) : base(info)
        {
            _model = creatureModel;
            _edible = edibleData;
            _vfxFabricatingData = vfxFabricatingData;
        }

        public override IEnumerator GetPrefabAsync(TaskResult<GameObject> gameObject)
        {
            GameObject prefab = Object.Instantiate(_model);
            PrefabUtils.AddBasicComponents(prefab, info.ClassID, info.TechType, LargeWorldEntity.CellLevel.Near);
            CreaturePrefabUtils.AddEatable(prefab, _edible);
            CreaturePrefabUtils.AddVFXFabricating(prefab, _vfxFabricatingData);
            gameObject.Set(prefab);
            yield break;
        }
    }
}