using UnityEngine;

public class Coin : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public int m_Value = 1;
    [SerializeField] private AudioSource m_Ding;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // only trigger if the player touches the coin :3
        if (collision.CompareTag("Player"))
        {
            // find the score manager nad add points 
            Scoremanager scoremanager = FindObjectOfType<Scoremanager>();

            if(scoremanager != null )
            {
                scoremanager.AddScore(m_Value);
            }

            m_Ding.Play();
            // destroy the coin so it dissapears (nooo wallstreet crash)
            Destroy(gameObject);


        }
    }

}
