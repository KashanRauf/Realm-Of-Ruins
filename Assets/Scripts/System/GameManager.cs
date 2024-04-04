using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

// Manages the player, loadouts, enemies and overall system when in the world.
public class GameManager : MonoBehaviour
{
    // Also manages the enemies that exist, will handle spawning
    public int saved = 0;
    public int killed = 0;
    public PlayerState player;
    public bool isPaused = false;
    public Slider healthBar;
    public TMP_Text history;
    public GameObject activeUI;
    public GameObject pauseUI;

    private void Awake()
    {
        activeUI.SetActive(true);
        pauseUI.SetActive(false);
    }

    private void Update()
    {
        if (player == null) return;
        //history.text = "Killed: " + killed + "\nSaved: " + saved;
        history.text = "";
        healthBar.maxValue = player.maxHealth;
        healthBar.minValue = 0;
        healthBar.value = player.currentHealth;

        player.CheckIfDead();

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }

    }

    void Pause()
    {
        isPaused = true;
        pauseUI.SetActive(true);
        activeUI.SetActive(false);
        Time.timeScale = 0f;
    }

    void Resume()
    {
        isPaused = false;
        pauseUI.SetActive(false);
        activeUI.SetActive(true);
        Time.timeScale = 1f;
    }

    public void ResumeButton()
    {
        Resume();
    }

    public void ReturnToTitle()
    {
        Debug.Log("Return to title");
        SceneManager.LoadScene("Main Menu");
    }

}

