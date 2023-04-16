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
}
