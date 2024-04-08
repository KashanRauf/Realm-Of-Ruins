using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetStats : MonoBehaviour
{
    public GameManager manager;


    private void Start()
    {
        manager.killed = 0;
        manager.saved = 0;
    }

}
