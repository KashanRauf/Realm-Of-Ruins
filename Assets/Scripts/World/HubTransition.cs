using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class HubTransition : MonoBehaviour
{
    public GameObject prompt;
    public string scene;
    private float time = 0f;
    private bool standing = false;
    public GameManager manager;

    private void Awake()
    {
        prompt.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        standing = true;
        prompt.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        standing = false;
        prompt.SetActive(false);
    }

    private void Update()
    {
        scene = manager.nextLevel;
        Debug.Log("Should transfer to " +  scene);
        if (!standing)
        {
            time = 0f;
            return;
        }

        time += Time.deltaTime;
        if (time >= 2) SceneManager.LoadScene(scene);
    }
}
