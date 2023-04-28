using ECCLibrary.Data;

namespace ECCLibrary;

/// <summary>
/// Contains various methods to assist in creating data objects that have many fields.
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
    public static void SetItemSounds(TechType techType, ItemSoundsType soundType)
    {
        string pickupSound = SoundEvents.GetPickupSoundEvent(soundType);
        string dropSound = SoundEvents.GetDropSoundEvent(soundType);
        string eatSound = SoundEvents.GetEatSoundEvent(soundType);
        CraftData.pickupSoundList.Add(techType, pickupSound);
        CraftData.dropSoundList.Add(techType, dropSound);
        CraftData.useEatSound.Add(techType, eatSound);
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
    /// <returns>Already patched instance of <see cref="PDAEncyclopedia.EntryData"/> and instance of <see cref="PDAScanner.EntryData"/> if applicable (<paramref name="scannable"/> == true).</returns>
    public static void AddCreaturePDAEncyclopediaEntry(CreatureAsset creature, string path, string title, string desc, float scanTime, Texture2D image, Sprite popupImage)
    {
        AddPDAEncyclopediaEntry(creature.PrefabInfo, path, title, desc, image, popupImage, new ScannerEntryData(scanTime));
    }

    /// <summary>
    /// Registers a single PDA encylopedia entry into the game.
    /// </summary>
    /// <param name="info">Relevant PrefabInfo.</param>
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
    /// <param name="image">Databank entry image. Can be null.</param>
    /// <param name="popupImage">Small popup image. Can be null.</param>
    /// <param name="scanData">Data pertaining to scanning. If unassigned, the object will not be scannable.</param>
    public static void AddPDAEncyclopediaEntry(PrefabInfo info, string path, string title, string desc, Texture2D image, Sprite popupImage, ScannerEntryData scanData)
    {
        if (string.IsNullOrEmpty(path))
        {
            return;
        }

        string[] encyNodes;
        if (string.IsNullOrEmpty(path))
            encyNodes = new string[0];
        else
            encyNodes = path.Split('/');

        var encyEntryData = new PDAEncyclopedia.EntryData()
        {
            key = info.ClassID,
            nodes = encyNodes,
            path = path,
            image = image,
            popup = popupImage,
            sound = ECCSoundAssets.UnlockDatabankEntry
        };
        PDAHandler.AddEncyclopediaEntry(encyEntryData);

        
        if (scanData != null)
        {
            PDAScanner.EntryData scannerEntryData = new PDAScanner.EntryData()
            {
                key = info.TechType,
                encyclopedia = info.ClassID,
                scanTime = scanData.scanTime,
                isFragment = scanData.isFragment,
                blueprint = scanData.blueprintToUnlock,
                destroyAfterScan = scanData.destroyAfterScan,
                totalFragments = scanData.totalFragments
            };
            PDAHandler.AddCustomScannerEntry(scannerEntryData);
        }

        if (!string.IsNullOrEmpty(title)) LanguageHandler.SetLanguageLine("Ency_" + info.ClassID, title);
        if (!string.IsNullOrEmpty(desc)) LanguageHandler.SetLanguageLine("EncyDesc_" + info.ClassID, desc);
    }
}