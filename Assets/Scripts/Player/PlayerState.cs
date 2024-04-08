using UnityEngine;
using UnityEngine.UI;

public class PlayerState : State
{

    public float damageMultiplier { get; private set; } = 1.0f;

    public int maxDashes = 2;
    public int dashesUsed;
    public float dashCooldownTime { get; private set; } = 2.20f;
    public float dashWait;
    public bool isDashing = false;
    public bool skillOccupied = false;

    // Use some file that contains the data to load
    public Weapon weapon { get; private set; }
    public Skill[] skills = new Skill[4];

    public Image[] fillBars;
    public Image[] icons;
    public Sprite[] activeIcons;

    private void Awake()
    {
        maxHealth = 50f;
        currentHealth = maxHealth;
        dashesUsed = 0;

        // Change to instantiate a weapon as a child object depending on data
        weapon = GetComponentInChildren<Weapon>();

        for (int i = 0; i < 4; i++)
        {
            if (skills[i] == null) break;
            if (skills[i].isActive)
            {
                //skills[i].Initialize();
                skills[i].fillBar = fillBars[i];
                skills[i].fillBar.fillAmount = 0;
                icons[i].sprite = activeIcons[i];
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (currentHealth > maxHealth) currentHealth = maxHealth;

        if (skillOccupied) return;

        // Using if, else-if instead of multiple ifs -> Only one skill can be invoked per frame
        if (Input.GetKeyDown(KeyCode.Alpha1) && skills[0].isActive)
        {
            Debug.Log("Attempting skill 1");
            skills[0].Invoke();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2) && skills[1].isActive)
        {
            Debug.Log("Attempting skill 2");
            skills[1].Invoke();
        } 
        else if (Input.GetKeyDown(KeyCode.Alpha3) && skills[2].isActive)
        {
            Debug.Log("Attempting skill 3");
            skills[2].Invoke();
        } 
        else if (Input.GetKeyDown(KeyCode.Alpha4) && skills[3].isActive)
        {
            Debug.Log("Attempting skill 4");
            skills[3].Invoke();
        }

        // Should later include little dots indicating # skill stacks available
        // Recharges skill stacks, also update UI components
        // Could probably assign skills[i] to a var called skill but i dont feel like checking if the copies are shallow or deep
        for (int i = 0; i < 4; i++)
        {
            if (skills[i] == null) break;
            if (!skills[i].isActive) break;

            if (skills[i].usages > 0)
            {
                // Debug.Log("Recharging: " + skills[i].wait + ", Stacks: " + skills[i].usages);
                skills[i].wait -= Time.deltaTime;
                if (skills[i].wait <= skills[i].cooldownTime * (skills[i].usages-1))
                {
                    skills[i].usages--;
                }
                Debug.Log(skills[i].fillBar.fillAmount);
                skills[i].fillBar.fillAmount = skills[i].CooldownFillBar();
                Debug.Log(skills[i].fillBar.fillAmount);
                // Debug.Log("Recharged to: " + skills[i].wait);
            }
        }
    }

    // Override CheckIfDead() to trigger a game over instead
    new public bool CheckIfDead()
    {
        return currentHealth <= 0;
    }
}
