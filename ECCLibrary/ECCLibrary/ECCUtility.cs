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

    /// <summary>
    /// Applies the MarmosetUBER shader to all renderers in a given prefab and its children, including inactive children.
    /// </summary>
    /// <param name="prefab">The GameObject to fi. Does not necessarily have to be a prefab.</param>
    /// <param name="materialSettings">A set of </param>
    public static void ApplySNShaders(GameObject prefab, UBERMaterialProperties materialSettings)
    {
        var renderers = prefab.GetComponentsInChildren<Renderer>(true);
        var newShader = Shader.Find("MarmosetUBER");
        for (int i = 0; i < renderers.Length; i++)
        {
            for (int j = 0; j < renderers[i].materials.Length; j++)
            {
                Material material = renderers[i].materials[j];
                Texture specularTexture = material.GetTexture("_SpecGlossMap");
                Texture emissionTexture = material.GetTexture("_EmissionMap");
                material.shader = newShader;

                material.DisableKeyword("_SPECGLOSSMAP");
                material.DisableKeyword("_NORMALMAP");
                if (specularTexture != null)
                {
                    material.SetTexture("_SpecTex", specularTexture);
                    material.SetFloat("_SpecInt", materialSettings.SpecularInt);
                    material.SetFloat("_Shininess", materialSettings.Shininess);
                    material.EnableKeyword("MARMO_SPECMAP");
                    material.SetColor("_SpecColor", new Color(1f, 1f, 1f, 1f));
                    material.SetFloat("_Fresnel", 0.24f);
                    material.SetVector("_SpecTex_ST", new Vector4(1.0f, 1.0f, 0.0f, 0.0f));
                }
                if (material.IsKeywordEnabled("_EMISSION"))
                {
                    material.EnableKeyword("MARMO_EMISSION");
                    material.SetFloat("_EnableGlow", 1f);
                    material.SetTexture("_Illum", emissionTexture);
                    material.SetFloat("_GlowStrength", materialSettings.EmissionScale);
                    material.SetFloat("_GlowStrengthNight", materialSettings.EmissionScale);
                }

                if (material.GetTexture("_BumpMap"))
                {
                    material.EnableKeyword("MARMO_NORMALMAP");
                }

                if (CompareStrings(material.name, "Cutout", ECCStringComparison.Contains))
                {
                    material.EnableKeyword("MARMO_ALPHA_CLIP");
                }
                if (CompareStrings(material.name, "Transparent", ECCStringComparison.Contains))
                {
                    material.EnableKeyword("_ZWRITE_ON");
                    material.EnableKeyword("WBOIT");
                    material.SetInt("_ZWrite", 0);
                    material.SetInt("_Cutoff", 0);
                    material.SetFloat("_SrcBlend", 1f);
                    material.SetFloat("_DstBlend", 1f);
                    material.SetFloat("_SrcBlend2", 0f);
                    material.SetFloat("_DstBlend2", 10f);
                    material.SetFloat("_AddSrcBlend", 1f);
                    material.SetFloat("_AddDstBlend", 1f);
                    material.SetFloat("_AddSrcBlend2", 0f);
                    material.SetFloat("_AddDstBlend2", 10f);
                    material.globalIlluminationFlags = MaterialGlobalIlluminationFlags.EmissiveIsBlack | MaterialGlobalIlluminationFlags.RealtimeEmissive;
                    material.renderQueue = 3101;
                    material.enableInstancing = true;
                }
            }
        }
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

    /// <returns>An arbitrary <see cref="AnimationCurve"/> which is used in ECC TrailManagers by default.</returns>
    public static AnimationCurve Curve_Trail()
    {
        return new AnimationCurve(new Keyframe[] { new Keyframe(0f, 0.25f), new Keyframe(1f, 0.75f) });
    }

    /// <returns>An <see cref="AnimationCurve"/> with no variation in value.</returns>
    public static AnimationCurve Curve_Flat(float value = 1f)
    {
        return new AnimationCurve(new Keyframe[] { new Keyframe(0f, value), new Keyframe(1f, value) });
    }

    /// <returns>A new instance of a <see cref="LiveMixinSettings"/>.</returns>
    public static LiveMixinSettings CreateNewLiveMixinData()
    {
        return ScriptableObject.CreateInstance<LiveMixinSettings>();
    }

    /// <summary>
    /// Makes a given GameObject scannable with the scanner room, using the <see cref="ResourceTracker"/> component.
    /// </summary>
    /// <param name="gameObject"></param>
    /// <param name="updatePositionPeriodically">Whether to automatically update the position of this ResourceTracker or not (should always be true for creatures).</param>
    public static void MakeObjectScannerRoomScannable(GameObject gameObject, bool updatePositionPeriodically)
    {
        ResourceTracker resourceTracker = gameObject.AddComponent<ResourceTracker>();
        resourceTracker.prefabIdentifier = gameObject.GetComponent<PrefabIdentifier>();
        resourceTracker.rb = gameObject.GetComponent<Rigidbody>();
        if (updatePositionPeriodically == true)
        {
            gameObject.AddComponent<ResourceTrackerUpdater>();
        }
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
        GameObject obj = SearchChildRecursive(gameObject, byName, stringComparison);
        if (obj == null)
        {
            ECCLog.AddMessage("No child found in hierarchy by name {0}.", byName);
        }
        return obj;
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
/// <summary>
/// Enum with values that correspond to item pickup sounds.
/// </summary>
public enum ItemSoundsType
{
    Default,
    Organic,
    Egg,
    Fins,
    Suit,
    Tank,
    Floater,
    Light,
    AirBladder,
    FirstAidKit,
    Water,
    StillSuitWater,
    Fish
}
