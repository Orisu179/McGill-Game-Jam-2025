using UnityEngine;

public class KeyScript : MonoBehaviour
{
    public GameObject pickupEffect;
    public GameObject lockObject;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D other) {
        Instantiate(pickupEffect, transform.position, Quaternion.identity);
        Destroy(lockObject);
        Destroy(gameObject);
        CameraScript.Shake(1);
    }
}
