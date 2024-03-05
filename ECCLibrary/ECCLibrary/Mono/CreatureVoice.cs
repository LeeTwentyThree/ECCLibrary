using UnityEngine.Serialization;

namespace ECCLibrary.Mono;

/// <summary>
/// A basic component for creatures that plays idle sounds at random intervals, based on distance.
/// </summary>
public class CreatureVoice : MonoBehaviour
{
    /// <summary>
    /// The normal idle sound asset. Required.
    /// </summary>
    public FMODAsset closeIdleSound;
    /// <summary>
    /// Optional sound asset that is played when the distance between the camera and this creature is greater than <see cref="farThreshold"/>.
    /// </summary>
    public FMODAsset farIdleSound;
    /// <summary>
    /// The component that plays these idle sounds.
    /// </summary>
    public FMOD_CustomEmitter emitter;
    /// <summary>
    /// If true, this creature will play its idle sound when spawned in.
    /// </summary>
    public bool playSoundOnStart;
    /// <summary>
    /// Optional animator for playing an animation when the idle sound is played.
    /// </summary>
    public Animator animator;
    /// <summary>
    /// The animation parameter for the trigger that is activated when the idle sound is played. Must be assigned a value if the animator field is not null.
    /// </summary>
    public string animatorTriggerParam;
    
    /// <summary>
    /// The minimum distance between the creature and the camera required for the idle sound to play.
    /// </summary>
    public float farThreshold = 80;

    /// <summary>
    /// The minimum number of seconds between idle sounds.
    /// </summary>
    public float minInterval = 10;
    /// <summary>
    /// The maximum number of seconds between idle sounds.
    /// </summary>
    public float maxInterval = 20;

    private float _timeCanPlaySoundAgain;
    private bool _hasFarSound;
    
    /// <summary>
    /// The time that any idle sound was last played. Useful for blocking unnecessary interruptions.
    /// </summary>
    public float TimeLastPlayed { get; private set; }

    /// <summary>
    /// Stops any idle sounds from being played until the given number of seconds has passed.
    /// </summary>
    /// <param name="seconds">After this many seconds, the creature will be free to make idle sounds again.</param>
    public void BlockIdleSoundsForTime(float seconds)
    {
        _timeCanPlaySoundAgain = Mathf.Max(Time.time + seconds, _timeCanPlaySoundAgain);
    }
    
    private void Start()
    {
        _hasFarSound = farIdleSound != null;
        if (!playSoundOnStart)
        {
            _timeCanPlaySoundAgain = Time.time + Random.Range(minInterval, maxInterval);
        }
    }

    private void Update()
    {
        if (Time.time < _timeCanPlaySoundAgain) return;
        
        PlayIdleSound();
        _timeCanPlaySoundAgain = Time.time + Random.Range(minInterval, maxInterval);
    }

    private void PlayIdleSound()
    {
        if (_hasFarSound && Vector3.Distance(MainCamera.camera.transform.position, transform.position) >= farThreshold)
        {
            emitter.SetAsset(farIdleSound);
        }
        else
        {
            emitter.SetAsset(closeIdleSound);
        }
        emitter.Play();
        if (animator != null)
        {
            animator.SetTrigger(animatorTriggerParam);
        }
        TimeLastPlayed = Time.time;
    }
}