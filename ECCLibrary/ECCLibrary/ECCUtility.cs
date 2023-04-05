using System.Reflection;
using ECCLibrary.Data;

namespace ECCLibrary;

/// <summary>
/// Contains various utilities of no particular category.
/// </summary>
public static class ECCUtility
{
    /// <summary>
    /// Helps in the loading of AssetBundles from an "Assets" folder in your mod folder root. An example of an AssetBundle path: `...Subnautica\QMods\DeExtinction\Assets\deextinctionassets`.
    /// </summary>
    /// <param name="modAssembly">The assembly to grab the mod from. See <see cref="Assembly.GetExecutingAssembly"/>.</param>
    /// <param name="assetsFileName">The name of the AssetBundle file in your assets folder, that will be loaded. For De-Extinction, it is `deextinctionassets`.</param>
    /// <returns>A loaded AssetBundle.</returns>
    public static AssetBundle LoadAssetBundleFromAssetsFolder(Assembly modAssembly, string assetsFileName)
    {
        return AssetBundle.LoadFromFile(Path.Combine(Path.GetDirectoryName(modAssembly.Location), "Assets", assetsFileName));
    }

    /// <summary>
    /// Compares two strings using the simplified ECCStringComparison.
    /// </summary>
    /// <param name="original"></param>
    /// <param name="compareTo"></param>
    /// <param name="comparisonMode"></param>
    /// <returns></returns>
    public static bool CompareStrings(string original, string compareTo, ECCStringComparison comparisonMode)
    {
        switch (comparisonMode)
        {
            default:
                return original == compareTo;
            case ECCStringComparison.Equals:
                return original.ToLower() == compareTo.ToLower();
            case ECCStringComparison.EqualsCaseSensitive:
                return original == compareTo;
            case ECCStringComparison.StartsWith:
                return original.ToLower().StartsWith(compareTo.ToLower());
            case ECCStringComparison.StartsWithCaseSensitive:
                return original.StartsWith(compareTo);
            case ECCStringComparison.Contains:
                return original.ToLower().Contains(compareTo.ToLower());
            case ECCStringComparison.ContainsCaseSensitive:
                return original.Contains(compareTo);
        }
    }

    private static PhysicMaterial noFrictionPhysicMaterial;

    /// <summary>
    /// Returns an instance of the PhysicMaterial class that should be used for creatures. The dynamicFriction and staticFriction fields are set to 0 and the combine mode is set to multiply. Please do not modify fields on this class.
    /// </summary>
    /// <returns></returns>
    public static PhysicMaterial FrictionlessPhysicMaterial
    {
        get
        {
            if (noFrictionPhysicMaterial == null)
            {
                noFrictionPhysicMaterial = new PhysicMaterial("NoFriction")
                {
                    dynamicFriction = 0f,
                    staticFriction = 0f,
                    frictionCombine = PhysicMaterialCombine.Multiply,
                    bounceCombine = PhysicMaterialCombine.Multiply
                };
            }
            return noFrictionPhysicMaterial;
        }
    }

    /// <summary>
    /// Converts a <see cref="Atlas.Sprite"/> to a <see cref="Sprite"/>.
    /// </summary>
    /// <param name="sprite"></param>
    public static Sprite CreateSpriteFromAtlasSprite(Atlas.Sprite sprite)
    {
        var tex = sprite.texture;
        return Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), Vector2.one * 0.5f);
    }

    /// <summary>
    /// Creates an FMODAsset with the given parameters.
    /// </summary>
    /// <param name="path"></param>
    /// <param name="id"></param>
    /// <returns></returns>
    public static FMODAsset GetFmodAsset(string path, string id)
    {
        var asset = ScriptableObject.CreateInstance<FMODAsset>();
        asset.path = path;
        asset.id = id;
        return asset;
    }

    internal static Transform SearchChildRecursive(Transform transform, string byName, ECCStringComparison stringComparison)
    {
        foreach (Transform child in transform)
        {
            if (CompareStrings(child.gameObject.name, byName, stringComparison))
            {
                return child;
            }
            Transform recursive = SearchChildRecursive(child, byName, stringComparison);
            if (recursive)
            {
                return recursive;
            }
        }
        return null;
    }
}
/// <summary>
/// Various ECC-related extensions for GameObjects.
/// </summary>
public static class GameObjectExtensions
{
    /// <summary>
    /// Find a GameObject in this object's hiearchy, by name.
    /// </summary>
    /// <param name="gameObject"></param>
    /// <param name="byName"></param>
    /// <param name="stringComparison"></param>
    /// <returns></returns>
    public static GameObject SearchChild(this GameObject gameObject, string byName, ECCStringComparison stringComparison = ECCStringComparison.Equals)
    {
        return ECCUtility.SearchChildRecursive(gameObject.transform, byName, stringComparison).gameObject;
    }
}
/// <summary>
/// Various ECC-related extensions for Transforms.
/// </summary>
public static class TransformExtensions
{
    /// <summary>
    /// Find a GameObject in this object's hiearchy, by name.
    /// </summary>
    /// <param name="transform"></param>
    /// <param name="byName"></param>
    /// <param name="stringComparison"></param>
    /// <returns></returns>
    public static Transform SearchChild(this Transform transform, string byName, ECCStringComparison stringComparison = ECCStringComparison.Equals)
    {
        return ECCUtility.SearchChildRecursive(transform, byName, stringComparison);
    }
}
/// <summary>
/// Enum which is solely used for ECCHelper methods.
/// </summary>
public enum ECCStringComparison
{
    /// <summary>
    /// 'A' == 'a'
    /// </summary>
    Equals,
    /// <summary>
    /// 'A' != 'a'
    /// </summary>
    EqualsCaseSensitive,
    /// <summary>
    /// Whether this string starts with the other given string. Not case sensitive.
    /// </summary>
    StartsWith,
    /// <summary>
    /// Whether this string starts with the other given string. Case sensitive.
    /// </summary>
    StartsWithCaseSensitive,
    /// <summary>
    /// Whether a given string is located anywhere inside of a larger string. Not case sensitive.
    /// </summary>
    Contains,
    /// <summary>
    /// Whether a given string is located anywhere inside of a larger string. Case sensitive.
    /// </summary>
    ContainsCaseSensitive
}