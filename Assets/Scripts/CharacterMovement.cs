using System;
using System.Collections;
using UnityEditor.Callbacks;
using UnityEngine;
using UnityEngine.UIElements;

public class CharacterMovement : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private float speed = 4.5f;
    [SerializeField] private float jumpForce = 12f;
    [SerializeField] private float delayBetweenDashPress = 0.75f;
    [SerializeField] private float dashingPower;

    private BoxCollider2D _box;
    private Rigidbody2D _rb;
    private bool _isDashing;
    private bool _canDash;
    private float _gravityScale;
    private float _speedFactor;
    private float _extraJumpTime;
    private int _facingRight;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _box = GetComponent<BoxCollider2D>();
        _isDashing = false;
        _canDash = true;
        _facingRight = 1;
        _gravityScale = _rb.gravityScale;
    }

    // Update is called once per frame
    void Update()
    {
        float inputX = Input.GetAxis("Horizontal");
        float deltaX = inputX * speed;

        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow)) && _extraJumpTime > 0)
        {
            _rb.linearVelocity = new Vector3(_rb.linearVelocityX, jumpForce);
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && _canDash)
        {
            StartCoroutine(Dash());
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManagement.Instance.LoadScene();
        }

        if(inputX != 0){
            _facingRight = (int)inputX;
        }
    }

    private void FixedUpdate()
    {
        if (_isDashing)
            return;
        float deltaX = Input.GetAxis("Horizontal") * speed * Time.fixedDeltaTime * _speedFactor;
        Vector2 movement = new Vector2(deltaX, _rb.linearVelocity.y);
        _rb.linearVelocity = movement;

        Vector3 max = _box.bounds.max;
        Vector3 min = _box.bounds.min;
        Vector2 corner1 = new Vector2(max.x - (max.x - min.x) / 20, min.y - 0.1f);
        Vector2 corner2 = new Vector2(min.x + (max.x - min.x) / 20, min.y - 0.15f);
        Collider2D hit = Physics2D.OverlapArea(corner1, corner2, LayerMask.GetMask("Ground"));
        bool isGrounded = hit != null;

        MoveSettings(deltaX, isGrounded);
        // This is the case for slope, gravity will pull it down
    }

    private void MoveSettings(float inputX, bool isGrounded)
    {
        if (isGrounded)
        {
            //on ground
            //move faster, grippier
            _rb.linearDamping = 5f;
            _speedFactor = 1;
        }
        else
        {
            //in air
            //move slower, less drag
            _rb.linearDamping = 4;
            _speedFactor = 0.7f;
        }

        //slow rise, fast fall
        if (_rb.linearVelocityY > 2)
        {
            _rb.gravityScale = 1f;
        }
        else if (isGrounded)
        {
            _rb.gravityScale = 0.5f;
        }
        else
        {
            _rb.gravityScale = 2f;
        }

        //direction of the player
        float direction = _rb.linearVelocityX / Mathf.Abs(_rb.linearVelocityX);

        //faster turning
        if (direction != inputX)
        {
            _speedFactor = 2f;
        }

        //extra x drag
        _rb.linearVelocity *= new Vector2(1 - Time.fixedDeltaTime * 2, 1);

        //cayote time
        if (isGrounded)
        {
            _extraJumpTime = 0.1f;
        }

        _extraJumpTime -= Time.deltaTime;
    }

    private IEnumerator Dash()
    {
        _canDash = false;
        _isDashing = true;
        _rb.gravityScale = 0;
        _rb.linearVelocityX = transform.localScale.x * dashingPower * _facingRight;
        yield return new WaitForSeconds(0.2f);

        _rb.gravityScale = _gravityScale;
        _isDashing = false;
        yield return new WaitForSeconds(delayBetweenDashPress);

        _canDash = true;
    }
}