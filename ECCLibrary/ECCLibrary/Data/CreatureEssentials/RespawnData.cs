namespace ECCLibrary.Data;

/// <summary>
/// Contains data pertaining to the <see cref="CreatureDeath"/> component.
/// </summary>
public class RespawnData
{
    /// <summary>
    /// Whether the creature respawns or not.
    /// </summary>
    public bool respawn;
    /// <summary>
    /// If the creature can respawn at all, then this stops it from respawning when killed by the player. Should be FALSE for prey, and TRUE for predators!
    /// </summary>
    public bool respawnOnlyIfKilledByCreature;
    /// <summary>
    /// How long it takes for this creature to respawn.
    /// </summary>
    public float respawnInterval;

    /// <summary>
    /// Contains data pertaining to the <see cref="CreatureDeath"/> component.
    /// </summary>
    /// <param name="respawn">Whether the creature respawns or not.</param>
    /// <param name="respawnOnlyIfKilledByCreature">If the creature can respawn at all, then this stops it from respawning when killed by the player. In the base game this is generally FALSE for passive fauna, and TRUE for aggressive fauna!</param>
    /// <param name="respawnInterval">How long it takes for this creature to respawn.</param>
    public RespawnData(bool respawn, bool respawnOnlyIfKilledByCreature = false, float respawnInterval = 300f)
    {
        this.respawn = respawn;
        this.respawnOnlyIfKilledByCreature = respawnOnlyIfKilledByCreature;
        this.respawnInterval = respawnInterval;
    }
}