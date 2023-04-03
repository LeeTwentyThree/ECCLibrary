namespace ECCLibrary;

internal abstract class CreatureAsset
{
    protected abstract CreatureData GetData();

    protected abstract void ModifyPrefab(GameObject obj);

    private CreatureData data;

    public TechType TechType
    {
        get
        {
            if (data != null && data.PrefabInfo != null) return data.PrefabInfo.TechType;
            ECCPlugin.logger.LogError("Attempting to access TechType property before creature has been initialized!");
            return TechType.None;
        }
    }

    public void Register()
    {
        data = GetData();
        if (data.PrefabInfo == null || data.PrefabInfo.TechType == TechType.None)
        {
            ECCPlugin.logger.LogError("Creature initialized with invalid PrefabInfo!");
            return;
        }
        if (data.Sprite != null) data.PrefabInfo.WithIcon(data.Sprite);
    }
}