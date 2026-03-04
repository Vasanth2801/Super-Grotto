using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private int facingDirection = 1;
    [SerializeField] private Vector2 deathKick = new Vector2(10f, 10f);
    [SerializeField] private bool isAlive = true;

    [Header("Climbing Settings")]
    [SerializeField] private float climbSpeed = 5f;
    [SerializeField] private float gravityScaleAtStart;

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
    [SerializeField] private float moveInputX;
    [SerializeField] private float moveInputY;

    void Start()
    {
        gravityScaleAtStart = rb.gravityScale;
    }

    void Update()
    {
        if(!isAlive)
        {
            return;
        }

        moveInputX = Input.GetAxis("Horizontal");
        moveInputY = Input.GetAxis("Vertical");

        Jump();

        if(moveInputX > 0 && transform.localScale.x < 0 || moveInputX < 0 && transform.localScale.x > 0)
        {
            Flip();
        }

        ClimbLadder();

        HandleAnimations();

        Death();
    }

    void FixedUpdate()
    {
        if (!isAlive)
        {
            return;
        }
        rb.linearVelocity = new Vector2(moveInputX * speed, rb.linearVelocity.y);
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

    void ClimbLadder()
    {
        if(!rb.IsTouchingLayers(LayerMask.GetMask("Climbing")))
        {
            rb.gravityScale = gravityScaleAtStart;
            animator.SetBool("isClimbing", false);
            return;
        }

        Vector2 climbVelocity = new Vector2(rb.linearVelocity.x, moveInputY * climbSpeed);
        rb.linearVelocity = climbVelocity;
        rb.gravityScale = 0;

        bool playerHasVerticalSpeed = Mathf.Abs(rb.linearVelocity.y) > Mathf.Epsilon;
        animator.SetBool("isClimbing", playerHasVerticalSpeed);
    }

    void  HandleAnimations()
    {
        bool isMoving = Mathf.Abs(moveInputX) > 0 && isGrounded;

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