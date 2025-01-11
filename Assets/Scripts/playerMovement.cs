using UnityEngine;

public class playerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private float moveSpeed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float xInput = Input.GetAxisRaw("Horizontal");
        float yInput = Input.GetAxisRaw("Vertical");

        rb.linearVelocity = new Vector2(xInput, yInput) * moveSpeed;
    }
}
