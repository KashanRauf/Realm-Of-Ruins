using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.XR;

public class PlayerState : State
{

    public float damageMultiplier { get; private set; } = 1.0f;

    public int maxDashes = 2;
    public int dashesUsed;
    public float dashCooldownTime { get; private set; } = 3.50f;
    public float dashWait;
    public bool isDashing = false;
    public bool skillOccupied = false;

    // Use some file that contains the data to load
    public Weapon weapon { get; private set; }
    public Skill[] skills = new Skill[4];

    private void Awake()
    {
        maxHealth = 50f;
        currentHealth = maxHealth;
        dashesUsed = 0;

        // Change to instantiate a weapon as a child object depending on data
        weapon = GetComponentInChildren<Weapon>();

        for (int i = 0; i < 4; i++)
        {
            if (skills[i] != null)
            {
                //skills[i].Initialize();
                skills[i].icon.fillAmount = 0;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (skillOccupied)
        {
            return;
        }
        // Using if, else-if instead of multiple ifs -> Only one skill can be invoked per frame
        if (Input.GetKeyDown(KeyCode.Alpha1) && skills[0] != null)
        {
            Debug.Log("Attempting skill");
            skills[0].Invoke();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2) && skills[1] != null)
        {
            skills[1].Invoke();
        } 
        else if (Input.GetKeyDown(KeyCode.Alpha3) && skills[2] != null)
        {
            skills[2].Invoke();
        } 
        else if (Input.GetKeyDown(KeyCode.Alpha4) && skills[3] != null)
        {
            skills[3].Invoke();
        }

        // Should later include little dots indicating # skill stacks available
        // Recharges skill stacks, also update UI components
        // Could probably assign skills[i] to a var called skill but i dont feel like checking if the copies are shallow or deep
        for (int i = 0; i < 4; i++)
        {
            if (skills[i] == null)
            {
                break;
            }

            if (skills[i].usages > 0)
            {
                // Debug.Log("Recharging: " + skills[i].wait + ", Stacks: " + skills[i].usages);
                skills[i].wait -= Time.deltaTime;
                if (skills[i].wait <= skills[i].cooldownTime * (skills[i].usages-1))
                {
                    skills[i].usages--;
                }
                Debug.Log(skills[i].icon.fillAmount);
                skills[i].icon.fillAmount = skills[i].CooldownFillBar();
                Debug.Log(skills[i].icon.fillAmount);
                // Debug.Log("Recharged to: " + skills[i].wait);
            }
        }
    }

    // Override CheckIfDead() to trigger a game over instead
}
