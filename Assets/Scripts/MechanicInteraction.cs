using UnityEngine;
using System.Collections;
public class MechanicInteraction : MonoBehaviour
{
    private Rigidbody2D _rb;
    private Vector3 _startPos;
    public GameObject bangEffect;
    public static bool inThought;
    public float ThoughtShotSpeed;
    public float ThoughtRotationSpeed;
    public Transform mySprite;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _startPos = transform.position;
        _rb = gameObject.GetComponent<Rigidbody2D>();
    }
    
    // Update is called once per frame
    void Update()
    {

    }


    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.tag == "Spring"){
            Instantiate(bangEffect, transform.position, Quaternion.identity);
            _rb.linearVelocity = new Vector2(_rb.linearVelocityX, 15);
            CameraScript.Shake(0.2f);
            other.gameObject.GetComponent<Animation>().Play(); 
            Debug.Log("Play");
        }
        else if(other.gameObject.tag == "Bubble"){
            _rb.linearVelocity = new Vector2(_rb.linearVelocityX, 12);
            transform.position = other.transform.position;
        }
        else if (other.gameObject.tag == "Border")
        {
            transform.position = _startPos;
        }
    }
}
