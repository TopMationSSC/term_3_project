using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    // health info
    public int m_MaxHealth = 3;
    public int m_CurrentHealth;

    // respawn
    public Vector3 m_RespawnPosition;
    public float m_RespawnDelay = 0.6f;
    public Text m_HealthText;

    // damage feedback
    public float m_KnockbackForce = 8f;
    public float m_DamageFlashTime = 0.1f;
    public float m_InvincibilityTime = 0.5f;
    public float m_HurtLockTime = 0.2f;


    // references
    Rigidbody2D m_RB;
    PlayerMovement m_PlayerMovement;
    SpriteRenderer m_Sprite;

    bool m_Invincible;

    void Start()
    {
        m_CurrentHealth = m_MaxHealth;
        m_RB = GetComponent<Rigidbody2D>();
        m_PlayerMovement = GetComponent<PlayerMovement>();
        m_Sprite = GetComponent<SpriteRenderer>();

        m_RespawnPosition = transform.position;
        UpdateHealthUI();
    }

    // DIRECTIONAL DAMAGE
    public void TakeDamage(int amount, Vector2 damageSourcePosition)
    {
        if (m_Invincible)
            return;

        m_CurrentHealth -= amount;
        UpdateHealthUI();

        if (m_RB != null)
        {
            // determine direction away from enemy
            float direction = transform.position.x > damageSourcePosition.x ? 1f : -1f;

            // reset velocity for consistent knockback
            m_RB.linearVelocity = Vector2.zero;

            // apply knockback (side + up)
            Vector2 knockback = new Vector2(
                direction * (m_KnockbackForce * 0.6f),
                m_KnockbackForce
            );

            m_RB.AddForce(knockback, ForceMode2D.Impulse);
        }

        StartCoroutine(DamageFlash());
        StartCoroutine(InvincibilityCoroutine());
        StartCoroutine(HurtLockCoroutine());

        if (m_CurrentHealth <= 0)
        {
            StartCoroutine(RespawnCoroutine());
        }
    }

    public void Heal(int amount)
    {
        m_CurrentHealth += amount;
        if (m_CurrentHealth > m_MaxHealth)
            m_CurrentHealth = m_MaxHealth;

        UpdateHealthUI();
    }

    public void SetRespawnPosition(Vector3 newPos)
    {
        m_RespawnPosition = newPos;
    }

    IEnumerator RespawnCoroutine()
    {
        if (m_PlayerMovement != null)
            m_PlayerMovement.enabled = false;

        if (m_RB != null)
            m_RB.linearVelocity = Vector2.zero;

        yield return new WaitForSeconds(m_RespawnDelay);

        transform.position = m_RespawnPosition;
        m_CurrentHealth = m_MaxHealth;
        UpdateHealthUI();

        if (m_PlayerMovement != null)
            m_PlayerMovement.enabled = true;
    }

    IEnumerator DamageFlash()
    {
        if (m_Sprite != null)
        {
            m_Sprite.color = Color.red;
            yield return new WaitForSeconds(m_DamageFlashTime);
            m_Sprite.color = Color.white;
        }
    }

    IEnumerator InvincibilityCoroutine()
    {
        m_Invincible = true;
        yield return new WaitForSeconds(m_InvincibilityTime);
        m_Invincible = false;
    }

    void UpdateHealthUI()
    {
        if (m_HealthText != null)
        {
            m_HealthText.text = "health: " + m_CurrentHealth;
        }
    }

    internal void TakeDamage(int damageOnWrongAnswer)
    {
        throw new NotImplementedException();
    }
    IEnumerator HurtLockCoroutine()
    {
        if (m_PlayerMovement != null)
            m_PlayerMovement.enabled = false;

        yield return new WaitForSeconds(m_HurtLockTime);

        if (m_PlayerMovement != null)
            m_PlayerMovement.enabled = true;
    }
}
