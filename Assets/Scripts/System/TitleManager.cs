using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

// Manages the player, loadouts, enemies and overall system when in the world.
public class TitleManager : MonoBehaviour
{
    public GameObject UI;
    public GameObject credits;
    private bool showingCredits = false;

    private void Awake()
    {
        UI.SetActive(true);
        credits.SetActive(false);
    }

    private void Update()
    {
        if (!showingCredits) return;

        if (Input.anyKeyDown)
        {
            credits.SetActive(false);
            showingCredits = false;
        }
    }

    public void Play()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void Credits()
    {
        credits.SetActive(true);
        showingCredits = true;
    }

    public void Quit()
    {
        Debug.Log("Quitting game");
        Application.Quit();
    }
}

