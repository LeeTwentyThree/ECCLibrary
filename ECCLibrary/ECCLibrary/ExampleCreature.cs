namespace ECCLibrary;

internal class ExampleCreature : CreatureAsset
{
    protected override void ModifyPrefab(GameObject obj)
    {
        
    }

    protected override CreatureData GetData()
    {
        var prefabInfo = PrefabInfo.WithTechType("ExampleCreature", "Example Creature", "Un ejemplo.");
        var liveMixinData = CreaturePrefabUtils.CreateLiveMixinData(160f);
        var data = new CreatureData(prefabInfo, LargeWorldEntity.CellLevel.Medium, BehaviourType.SmallFish, EcoTargetType.SmallFish, liveMixinData);
        return data;
    }
}