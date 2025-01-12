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
    private bool walkingFlag;
    private void Awake()
    {
       audioSource = GetComponent<AudioSource>();
       audioSource.playOnAwake = false;
       audioSource.Stop();
       audioSource.resource = walkSound;
       audioSource.loop = true;
    }

    void Start()
    {
       characterMovement = GetComponent<CharacterMovement>();
       walkingFlag = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!characterMovement.isActiveAndEnabled)
        {
            audioSource.Stop();
            return;
        }
        if (characterMovement.isWalking && !characterMovement.isDashing)
        {
            PlaySound("Walk");
        }
        else if (Input.GetKeyDown(KeyCode.LeftShift) && characterMovement.canDash)
        {
            PlaySound("Dash");
        }
        else if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow)) && characterMovement.canJump)
        {
            PlaySound("Jump");
        }
        else if(walkingFlag)
        {
            audioSource.Stop();
            walkingFlag = false;
        }
    }

    private void PlaySound(string soundName)
    {
        if (audioSource.isPlaying && walkingFlag)
            return;
        // should be using enum but whatever
        if (soundName == "Walk")
        {
            walkingFlag = true;
            audioSource.Play();
        } else if (soundName == "Jump")
        {
            audioSource.PlayOneShot(jumpSound);
        } else if (soundName == "Dash")
        {
            audioSource.PlayOneShot(dashSound);
        }
    }
}
