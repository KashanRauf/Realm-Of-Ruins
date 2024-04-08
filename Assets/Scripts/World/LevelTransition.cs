using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LevelTransition : MonoBehaviour
{
    public GameObject prompt;
    public string scene;
    private float time = 0f;
    private bool standing = false;
    public string nextHub;
    public GameManager manager;

    private void Awake()
    {
        prompt.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        standing = true;
        prompt.SetActive(true);

        //Debug.Log(key);
        //if (key)
        //{
        //    Debug.Log("Attempted to leave");
        //    SceneManager.LoadScene(scene);
        //}
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        standing = false;
        prompt.SetActive(false);
    }

    private void Update()
    {
        if (!standing)
        {
            time = 0f;
            return;
        }

        time += Time.deltaTime;
        if (time >= 2)
        {
            manager.nextLevel = nextHub;
            SceneManager.LoadScene(scene);
        }
    }
}
