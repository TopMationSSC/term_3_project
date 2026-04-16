using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HazardSCript : MonoBehaviour
{
    // Start is called before the first frame update (ring ring...is your start running? then you better catch it!)

    public int m_Damage = 1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerHealth playerHealth = collision.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(m_Damage);
            }
        }
    }

}
