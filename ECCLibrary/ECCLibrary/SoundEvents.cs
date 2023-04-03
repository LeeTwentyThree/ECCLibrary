using ECCLibrary.Data;

namespace ECCLibrary;

internal static class SoundEvents
{
    public static string GetPickupSoundEvent(ItemSoundsType soundType)
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

    public static string GetDropSoundEvent(ItemSoundsType soundType)
    {
        switch (soundType)
        {
            default:
                return CraftData.defaultDropSound;
            case ItemSoundsType.Floater:
                return "event:/loot/floater/floater_place";
        }
    }

    public static string GetEatSoundEvent(ItemSoundsType soundType)
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
}
