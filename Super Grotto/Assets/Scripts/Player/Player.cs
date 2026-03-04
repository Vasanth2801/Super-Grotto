using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private int facingDirection = 1;
    [SerializeField] private Vector2 deathKick = new Vector2(10f, 10f);
    [SerializeField] private bool isAlive = true;

    [Header("Ground Check Settings")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundRadius;
    [SerializeField] private LayerMask groundLayerMask;
    [SerializeField] private bool isGrounded;

    [Header("References")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator animator;
    [SerializeField] private Transform firePointTransform;

    [Header("Inputs")]
    [SerializeField] private float moveInput;

    void Update()
    {
        if(!isAlive)
        {
            return;
        }

        moveInput = Input.GetAxis("Horizontal");

        Jump();

        if(moveInput > 0 && transform.localScale.x < 0 || moveInput < 0 && transform.localScale.x > 0)
        {
            Flip();
        }

        HandleAnimations();

        Death();
    }

    void FixedUpdate()
    {
        if (!isAlive)
        {
            return;
        }
        rb.linearVelocity = new Vector2(moveInput * speed, rb.linearVelocity.y);
    }

    void Jump()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, groundLayerMask);

        if(Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }
    }

    void Flip()
    {
        facingDirection *= -1;
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
        firePointTransform.Rotate(0f, 180f, 0f);
    }

    void  HandleAnimations()
    {
        bool isMoving = Mathf.Abs(moveInput) > 0 && isGrounded;

        animator.SetBool("isIdling", !isMoving && isGrounded);
        animator.SetBool("isRunning", isMoving && isGrounded);
        animator.SetBool("isJumping", rb.linearVelocity.y > 0.1);
    }

    void Death()
    {
        if(rb.IsTouchingLayers(LayerMask.GetMask("Enemies")))
        {
            isAlive = false;
            rb.linearVelocity = deathKick;
            animator.SetTrigger("Death");
        }
    }
}