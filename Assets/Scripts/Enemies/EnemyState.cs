using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyState : State
{
    public GameManager manager;
    public float damageOnAttack = 4f;
    public float stunTime = 0f;
    public float maxCorruption = 6f;
    public float corruption;
    public Slider healthBar;
    public Slider corruptionBar;
    public Animator animator;

    private void Awake()
    {
        maxHealth = 20f;
        currentHealth = maxHealth;
        healthBar.maxValue = maxHealth;
        healthBar.minValue = 0;
        corruption = maxCorruption;
        corruptionBar.maxValue = maxCorruption;
        corruptionBar.minValue = 0;
    }

    private void Update()
    {
        healthBar.value = currentHealth;
        corruptionBar.value = corruption;

        if (corruption <= 0)
        {
            animator.SetTrigger("Purified");
        }

        if (currentHealth <= 0)
        {
            Destroy(gameObject);
            manager.killed++;
        }

        // Will move to the game manager later
        CheckIfDead();
    }

    // Called as an event by the purify animation
    public void OnPurificationEnd()
    {
        Destroy(gameObject);
        manager.saved++;
    }
}