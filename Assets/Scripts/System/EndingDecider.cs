using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingDecider : MonoBehaviour
{
    public GameManager manager;

    private void Update()
    {
        if (manager.killed > manager.saved)
        {
            gameObject.GetComponent<LevelTransition>().scene = "BrutalistEnding";
        } else
        {
            gameObject.GetComponent<LevelTransition>().scene = "PacifistEnding";
        }
    }
}
