using UnityEngine;

public class bubbleScript : MonoBehaviour
{
    private CircleCollider2D hitBox;
    private SpriteRenderer spriteRenderer;
    private float respawnTimer;
    public GameObject popEffect;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        hitBox = gameObject.GetComponent<CircleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        respawnTimer -= Time.deltaTime;

        if(respawnTimer > 0){
            hitBox.enabled = false;
            spriteRenderer.enabled = false;
        }
        else{
            hitBox.enabled = true;
            spriteRenderer.enabled = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        Instantiate(popEffect, transform.position, Quaternion.identity);
        CameraScript.Shake(0.1f);
        respawnTimer = 1.5f;
    }
}
