using UnityEngine;

public class CharacterAudio : MonoBehaviour
{
    #region Serialized fields
    [Header("Delay")]
    [SerializeField] private CharacterAnimationsEvents characterAnimationsEvents = null;
    [SerializeField] float minDelayBetweenSounds = 0.2f;

    [Header ("Sounds")]
    [SerializeField] private RepetitiveSound StartSound = null;
    [SerializeField] private RepetitiveSound JumpSound = null;
    [SerializeField] private RepetitiveSound CoinsSound = null;
    [SerializeField] private RepetitiveSound DeathSound = null;

    #endregion

    #region Attributes
    float nextAllowTime;
    #endregion

    #region Unity methods
    private void Awake() 
    {
        characterAnimationsEvents.OnJump += PlayJumpSound;
        characterAnimationsEvents.OnDeath += PlayDeathSound;
    }

    public void PlayStartSound()
    {
        if(Time.time - minDelayBetweenSounds > nextAllowTime)
        {
            StartSound.PlaySound();
        }

        nextAllowTime = Time.time + minDelayBetweenSounds;
    }

    public void PlayCoinsSound()
    {
        if (Time.time - minDelayBetweenSounds > nextAllowTime)
        {
            CoinsSound.PlaySound();
        }

        nextAllowTime = Time.time + minDelayBetweenSounds;
    }

    private void PlayJumpSound()
    {
        if(Time.time - minDelayBetweenSounds > nextAllowTime)
        {
            JumpSound.PlaySound();
        }

        nextAllowTime = Time.time + minDelayBetweenSounds;
    }

    private void PlayDeathSound()
    {
        if(Time.time - minDelayBetweenSounds > nextAllowTime)
        {
            DeathSound.PlaySound();
        }

        nextAllowTime = Time.time + minDelayBetweenSounds;
    }
    #endregion
}
