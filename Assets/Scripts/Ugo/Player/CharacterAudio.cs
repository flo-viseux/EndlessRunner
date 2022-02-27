using UnityEngine;

public class CharacterAudio : MonoBehaviour
{
    #region Serialized fields
    [Header("Delay")]
    [SerializeField] private CharacterAnimationsEvents characterAnimationsEvents = null;
    [SerializeField] float minDelayBetweenSounds = 0.2f;

    [Header ("Sounds")]
    [SerializeField] private RepetitiveSound JumpSound = null;
    [SerializeField] private RepetitiveSound CoinsSound = null;
    [SerializeField] private RepetitiveSound SlideSound = null;
    [SerializeField] private RepetitiveSound FootStepSound = null;
    [SerializeField] private RepetitiveSound DeathSound = null;
    #endregion

    #region Attributes
    float nextAllowTime;
    #endregion

    #region Unity methods
    private void Awake() 
    {
        characterAnimationsEvents.OnJump += PlayJumpSound;
        characterAnimationsEvents.OnSlide += PlaySlideSound; 
        characterAnimationsEvents.OnFootStep += PlayFootStepSound;   
        characterAnimationsEvents.OnDeath += PlayDeathSound;
    }

    private void PlayFootStepSound()
    {
        if(Time.time - minDelayBetweenSounds > nextAllowTime)
        {
            FootStepSound.PlaySound();
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

    private void PlaySlideSound()
    {
        if(Time.time - minDelayBetweenSounds > nextAllowTime)
        {
            SlideSound.PlaySound();
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
