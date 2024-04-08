using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ZogState : EnemyState
{
    public GameObject exit;

    private void Awake()
    {
        currentHealth = maxHealth;
        healthBar.maxValue = maxHealth;
        healthBar.minValue = 0;
        corruption = maxCorruption;
        corruptionBar.maxValue = maxCorruption;
        corruptionBar.minValue = 0;
        exit.SetActive(false);
    }

    private void Update()
    {
        healthBar.value = currentHealth;
        corruptionBar.value = corruption;

        if (corruption <= 0)
        {
            Debug.Log("Should be pure");
            animator.SetTrigger("Purified");
        }

        if (currentHealth <= 0)
        {
            Destroy(gameObject);
            manager.killed++;
            exit.SetActive(true);
            Debug.Log("Killed: " + manager.killed);
        }

        // Will move to the game manager later
        CheckIfDead();
    }

    // Called as an event by the purify animation
    public void Purification()
    {
        Debug.Log("Attempting to destroy");
        Destroy(gameObject);
        manager.saved++;
        exit.SetActive(true);
        Debug.Log("Saved: " + manager.saved);
    }
}