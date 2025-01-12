using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class PlayerAudioControl : MonoBehaviour
{
    [SerializeField] private AudioResource walkSound;

    [SerializeField] private AudioClip jumpSound;

    [SerializeField] private AudioClip dashSound;
    private CharacterMovement characterMovement;
    private AudioSource audioSource;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
       audioSource = GetComponent<AudioSource>();
       audioSource.playOnAwake = false;
       audioSource.Stop();
    }

    void Start()
    {
       characterMovement = GetComponent<CharacterMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (characterMovement.isWalking && !characterMovement.isDashing)
        {
            if (!audioSource.isPlaying)
            {
                PlayWalk();
            }
        } else if (characterMovement.isDashing)
        {
        }
        else
        {
            audioSource.Stop();
        }
    }

    private void PlaySound(string soundName)
    {
        if (soundName == "Walk")
        {

        }
    }
    private void PlayWalk()
    {
        audioSource.resource = walkSound;
        audioSource.loop = true;
        audioSource.Play();
    }
}
