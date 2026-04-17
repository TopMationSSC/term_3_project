using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float m_Speed = 10f;
    public float m_JumpPower = 10f;

    [Header("Ground Check")]
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;

    [Header("Crouch")]
    public float crouchSpeed = 4f;
    public float crouchHeight = 0.5f;
    private bool isCrouching;

    private Rigidbody2D m_Rigidbody;
    private BoxCollider2D boxCollider;
    private SpriteRenderer spriteRenderer;
    private bool isGrounded;

    private Vector2 originalColliderSize;
    private Vector2 originalColliderOffset;

    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        originalColliderSize = boxCollider.size;
        originalColliderOffset = boxCollider.offset;
    }

    void Update()
    {
        // Ground check (everyone asks what is ground? but never how is ground?)
        isGrounded = Physics2D.OverlapCircle(
            groundCheck.position,
            groundCheckRadius,
            groundLayer
        );

        // Horizontal movement
        float xMove = Input.GetAxisRaw("Horizontal");
        float currentSpeed = isCrouching ? crouchSpeed : m_Speed;

        m_Rigidbody.linearVelocity = new Vector2(
            xMove * currentSpeed,
            m_Rigidbody.linearVelocity.y
        );

        // Sprite flip
        if (xMove > 0)
        {
            spriteRenderer.flipX = false; // facing right
        }
        else if (xMove < 0)
        {
            spriteRenderer.flipX = true; // facing left
        }

        // Jump
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded && !isCrouching)
        {
            m_Rigidbody.AddForce(Vector2.up * m_JumpPower, ForceMode2D.Impulse);
        }

        HandleCrouch();
    }

    void HandleCrouch()
    {
        // Start crouching
        if (Input.GetKeyDown(KeyCode.S) && isGrounded)
        {
            isCrouching = true;

            boxCollider.size = new Vector2(
                originalColliderSize.x,
                crouchHeight
            );

            boxCollider.offset = new Vector2(
                originalColliderOffset.x,
                originalColliderOffset.y - (originalColliderSize.y - crouchHeight) / 2f
            );
        }

        // Stop crouching
        if (Input.GetKeyUp(KeyCode.S))
        {
            isCrouching = false;

            boxCollider.size = originalColliderSize;
            boxCollider.offset = originalColliderOffset;
        }
    }
}