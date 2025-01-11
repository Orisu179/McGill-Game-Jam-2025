using UnityEngine;

public class PlayerAudioControl : MonoBehaviour
{
    [SerializeField] private AudioClip walkSound;

    [SerializeField] private AudioClip jumpSound;

    [SerializeField] private AudioClip dashSound;
    private CharacterMovement characterMovement;
    private AudioSource audioSource;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       characterMovement = GetComponent<CharacterMovement>();
       audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (characterMovement.isWalking && !characterMovement.isDashing)
        {
            if (audioSource.isPlaying)
            {
                audioSource.Stop();
            }
            audioSource.clip = walkSound;
            audioSource.loop = true;
            audioSource.Play();
        }
    }
}
