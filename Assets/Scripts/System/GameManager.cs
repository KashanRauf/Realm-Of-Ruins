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
    public GameObject gameOverUI;
    public string nextLevel;
    public string currentLevel;


    private void Awake()
    {
        gameOverUI.SetActive(false);
        Resume();
    }

    private void Update()
    {
        if (player == null) return;
        //history.text = "Killed: " + killed + "\nSaved: " + saved;
        history.text = "";
        healthBar.maxValue = player.maxHealth;
        healthBar.minValue = 0;
        healthBar.value = player.currentHealth;

        if (player.CheckIfDead())
        {
            // Trigger game over screen
            gameOverUI.SetActive(true);
            Time.timeScale = 0.0f;
            isPaused = true;
            return;
        }

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

    public void Pause()
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
        SceneManager.LoadScene("MainMenu");
    }

    public void Restart()
    {
        SceneManager.LoadScene(currentLevel);
    }

    public void OnDisable()
    {
        PlayerPrefs.SetInt("Saved", saved);
        PlayerPrefs.SetInt("Killed", killed);
        PlayerPrefs.SetString("Next", nextLevel);
        Debug.Log("Dis Saved: " + saved + ", Killed: " + killed + ", Next: " + nextLevel);
    }

    public void OnEnable()
    {
        saved = PlayerPrefs.GetInt("Saved");
        killed = PlayerPrefs.GetInt("Killed");
        nextLevel = PlayerPrefs.GetString("Next");
        Debug.Log("En Saved: " + saved + ", Killed: " + killed + ", Next: " + nextLevel);
    }

}

