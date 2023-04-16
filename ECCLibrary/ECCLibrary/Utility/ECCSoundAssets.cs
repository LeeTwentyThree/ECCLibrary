namespace ECCLibrary;

/// <summary>
/// Holds generic sound assets that are used for creatures in the game.
/// </summary>
public static class ECCSoundAssets
{
    /// <summary>
    /// Fish damage sound.
    /// </summary>
    public static FMODAsset FishSplat { get; } = ECCUtility.GetFmodAsset("event:/sub/common/fishsplat", "{0e47f1c6-6178-41bd-93bf-40bfca179cb6}");
}