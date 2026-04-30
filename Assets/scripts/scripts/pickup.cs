using UnityEngine;

public class ShootPickup : MonoBehaviour
{
    private bool playerInRange;

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            PlayerShoot playerShoot = Object.FindFirstObjectByType<PlayerShoot>();
            playerShoot.canShoot = true;

            Destroy(gameObject); // remove pickup
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }
}