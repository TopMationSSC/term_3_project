using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{


    // Start is called before the first frame update

    // health info
    public int m_MaxHealth = 3;
    public int m_CurrentHealth;

    //variable for respawn function
    public Vector3 m_RespawnPosition;
    public float m_RespawnDelay = 0.6f;
    public Text m_HealthText;

    //reference to componants we need
    Rigidbody2D m_RB;
    PlayerMovement m_PlayerMovement;

    void Start()
    {
        m_CurrentHealth = m_MaxHealth;
        m_RB = GetComponent<Rigidbody2D>();
        m_PlayerMovement = GetComponent<PlayerMovement>();
        m_RespawnPosition = transform.position;
        UpdateHealthUI();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(int amount)
    {
        //reduce player health
        m_CurrentHealth -= amount;
        //update player ui
        UpdateHealthUI();
        //if health is below 0
        if(m_CurrentHealth <= 0)
        {
            StartCoroutine(RespawnCoroutine());
        }
    }

    public void Heal(int amount)
    {
        //increase current health
        m_CurrentHealth += amount;
        if(m_CurrentHealth > m_MaxHealth)
        {
            m_CurrentHealth = m_MaxHealth;
        }

        //update heatlh ui after heal
        UpdateHealthUI();
    }

    public void SetRespawnPosition(Vector3 newPos)
    {
        //set the respawb poistion to the position that is passed in
        m_RespawnPosition = newPos;
    }

    IEnumerator RespawnCoroutine()
    {
        if(m_PlayerMovement != null)
        {
            m_PlayerMovement.enabled = false;
        }

        if(m_RB != null)
        {
            m_RB.linearVelocity = Vector2.zero;
        }

        yield return new WaitForSeconds(m_RespawnDelay);

        transform.position = m_RespawnPosition;
        m_CurrentHealth = m_MaxHealth;
        UpdateHealthUI();

        if(m_PlayerMovement != null)
        {
            m_PlayerMovement.enabled = true;
        }


    }

    void UpdateHealthUI()
    {
        if(m_HealthText != null)
        {
            m_HealthText.text = "health: " + m_CurrentHealth;
        }
    }
}
