using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

// Manages the player, loadouts, enemies and overall system when in the world.
public class GameManager : MonoBehaviour
{
    // Also manages the enemies that exist, will handle spawning
    public int saved = 0;
    public int killed = 0;
    public PlayerState player;
    public bool isPaused;
    public Slider healthBar;
    public TMP_Text history;

    // public GameObject (should be a list or sm idk how to manage enemies yet)

    private void Update()
    {
        history.text = "Killed: " + killed + "\nSaved: " + saved;
        healthBar.maxValue = player.maxHealth;
        healthBar.minValue = 0;
        healthBar.value = player.currentHealth;

        player.CheckIfDead();
    }
}

