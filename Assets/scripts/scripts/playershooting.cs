using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform firePoint;
    public float fireCooldown = 0.3f;

    public bool canShoot = false;

    private float fireTimer;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        fireTimer -= Time.deltaTime;

        if (canShoot && Input.GetMouseButtonDown(0) && fireTimer <= 0f)
        {
            Shoot();
            fireTimer = fireCooldown;
        }
    }

    void Shoot()
    {
        float direction = spriteRenderer.flipX ? -1f : 1f;

        GameObject projectile = Instantiate(
            projectilePrefab,
            firePoint.position,
            Quaternion.identity
        );

        projectile.GetComponent<Rigidbody2D>().linearVelocity =
            new Vector2(direction * 12f, 0f);
    }
}