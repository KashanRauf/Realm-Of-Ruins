using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ZogState : EnemyState
{


    private void Awake()
    {
        maxHealth = 60f;
        maxCorruption = 20f;
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

        if (currentHealth <= 0)
        {
            Destroy(gameObject);
            manager.killed++;
        }

        // Will move to the game manager later
        bool isDead = CheckIfDead();
        if (isDead || corruption <= 0)
        {
            Destroy(gameObject);
        }
    }

    // Called as an event by the purify animation
    new public void OnPurificationEnd()
    {
        Destroy(gameObject);
        manager.saved++;
    }
}