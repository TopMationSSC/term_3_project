using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class QuestionBlock : MonoBehaviour
{
    public Sprite usedBlockSprite;
    public GameObject itemToSpawn;
    public Transform spawnPoint;

    public bool isCorrectAnswer = false; // set to true for the right block
    public Tilemap wallTilemap; // i need to drag the Tilemap wall in the Inspector
    public int damageOnWrongAnswer = 1; // Damage to the player if wrong block is hit

    private bool isUsed = false;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isUsed) return;

        if (collision.CompareTag("Player"))
        {
            Vector2 contactPoint = collision.ClosestPoint(transform.position);
            if (contactPoint.y < transform.position.y)
            {
                ActivateBlock(collision);
            }
        }
    }

    void ActivateBlock(Collider2D collision)
    {
        isUsed = true;
        spriteRenderer.sprite = usedBlockSprite;

        if (itemToSpawn != null && spawnPoint != null)
        {
            Instantiate(itemToSpawn, spawnPoint.position, Quaternion.identity);
        }

        if (isCorrectAnswer)
        {
            if (wallTilemap != null)
            {
                wallTilemap.ClearAllTiles(); // Destroys the wall
            }
        }
        else
        {
            // Apply damage to player
            PlayerHealth playerHealth = collision.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damageOnWrongAnswer);
            }
        }
    }
}
