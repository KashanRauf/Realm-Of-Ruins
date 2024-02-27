using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State : MonoBehaviour
{
    public float maxHealth = 50f;
    public float currentHealth;

    public bool CheckIfDead()
    {
        if (currentHealth <= 0f)
        {
            Destroy(gameObject);
            return true;
        }
        return false;
    }
}