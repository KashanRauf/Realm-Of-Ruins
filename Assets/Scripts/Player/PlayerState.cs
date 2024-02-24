using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    public float maxHealth { get; private set; } = 50f;
    public float currentHealth { get; private set; }
    public float damageMultiplier { get; private set; } = 1.0f;
    
    public int dashCharges { get; private set; } = 2;
    public float dashCooldownTime { get; private set; } = 5.0f;
    public float dashWait;


    // Use some file that contains the data to load
    public Weapon weapon { get; private set; }
    public Skill[] skills = new Skill[4];

    private void Awake()
    {
        currentHealth = maxHealth;
        // Change to instantiate a weapon as a child object depending on data
        weapon = GetComponentInChildren<Weapon>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
