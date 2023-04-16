namespace ECCLibrary;

internal static class ObjectReferences
{
    public static bool Done { get; private set; }

    public static IEnumerator SetReferences()
    {
        var task = CraftData.GetPrefabForTechTypeAsync(TechType.Peeper);
        yield return task;
        var peeper = task.GetResult();
        var lm = peeper.GetComponent<LiveMixin>().data;
        genericCreatureHit = lm.damageEffect;
        electrocutedEffect = lm.electricalDamageEffect;
        respawnerPrefab = peeper.GetComponent<CreatureDeath>().respawnerPrefab;

        Done = true;
    }

    public static GameObject genericCreatureHit;
    public static GameObject electrocutedEffect;
    public static GameObject respawnerPrefab;
}
