using UnityEngine;

public class RepetitiveSound : MonoBehaviour
{
    #region Serialized fields
    [SerializeField, Range (0.01f, 0.5f)] private float pitchDelta;
    [SerializeField] private AudioClip[] clips = null;
    [SerializeField] private AudioSource[] audioSources = null;
    #endregion

    #region Attributes
    private int currentIndex = 0;
    float initialPitch;
    #endregion

    #region API
    public void PlaySound()
    {
        float randomizedPitchDelta = Random.Range(-pitchDelta, pitchDelta);
        AudioClip clip = clips[Random.Range(0, clips.Length)];

        AudioSource SelectedAudioSource = null;

        foreach (AudioSource audioSource in audioSources)
            if (!audioSource.isPlaying)
            {
                SelectedAudioSource = audioSource;
                break;
            }

            if (SelectedAudioSource)
            {
                SelectedAudioSource.pitch = initialPitch + randomizedPitchDelta;
                SelectedAudioSource.PlayOneShot(clip);
            }
    }
    #endregion

    #region Unity methods
    private void Awake() 
    {
        initialPitch = audioSources[0].pitch;   
    }
    #endregion
}
