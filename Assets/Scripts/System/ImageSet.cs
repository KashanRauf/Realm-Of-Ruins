using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ImageSet : MonoBehaviour
{
    public Image image;
    public Sprite[] images;
    int counter = 0;

    private void Awake()
    {
        image.sprite = images[0];
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if (counter < images.Length)
            {
                counter++;
                image.sprite = images[counter];
            } else
            {
                SceneManager.LoadScene("MainMenu");
            }
        }
    }
}
