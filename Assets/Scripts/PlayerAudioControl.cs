using System;
using System.Collections;
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
    private bool jumpPlayed;
    private bool dashPlayed;
    private bool walkPlayed;

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
        jumpPlayed = false;
        dashPlayed = false;
        walkPlayed = false;
    }

    // Update is called once per frame
    void Update()
    {
        // if (!characterMovement.isActiveAndEnabled)
        // {
        //     audioSource.Stop();
        //     return;
        // }

        if (characterMovement.isWalking && !characterMovement.isDashing)
        {
            PlaySound("Walk");
        }
        else if (characterMovement.isDashing)
        {
            PlaySound("Dash");
        }
        else if (characterMovement.isJumping)
        {
            PlaySound("Jump");
        }
        else if (walkPlayed && !jumpPlayed && !dashPlayed)
        {
            audioSource.Stop();
            walkPlayed = false;
        }
    }

    private void PlaySound(string soundName)
    {
        if (audioSource.isPlaying)
            return;
        // should be using enum but whatever
        if (soundName == "Jump" && !jumpPlayed)
        {
            audioSource.PlayOneShot(jumpSound);
            StartCoroutine(TimeOutJump(0.5f));
        }
        else if (soundName == "Dash" && !dashPlayed)
        {
            audioSource.PlayOneShot(dashSound);
            StartCoroutine(TimeOutDash(0.8f));
        } else if (soundName == "Walk" && !jumpPlayed && !dashPlayed)
        {
            walkPlayed = true;
            audioSource.Play();
        }
    }

    private IEnumerator TimeOutJump(float seconds)
    {
        jumpPlayed = true;
        yield return new WaitForSeconds(seconds);
        jumpPlayed = false;
    }

    private IEnumerator TimeOutDash(float seconds)
    {
        dashPlayed = true;
        yield return new WaitForSeconds(seconds);
        dashPlayed = false;
    }
}