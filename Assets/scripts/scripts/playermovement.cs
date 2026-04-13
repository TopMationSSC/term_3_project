using UnityEngine;


public class PlayerMovement : MonoBehaviour
{
    public float m_Speed = 10.0f;
    public float m_JumpPower = 10f;
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;

    private Rigidbody2D m_Rigidbody;
    private bool isGrounded;

    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float xMove = Input.GetAxisRaw("Horizontal");
        m_Rigidbody.velocity = new Vector2(xMove * m_Speed, m_Rigidbody.velocity.y);

        // Check if player is grounded
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        // Jump only if grounded
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            m_Rigidbody.AddForce(Vector2.up * m_JumpPower, ForceMode2D.Impulse);
        }

        // Optional: Fast fall
        if (Input.GetKey(KeyCode.S))
        {
            m_Rigidbody.AddForce(Vector2.down * m_JumpPower);
        }
    }
}

