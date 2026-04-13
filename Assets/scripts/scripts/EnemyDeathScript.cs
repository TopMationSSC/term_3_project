using UnityEngine;

public class EnemyHeadTrigger2D : MonoBehaviour
{
    public GameObject enemy; // Assign the enemy GameObject in the Inspector

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Optional: Add bounce effect
            Rigidbody2D rb = collision.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.velocity = new Vector2(rb.velocity.x, 10f); // Adjust bounce force
            }

            Destroy(enemy); // Kill the enemy
        }
    }
}
