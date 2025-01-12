using UnityEngine;

public class EffectsAudioControl : MonoBehaviour
{
    [SerializeField] private AudioClip mushroom;
    [SerializeField] private AudioClip bubble;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void playCollisionSound(Collider2D other)
    {
        var audioSource = other.gameObject.GetComponent<AudioSource>();
        if (audioSource != null)
        {

        }
    }
}
