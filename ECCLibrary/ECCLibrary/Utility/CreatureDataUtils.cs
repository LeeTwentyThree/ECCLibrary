using ECCLibrary.Data;

namespace ECCLibrary;

/// <summary>
/// Utility methods related to assigning patch-time data.
/// </summary>
public static class CreatureDataUtils
{
    /// <summary>
    /// Creates an instance of the <see cref="LiveMixinData"/> ScriptableObject.
    /// </summary>
    /// <param name="maxHealth"></param>
    /// <param name="weldable"></param>
    /// <param name="knifeable"></param>
    /// <param name="destroyOnDeath"></param>
    /// <returns></returns>
    public static LiveMixinData CreateLiveMixinData(float maxHealth, bool weldable = false, bool knifeable = true, bool destroyOnDeath = false)
    {
        var data = ScriptableObject.CreateInstance<LiveMixinData>();
        data.maxHealth = maxHealth;
        data.weldable = weldable;
        data.knifeable = knifeable;
        data.destroyOnDeath = destroyOnDeath;
        return data;
    }

    /// <summary>
    /// Makes a given TechType immune to acid, such as brine.
    /// </summary>
    /// <param name="techType"></param>
    public static void SetAcidImmune(TechType techType)
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
    public static void SetBehaviorType(TechType techType, BehaviourType behaviourType)
    {
        CreatureData.behaviourTypeList.Add(techType, behaviourType);
    }

    /// <summary>
    /// Patch the inventory sounds of a TechType.
    /// </summary>
    /// <param name="techType"></param>
    /// <param name="soundType"></param>
#if SUBNAUTICA
    public static void SetItemSounds(TechType techType, ItemSoundsType soundType)
#elif BELOWZERO
    public static void SetItemSounds(TechType techType, TechData.SoundType soundType)
#endif
    {
#if SUBNAUTICA
        CraftData.pickupSoundList.Add(techType, SoundEvents.GetPickupSoundEvent(soundType));
        CraftData.dropSoundList.Add(techType, SoundEvents.GetDropSoundEvent(soundType));
        CraftData.useEatSound.Add(techType, SoundEvents.GetEatSoundEvent(soundType));
#elif BELOWZERO
        CraftDataHandler.SetSoundType(techType, soundType);
#endif
    }

    /// <summary>
    /// Sets the Bioreactor charge of <paramref name="techType"/> to <paramref name="charge"/>.
    /// </summary>
    /// <param name="techType"></param>
    /// <param name="charge"></param>
    public static void SetBioreactorCharge(TechType techType, float charge)
    {
        var dict = BaseBioReactor.charge;
        if (dict.ContainsKey(techType))
        {
            dict[techType] = charge;
        }
        else
        {
            dict.Add(techType, charge);
        }
    }

    /// <summary>
    /// Registers a single PDA encylopedia entry into the game for a given creature asset.
    /// </summary>
    /// <param name="creature">Relevant CreatureAsset.</param>
    /// <param name="path"><para>Path to this entry in the databank.</para>
    /// <para>To find examples of this string, open "...Subnautica\Subnautica_Data\StreamingAssets\SNUnmanagedData\LanguageFiles\English.json" and search for "EncyPath".</para>
    /// <para>Examples:</para>
    /// <list type="bullet">
    /// <item>Lifeforms/Fauna/Herbivores</item>
    /// <item>Lifeforms/Fauna/Carnivores</item>
    /// <item>Lifeforms/Fauna/Rays</item>
    /// <item>Lifeforms/Fauna/Sharks</item>
    /// <item>Lifeforms/Fauna/Leviathans</item>
    /// <item>Lifeforms/Fauna/Other</item>
    /// <item>Lifeforms/Fauna/SmallHerbivores</item>
    /// <item>Lifeforms/Fauna/LargeHerbivores</item>
    /// </list>
    /// </param>
    /// <param name="title">Displayed title of the PDA entry in English. If set to null, you can implement your own language system.</param>
    /// <param name="desc">Displayed description of the PDA entry in English. If set to null, you can implement your own language system.</param>
    /// <param name="scanTime">Duration of scanning in seconds.</param>
    /// <param name="image">Databank entry image. Can be null.</param>
    /// <param name="popupImage">Small popup image. Can be null.</param>
    public static void AddCreaturePDAEncyclopediaEntry(CreatureAsset creature, string path, string title, string desc, float scanTime, Texture2D image, Sprite popupImage)
    {
        PDAHandler.AddEncyclopediaEntry(creature.ClassID, path, title, desc, image, popupImage, PDAHandler.UnlockBasic);
        PDAHandler.AddCustomScannerEntry(creature.TechType, scanTime, false, creature.ClassID);
    }
}