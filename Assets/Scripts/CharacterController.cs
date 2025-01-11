using System;
using Unity.VisualScripting;
using UnityEditor.Callbacks;
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
    private float speedFactor;
    private bool isGrounded;
    private float extraJumpTime;

    void Start()
    {
       _rb = GetComponent<Rigidbody2D>();
       _box = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float inputX = Input.GetAxis("Horizontal");
        _rb.linearVelocity += new Vector2(inputX * speed * Time.fixedDeltaTime, 0) * speedFactor;

        isGrounded = Physics2D.CircleCast(checkPosition.position, 0.1f, Vector2.zero, 0, ground);

        moveSettings(inputX);
    }

    void moveSettings(float inputX){
        if(isGrounded == true){
            //on ground
            //move faster, grippier
            _rb.linearDamping = 5f;
            speedFactor = 1;
        }
        else{
            //in air
            //move slower, less drag
            _rb.linearDamping = 4;
            speedFactor = 0.7f;
        }

        //slow rise, fast fall
        if(_rb.linearVelocityY > 2){
            _rb.gravityScale = 1f;
        }
        else if(isGrounded == true){
            _rb.gravityScale = 0;
        }
        else{
            _rb.gravityScale = 2f;
        }

        //direction of the player
        float direction = _rb.linearVelocityX / Mathf.Abs(_rb.linearVelocityX);

        //faster turning
        if(direction != inputX){
            speedFactor = 2f;
        }

        //extra x drag
        _rb.linearVelocity *= new Vector2(1 - Time.fixedDeltaTime * 2, 1);

        //cayote time
        if(isGrounded == true){
            extraJumpTime = 0.1f;
        }
        extraJumpTime -= Time.deltaTime;
    }

    void Update(){
        //single time inputs are in Update function
        //jumping
        if(extraJumpTime > 0){
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow))
            {
                _rb.linearVelocityY = jumpForce;
            }
        }
    }

    //TODO: do the dash
    private void Dash()
    {

    }
}
