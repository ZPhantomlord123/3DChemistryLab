using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioSource soundEffectSource;

    private void Awake()
    {
        // Singleton pattern to ensure only one instance exists
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }

    // Play a sound effect
    public void PlaySoundEffect()
    {
        soundEffectSource.Play();
    }

    // Stop all sound effects
    public void StopAllSoundEffects()
    {
        soundEffectSource.Stop();
    }

    // You can add more functions here as needed, such as volume control, pitch control, etc.
}
