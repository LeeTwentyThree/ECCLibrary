namespace ECCLibrary.Data;

/// <summary>
/// Contains data pertaining to the <see cref="PDAScanner.EntryData"/> class, for use in the <see cref="CreatureDataUtils.AddPDAEncyclopediaEntry"/> method.
/// </summary>
public class ScannerEntryData
{
    /// <summary>
    /// The total number of times objects with this TechType must be scanned before <see cref="blueprintToUnlock"/> is unlocked. Required for fragments.
    /// </summary>
    public int totalFragments;

    /// <summary>
    /// If true, the object will be destroyed after scanning.
    /// </summary>
    public bool destroyAfterScan;

    /// <summary>
    /// The blueprint that is unlocked when scanning this object. This may also recursively unlock other blueprints.
    /// </summary>
    public TechType blueprintToUnlock;

    /// <summary>
    /// By default about 2-3 seconds for smaller fish, 5-7 seconds for medium-sized fish, and 9 seconds for larger fauna and leviathans.
    /// </summary>
    public float scanTime;

    /// <summary>
    /// If true, this object may be less likely to spawn when visiting newly loaded areas once the associated blueprint is unlocked.
    /// </summary>
    public bool isFragment;

    /// <summary>
    /// Contains data pertaining to the <see cref="PDAScanner.EntryData"/> class, for use in the <see cref="CreatureDataUtils.AddPDAEncyclopediaEntry"/> method.
    /// </summary>
    /// <param name="totalFragments">The total number of times objects with this TechType must be scanned before <see cref="blueprintToUnlock"/> is unlocked. Required for fragments.</param>
    /// <param name="destroyAfterScan">If true, the object will be destroyed after scanning.</param>
    /// <param name="blueprintToUnlock">The blueprint that is unlocked when scanning this object. This may also recursively unlock other blueprints.</param>
    /// <param name="scanTime">By default about 2-3 seconds for smaller fish, 5-7 seconds for medium-sized fish, and 9 seconds for larger fauna and leviathans.</param>
    /// <param name="isFragment">If true, this object may be less likely to spawn when visiting newly loaded areas once the associated blueprint is unlocked.</param>
    public ScannerEntryData(float scanTime, TechType blueprintToUnlock = TechType.None, bool isFragment = false, int totalFragments = 1, bool destroyAfterScan = false)
    {
        this.totalFragments = totalFragments;
        this.destroyAfterScan = destroyAfterScan;
        this.blueprintToUnlock = blueprintToUnlock;
        this.scanTime = scanTime;
        this.isFragment = isFragment;
    }
}