namespace ECCLibrary.Mono;

/// <summary>
/// This component automatically plays its associated death sound when the creature is killed.
/// </summary>
public class CreatureDeathSound : MonoBehaviour
{
    /// <summary>
    /// The <see cref="FMOD_CustomEmitter"/> component that is played when the creature dies.
    /// </summary>
    public FMOD_CustomEmitter deathSoundEmitter;
    
    private bool _playedSound;

    // This is a Unity event, called by the LiveMixin class
    private void OnKill()
    {
        if (_playedSound)
            return;
        
        _playedSound = true;
        PlayDeathSound();
    }

    private void PlayDeathSound()
    {
        if (deathSoundEmitter == null)
        {
            ECCPlugin.logger.LogError("No emitter assigned to " + this);
            return;
        }
        deathSoundEmitter.Play();
    }
}