using UnityEngine;

public class PlayParticleOnEnable : MonoBehaviour
{

    private void OnEnable()
    {
        ParticleSystem particleSystem = GetComponent<ParticleSystem>();
        if (particleSystem != null)
        {
            particleSystem.Play();
        }
        else
        {
            Debug.LogWarning("Particle System reference not set in PlayParticleOnEnable script.");
        }
    }
}
