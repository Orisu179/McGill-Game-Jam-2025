using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class CharacterMovement : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private float speed = 4.5f;
    [SerializeField] private float jumpForce = 12f;
    [SerializeField] private float delayBetweenDashPress = 0.25f;
    [SerializeField] private float dashingPower;

    private BoxCollider2D _box;
    private Rigidbody2D _rb;
    private bool _isDashing;
    private bool _canDash;
    private float _gravityScale;

    private enum Direction
    {
        Left,
        Right
    }

    private Direction _curDirection;


    void Start()
    {
       _rb = GetComponent<Rigidbody2D>();
       _box = GetComponent<BoxCollider2D>();
       _isDashing = false;
       _canDash = true;
       _curDirection = Direction.Right;
       _gravityScale = _rb.gravityScale;
    }

    // Update is called once per frame
    void Update()
    {

        Vector3 max = _box.bounds.max;
        Vector3 min = _box.bounds.min;
        Vector2 corner1 = new Vector2(max.x, min.y - 0.1f);
        Vector2 corner2 = new Vector2(min.x, min.y - 0.25f);
        Collider2D hit = Physics2D.OverlapArea(corner1, corner2, LayerMask.GetMask("Ground"));
        bool isGrounded = hit != null;

        // This is the case for slope, gravity will pull it down
        float deltaX = Input.GetAxis("Horizontal") * speed;
        _rb.gravityScale = (isGrounded && Mathf.Approximately(deltaX, 0)) ? 0 : 1;
        
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            _rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && _canDash)
        {
            if (deltaX < 0)
            {
                StartCoroutine(Dash(Direction.Left));
            }
            else
            {
                StartCoroutine(Dash(Direction.Right));
            }
        }

    }

    void FixedUpdate()
    {
        if (_isDashing)
            return;
        float deltaX = Input.GetAxis("Horizontal") * speed;
        Vector2 movement = new Vector2(deltaX, _rb.linearVelocity.y);
        _rb.linearVelocity = movement;

    }

    // private void Dash(float lastPressedTime, KeyDownEvent downEvent)
    // {
    //     if (Time.time - lastPressedTime > delayBetweenDashPress) return;
    //     if (downEvent.keyCode == KeyCode.A)
    //     {
    //         _rb.AddForce(Vector2.left * speed, ForceMode2D.Impulse);
    //     } else if (downEvent.keyCode == KeyCode.D)
    //     {
    //
    //     }
    // }

    IEnumerator Dash(Direction direction)
    {
        _canDash = false;
        _isDashing = true;
        _rb.gravityScale = 0;
        _rb.linearVelocityX = transform.localScale.x * dashingPower;
        yield return new WaitForSeconds(0.2f);
        _rb.gravityScale = _gravityScale;
        _isDashing = false;
        yield return new WaitForSeconds(1.0f);
        _canDash = true;
    }

}
