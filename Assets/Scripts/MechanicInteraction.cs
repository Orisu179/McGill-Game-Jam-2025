using UnityEngine;
public class MechanicInteraction : MonoBehaviour
{
    private Rigidbody2D _rb;
    public GameObject bangEffect;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
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
            CameraScript.Shake(0.3f);
        }
        else if(other.gameObject.tag == "Bubble"){
            _rb.linearVelocity = new Vector2(_rb.linearVelocityX, 12);
        }
    }
}
