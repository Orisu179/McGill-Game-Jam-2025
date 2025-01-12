using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class PlayerAudioControl : MonoBehaviour
{
    [SerializeField] private AudioClip jumpSound;
    [SerializeField] private AudioClip dashSound;
    private CharacterMovement _characterMovement;
    private AudioSource _walkingSource;
    private AudioSource _dashAndJumpSource;

    private void Awake()
    {
        _walkingSource = GetComponents<AudioSource>()[0];
        _dashAndJumpSource = GetComponents<AudioSource>()[1];
        _walkingSource.playOnAwake = false;
        _walkingSource.Stop();
        _walkingSource.loop = true;
    }

    void Start()
    {
        _characterMovement = GetComponent<CharacterMovement>();
    }

    void Update()
    {
        if (_characterMovement.IsWalking && !_characterMovement.IsDashing)
        {
            PlayWalkSound();
        }
        else if (_walkingSource.isPlaying)
        {
            _walkingSource.Stop();
        }

        if (_characterMovement.CanDash && Input.GetKeyDown(KeyCode.LeftShift))
        {
            PlaySound("Dash");
        }
        else if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow)) && _characterMovement.CanJump)
        {
            PlaySound("Jump");
        }
    }

    private void PlayWalkSound()
    {
        if (_walkingSource.isPlaying)
        {
            return;
        }
        _walkingSource.Play();
    }

    private void PlaySound(string soundName)
    {
        // should be using enum but whatever
        if (soundName == "Jump")
        {
            _dashAndJumpSource.PlayOneShot(jumpSound);
        }
        else if (soundName == "Dash")
        {
            _dashAndJumpSource.PlayOneShot(dashSound);
        }
    }
}