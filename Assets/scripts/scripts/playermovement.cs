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

    [Header("Slide")]
    public float slideSpeed = 14f;
    public float slideDuration = 0.25f;
    private bool isSliding;
    private float slideTimer;
    private int slideDirection;

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
        // Ground check (are you ok ground?)
        isGrounded = Physics2D.OverlapCircle(
            groundCheck.position,
            groundCheckRadius,
            groundLayer
        );

        float xMove = Input.GetAxisRaw("Horizontal");

        // Slide movement (overrides normal movement)
        if (isSliding)
        {
            slideTimer -= Time.deltaTime;

            m_Rigidbody.linearVelocity = new Vector2(
                slideDirection * slideSpeed,
                m_Rigidbody.linearVelocity.y
            );

            if (slideTimer <= 0f)
            {
                isSliding = false;
            }
        }
        else
        {
            float currentSpeed = isCrouching ? crouchSpeed : m_Speed;

            m_Rigidbody.linearVelocity = new Vector2(
                xMove * currentSpeed,
                m_Rigidbody.linearVelocity.y
            );
        }

        // Sprite flip (disabled during slide to stop my brain hurting)
        if (!isSliding)
        {
            if (xMove > 0)
                spriteRenderer.flipX = false;
            else if (xMove < 0)
                spriteRenderer.flipX = true;
        }

        // Jump
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded && !isSliding && !isCrouching)
        {
            m_Rigidbody.AddForce(Vector2.up * m_JumpPower, ForceMode2D.Impulse);
        }

        HandleCrouchAndSlide();
    }

    void HandleCrouchAndSlide()
    {
        // Start crouch / slide (ground OR air slide :3)
        if (Input.GetKeyDown(KeyCode.S))
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

            if (Input.GetKey(KeyCode.D))
            {
                StartSlide(1);
            }
            else if (Input.GetKey(KeyCode.A))
            {
                StartSlide(-1);
            }
        }

        // Stop crouching
        if (Input.GetKeyUp(KeyCode.S))
        {
            isCrouching = false;

            boxCollider.size = originalColliderSize;
            boxCollider.offset = originalColliderOffset;
        }
    }

    void StartSlide(int direction)
    {
        if (isSliding) return;

        isSliding = true;
        slideTimer = slideDuration;
        slideDirection = direction;

        m_Rigidbody.linearVelocity = new Vector2(
            direction * slideSpeed,
            m_Rigidbody.linearVelocity.y
        );
    }
}