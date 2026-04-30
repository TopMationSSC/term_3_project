using UnityEngine;

public class HazardScriptSpike : MonoBehaviour
{
    public int m_Damage = 1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
            return;

        PlayerHealth playerHealth = collision.GetComponent<PlayerHealth>();

        if (playerHealth != null)
        {
            // Pass spike position so directional knockback works
            playerHealth.TakeDamage(m_Damage, transform.position);
        }
    }
}