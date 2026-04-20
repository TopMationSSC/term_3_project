using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float lifetime = 2f;

    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Later: damage logic
        Destroy(gameObject);
    }
}