using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyScript : MonoBehaviour
{
    public GameObject pickupEffect;
    public GameObject lockObject;

    private void OnCollisionEnter2D(Collision2D other)
    {
        Instantiate(pickupEffect, transform.position, Quaternion.identity);
        CameraScript.Shake(1);
        //var soundPlayer = other.transform.GetComponentInChildren<EffectsAudioControl>();
        //StartCoroutine(DoorAndKeySound(soundPlayer));
        Destroy(lockObject);
        Destroy(gameObject);
    }

    private IEnumerator DoorAndKeySound(EffectsAudioControl controller)
    {
        controller.PlayCollisionSound("Key");
        yield return new WaitForSeconds(0.5f);
        controller.PlayCollisionSound("Door");
        Destroy(lockObject);
        Destroy(gameObject);
    }
}