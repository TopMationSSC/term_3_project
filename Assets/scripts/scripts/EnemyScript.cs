using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public Transform pointA, pointB;
    public float speed = 2f;
    public int damage = 1;

    private Vector3 target;

    void Start()
    {
        target = pointB.position;
    }

    void Update()
    {
        // Patrol movement
        transform.position = Vector3.MoveTowards(
            transform.position,
            target,
            speed * Time.deltaTime
        );

        if (Vector3.Distance(transform.position, target) < 0.1f)
        {
            target = (target == pointA.position) ? pointB.position : pointA.position;
        }
    }

    //  DAMAGE PLAYER ON TOUCH
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerHealth playerHealth =
                collision.gameObject.GetComponent<PlayerHealth>();

            if (playerHealth != null)
            {
                // Pass enemy position for directional knockback
                playerHealth.TakeDamage(damage, transform.position);
            }
        }
    }
}