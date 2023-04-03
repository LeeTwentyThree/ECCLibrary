using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ECCLibrary;
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

    internal static SwimBehaviour EssentialComponentSystem_Swimming(GameObject prefab, float turnSpeed, Rigidbody rb)
    {
        Locomotion locomotion = prefab.AddComponent<Locomotion>();
        locomotion.useRigidbody = rb;
        SplineFollowing splineFollow = prefab.AddComponent<SplineFollowing>();
        splineFollow.respectLOD = false;
        splineFollow.locomotion = locomotion;
        SwimBehaviour swim = prefab.AddComponent<SwimBehaviour>();
        swim.splineFollowing = splineFollow;
        swim.turnSpeed = turnSpeed;
        return swim;
    }

    internal static BehaviourLOD EssentialComponent_BehaviourLOD(GameObject prefab, float near, float medium, float far)
    {
        BehaviourLOD bLod = prefab.AddComponent<BehaviourLOD>();
        bLod.veryCloseThreshold = near;
        bLod.closeThreshold = medium;
        bLod.farThreshold = far;
        return bLod;
    }

    /// <summary>
    /// Makes a given TechType immune to acid, such as brine.
    /// </summary>
    /// <param name="techType"></param>
    public static void MakeAcidImmune(TechType techType)
    {
        List<TechType> acidImmuneList = new List<TechType>(DamageSystem.acidImmune);
        acidImmuneList.Add(techType);
        DamageSystem.acidImmune = acidImmuneList.ToArray();
    }

    /// <summary>
    /// Set the BehaviourType of a TechType. Used for certain creature interactions.
    /// </summary>
    /// <param name="techType"></param>
    /// <param name="behaviourType"></param>
    public static void PatchBehaviorType(TechType techType, BehaviourType behaviourType)
    {
        CreatureData.behaviourTypeList.Add(techType, behaviourType);
    }

    /// <summary>
    /// Set the EquipmentType of an item.
    /// </summary>
    /// <param name="techType"></param>
    /// <param name="equipmentType"></param>
    public static void PatchEquipmentType(TechType techType, EquipmentType equipmentType)
    {
        CraftDataHandler.SetEquipmentType(techType, equipmentType);
    }

    /// <summary>
    /// Patch the inventory sounds of a TechType.
    /// </summary>
    /// <param name="techType"></param>
    /// <param name="soundType"></param>
    public static void PatchItemSounds(TechType techType, ItemSoundsType soundType)
    {
        string pickupSound = GetPickupSoundEvent(soundType);
        string dropSound = GetDropSoundEvent(soundType);
        string eatSound = GetEatSoundEvent(soundType);
        CraftData.pickupSoundList.Add(techType, pickupSound);
        CraftData.dropSoundList.Add(techType, dropSound);
        CraftData.useEatSound.Add(techType, eatSound);
    }

    private static string GetPickupSoundEvent(ItemSoundsType soundType)
    {
        switch (soundType)
        {
            default:
                return CraftData.defaultPickupSound;
            case ItemSoundsType.AirBladder:
                return "event:/tools/airbladder/airbladder_pickup";
            case ItemSoundsType.Light:
                return "event:/tools/lights/pick_up";
            case ItemSoundsType.Egg:
                return "event:/loot/pickup_egg";
            case ItemSoundsType.Fins:
                return "event:/loot/pickup_fins";
            case ItemSoundsType.Floater:
                return "event:/loot/floater/floater_pickup";
            case ItemSoundsType.Suit:
                return "event:/loot/pickup_suit";
            case ItemSoundsType.Tank:
                return "event:/loot/pickup_tank";
            case ItemSoundsType.Organic:
                return "event:/loot/pickup_organic";
            case ItemSoundsType.Fish:
                return "event:/loot/pickup_fish";
        }
    }
    private static string GetDropSoundEvent(ItemSoundsType soundType)
    {
        switch (soundType)
        {
            default:
                return CraftData.defaultDropSound;
            case ItemSoundsType.Floater:
                return "event:/loot/floater/floater_place";
        }
    }
    private static string GetEatSoundEvent(ItemSoundsType soundType)
    {
        switch (soundType)
        {
            default:
                return CraftData.defaultEatSound;
            case ItemSoundsType.Water:
                return "event:/player/drink";
            case ItemSoundsType.FirstAidKit:
                return "event:/player/use_first_aid";
            case ItemSoundsType.StillSuitWater:
                return "event:/player/drink_stillsuit";
        }
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
        return SearchChildRecursive(gameObject, byName, stringComparison);
    }

    static GameObject SearchChildRecursive(GameObject gameObject, string byName, ECCStringComparison stringComparison)
    {
        foreach (Transform child in gameObject.transform)
        {
            if (ECCUtility.CompareStrings(child.gameObject.name, byName, stringComparison))
            {
                return child.gameObject;
            }
            GameObject recursive = SearchChildRecursive(child.gameObject, byName, stringComparison);
            if (recursive)
            {
                return recursive;
            }
        }
        return null;
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