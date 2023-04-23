namespace ECCLibrary;

/// <summary>
/// Holds generic sound assets that are used for creatures in the game.
/// </summary>
public static class ECCSoundAssets
{
    /// <summary>
    /// Fish damage sound.
    /// </summary>
    public static FMODAsset FishSplat { get; } = ECCUtility.GetFmodAsset("event:/sub/common/fishsplat");

    /// <summary>
    /// Sound for unlocking normal databank entries.
    /// </summary>
    public static FMODAsset UnlockDatabankEntry { get; } = ECCUtility.GetFmodAsset("event:/tools/scanner/new_encyclopediea");

}