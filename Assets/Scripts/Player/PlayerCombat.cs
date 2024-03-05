using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    private PlayerState player;

    private void Awake()
    {
        // Manually setting for now
        player = GetComponent<PlayerState>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetButtonDown("Fire1"))
        {
            player.weapon.Attack();
        }
        else if (Input.GetButtonDown("Fire2"))
        {
            player.weapon.AltAttack();
        }
    }
}
