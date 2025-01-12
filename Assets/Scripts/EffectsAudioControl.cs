using UnityEngine;

public class EffectsAudioControl : MonoBehaviour
{
    [SerializeField] private AudioClip mushroom;
    [SerializeField] private AudioClip bubble;

    public void playCollisionSound(Collider2D other)
    {
        var audioSource = other.gameObject.GetComponent<AudioSource>();
        if (audioSource != null)
        {
            audioSource.PlayOneShot(audioSource.clip);
        }
    }
}
