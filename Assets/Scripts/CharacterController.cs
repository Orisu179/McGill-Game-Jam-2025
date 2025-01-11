using System;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private float speed = 4.5f;
    [SerializeField] private float jumpForce = 12f;
    [SerializeField] private float delayBetweenDashPress = 0.25f;
    private BoxCollider2D _box;
    private Rigidbody2D _rb;
    [SerializeField] private Transform checkPosition;
    [SerializeField] private LayerMask ground;

    void Start()
    {
       _rb = GetComponent<Rigidbody2D>();
       _box = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float deltaX = Input.GetAxis("Horizontal") * speed;
        Vector2 movement = new Vector2(deltaX, _rb.linearVelocity.y);
        _rb.linearVelocity = movement;

        /*Vector3 max = _box.bounds.max;
        Vector3 min = _box.bounds.min;
        Vector2 corner1 = new Vector2(max.x, min.y - 0.1f);
        Vector2 corner2 = new Vector2(min.x, min.y - 0.2f);
        Collider2D hit = Physics2D.OverlapArea(corner1, corner2);
        bool isGrounded = hit != null && hit.gameObject.layer == LayerMask.NameToLayer("Ground");
        */
        bool isGrounded = Physics2D.CircleCast(checkPosition.position, 1, Vector2.zero, 0, ground);

        // This is the case for slope, gravity will pull it down
        
        //_rb.gravityScale = (isGrounded && Mathf.Approximately(deltaX, 0)) ? 0 : 1;
        
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            //_rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            _rb.linearVelocityY = jumpForce;
        }

    }

    //TODO: do the dash
    private void Dash()
    {

    }
}
