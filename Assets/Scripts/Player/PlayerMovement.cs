using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Assertions.Comparers;
using System;

public class PlayerMovement : MonoBehaviour
{
    //components
    private Rigidbody2D rb;
    private Animator myAnimator;
    private SpriteRenderer myRenderer;
    private Transform meleeRange;

    //variables
    private float horizontal;
    private bool isGrounded = false;
    private bool isFacingRight = true;
    [SerializeField]
    private float speed;
    [SerializeField]
    private float jumpingPower;
    private float coyoteTime = 0.2f;
    private float coyoteTimeTimer;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isFacingRight && horizontal > 0f)
        {
            Flip();
        }
        else if (isFacingRight && horizontal < 0f)
        {
            Flip();
        }

        if (isGrounded)
        {
            coyoteTimeTimer = coyoteTime;
        }
        else
        {
            coyoteTimeTimer -= Time.deltaTime;
        }
    }
    
    void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontal * speed,rb.velocity.y);

        //velocity for walking/jumping animations
        myAnimator.SetFloat("xVelocity", Math.Abs(rb.velocity.x));
        myAnimator.SetFloat("yVelocity", rb.velocity.y);

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        isGrounded = true;
        myAnimator.SetBool("isJumping", !isGrounded);
    }

    void OnTriggerExit2D(Collider2D other)
    {
        isGrounded = false;
        myAnimator.SetBool("isJumping", !isGrounded);
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 flipX = transform.localScale;
        flipX.x *= -1;
        transform.localScale = flipX;
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (context.performed && coyoteTimeTimer > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
        }

        if (context.canceled && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
            coyoteTimeTimer = 0f;
        }
    }

    public void Move(InputAction.CallbackContext context)
    {
        horizontal = context.ReadValue<Vector2>().x;
    }
}
