using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class IgnorePlayerCollision2D : MonoBehaviour
{
    private void Start()
    {
        BoxCollider2D myCollider = GetComponent<BoxCollider2D>();

        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        foreach (GameObject player in players)
        {
            Collider2D playerCollider = player.GetComponent<Collider2D>();
            if (playerCollider != null)
            {
                Physics2D.IgnoreCollision(myCollider, playerCollider);
            }
        }
    }
}