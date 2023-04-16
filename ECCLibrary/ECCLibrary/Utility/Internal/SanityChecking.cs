namespace ECCLibrary;

internal class SanityChecking
{
    private static HashSet<TechType> registeredTechTypes = new HashSet<TechType>();

    public static bool CanRegisterTechTypeSafely(TechType techType)
    {
        return registeredTechTypes.Add(techType);
    }
}