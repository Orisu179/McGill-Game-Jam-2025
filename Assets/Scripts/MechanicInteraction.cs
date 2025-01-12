using UnityEngine;
using System.Collections;
public class MechanicInteraction : MonoBehaviour
{
    private Rigidbody2D _rb;
    private Vector3 _startPos;
    private EffectsAudioControl _audioControl;
    public GameObject bangEffect;
    public Transform mySprite;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _startPos = transform.position;
        _rb = gameObject.GetComponent<Rigidbody2D>();
        _audioControl = gameObject.GetComponentInChildren<EffectsAudioControl>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.CompareTag("Spring")){
            Instantiate(bangEffect, transform.position, Quaternion.identity);
            _rb.linearVelocity = new Vector2(_rb.linearVelocityX, 15);
            CameraScript.Shake(0.3f);
            other.gameObject.GetComponent<Animation>().Play();
            _audioControl.PlayCollisionSound("Spring");
        }
        else if(other.gameObject.CompareTag("Bubble")){
            _rb.linearVelocity = new Vector2(_rb.linearVelocityX, 12);
            transform.position = other.transform.position;
            _audioControl.PlayCollisionSound("Bubble");
        }
        else if (other.gameObject.CompareTag("Border"))
        {
            transform.position = _startPos;
            _audioControl.PlayCollisionSound("Border");
        }
        else if (other.gameObject.CompareTag("Portal"))
        {

        }
    }
}
