using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Horizontal movement")]
    [SerializeField]
    private float moveSpeed = 10f;
    [SerializeField]
    private Vector2 direction;

    [Header("Jumps")]
    [SerializeField]
    private LayerMask groundLayer;
    [SerializeField]
    private float groundLength = 0.8f;
    [SerializeField]
    private float jumpSpeed = 15f;
    [SerializeField]
    private float gravity = 1f;
    [SerializeField]
    private float fallMultiplier = 5f;
    [SerializeField]
    private float jumpDelay = 0.25f;

    [Header("Physics")]
    [SerializeField]
    private float maxSpeed = 7f;
    [SerializeField]
    private float linearDrag = 4f;
    [SerializeField]
    private Vector3 colliderOffset;

    private Animator animator;
    private Rigidbody2D rb;

    private bool facingRight = true;
    private bool onGround;

    private float jumpTimer;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        direction = new Vector2(horizontalInput, verticalInput);

        onGround = Physics2D.Raycast(transform.position + colliderOffset, Vector2.down, groundLength, groundLayer) ||
            Physics2D.Raycast(transform.position - colliderOffset, Vector2.down, groundLength, groundLayer);

        if (Input.GetButtonDown("Jump"))
        {
            jumpTimer = Time.time + jumpDelay;
        }
    }

    private void FixedUpdate()
    {
        MoveCharacter(direction.x);
        if (jumpTimer > Time.time && onGround)
        {
            Jump();
        }
        ModifyPhysics();
    }

    private void MoveCharacter(float horizontalInput)
    {
        rb.AddForce(Vector2.right * horizontalInput * moveSpeed);

        ChangeAnimations(rb.velocity.x, rb.velocity.y);

        if ((horizontalInput > 0 && !facingRight) || (horizontalInput < 0 && facingRight))
        {
            Flip();
        }

        if (Mathf.Abs(rb.velocity.x) > maxSpeed)
        {
            rb.velocity = new Vector2(Mathf.Sign(rb.velocity.x) * maxSpeed, rb.velocity.y);
        }
    }

    private void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, 0);
        rb.AddForce(Vector2.up * jumpSpeed, ForceMode2D.Impulse);
        jumpTimer = 0;
    }

    private void ModifyPhysics()
    {
        if (onGround)
        {
            ModifyRunPhysics();
        }
        else
        {
            ModifyJumpPhysics();
        }
    }

    private void ModifyRunPhysics()
    {
        bool changingDirections = (direction.x > 0 && rb.velocity.x < 0) || (direction.x < 0 && rb.velocity.x > 0);

        if (Mathf.Abs(direction.x) < 0.4f || changingDirections)
        {
            rb.drag = linearDrag;
        }
        else
        {
            rb.drag = 0;
        }
        rb.gravityScale = 0;
    }

    private void ModifyJumpPhysics()
    {
        rb.gravityScale = gravity;
        rb.drag = linearDrag * 0.15f;

        if (rb.velocity.y < 0)
        {
            rb.gravityScale = gravity * fallMultiplier;
        }
        else if (rb.velocity.y > 0 && !Input.GetButton("Jump"))
        {
            rb.gravityScale = gravity * (fallMultiplier / 2);
        }
    }

    private void ChangeAnimations(float horizontalInput, float verticalInput)
    {
        animator.SetFloat("horizontal", Mathf.Abs(horizontalInput));
        animator.SetFloat("vertical", Mathf.Abs(verticalInput));
    }

    private void Flip()
    {
        facingRight = !facingRight;

        transform.rotation = Quaternion.Euler(0, facingRight ? 0 : 180, 0);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position + colliderOffset, transform.position + colliderOffset + Vector3.down * groundLength);
        Gizmos.DrawLine(transform.position - colliderOffset, transform.position - colliderOffset + Vector3.down * groundLength);
    }
}