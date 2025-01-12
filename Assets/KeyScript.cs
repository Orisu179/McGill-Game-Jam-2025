using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyScript : MonoBehaviour
{
    public GameObject lockObject;
    [SerializeField] private GameObject keyEffect;

    private void OnCollisionEnter2D(Collision2D other)
    {
        CameraScript.Shake(1);
        var soundPlayer = other.transform.GetComponentInChildren<EffectsAudioControl>();
        Instantiate(keyEffect, transform.position, Quaternion.identity);
        StartCoroutine(DoorAndKeySound(soundPlayer));
    }

    private IEnumerator DoorAndKeySound(EffectsAudioControl controller)
    {
        controller.PlayCollisionSound("Key");
        lockObject.GetComponent<SpriteRenderer>().enabled = false;
        lockObject.GetComponent<BoxCollider2D>().enabled = false;
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        yield return new WaitForSeconds(0.5f);
        controller.PlayCollisionSound("Door");
        Destroy(lockObject);
        Destroy(gameObject);
    }
}