using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HazardScriptSpike : MonoBehaviour
{
    public int m_Damage = 1;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerHealth playerHealth =
                collision.gameObject.GetComponent<PlayerHealth>();

            if (playerHealth != null)
            {
                playerHealth.TakeDamage(m_Damage);
            }
        }
    }
}