namespace ECCLibrary;

internal static class SanityChecking
{
    private static readonly HashSet<TechType> RegisteredTechTypes = new();

    public static bool TryRegisterTechTypeForFirstTime(TechType techType)
    {
        return RegisteredTechTypes.Add(techType);
    }
}