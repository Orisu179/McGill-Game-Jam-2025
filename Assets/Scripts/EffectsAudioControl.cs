using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class EffectsAudioControl : MonoBehaviour
{
    [SerializeField] private AudioClip boomSound;
    [SerializeField] private AudioClip poofSound;
    [SerializeField] private AudioClip thought;
    [SerializeField] private AudioClip resetSound;
    [SerializeField] private AudioClip pickingUpKeySound;
    [SerializeField] private AudioClip openDoorSound;
    [SerializeField] private AudioClip transportSound;
    private Dictionary<string, AudioClip> _audioMap;
    private AudioSource _effectSource;

    private void Start()
    {
        _audioMap = new Dictionary<string, AudioClip>();
        _effectSource = GetComponents<AudioSource>()[1];
        GenerateAudioMap();
    }

    private void GenerateAudioMap()
    {
        _audioMap.Add("Spring", boomSound);
        _audioMap.Add("Bubble", poofSound);
        _audioMap.Add("Thought", thought);
        _audioMap.Add("Border", resetSound);
        _audioMap.Add("Key", pickingUpKeySound);
        _audioMap.Add("Door", openDoorSound);
        _audioMap.Add("Panel", transportSound);
    }
    public void PlayCollisionSound(string collisionString)
    {
       _effectSource.PlayOneShot(_audioMap[collisionString]);
    }
}
