using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAwareness : MonoBehaviour
{
    public bool isAware { get; private set; }
    public Vector2 distanceFromPlayer {  get; private set; }
    public Vector2 playerDir { get; private set; }
    [SerializeField]
    private float detectRange;
    private Transform player;

    private void Awake() 
    {
        // Gets the player's transform
        // FindObjectOfType is not recommended for repeated use, okay to use in methods like Awake() but not Update()
        player = FindObjectOfType<PlayerMovement>().transform;
        Debug.Log("Player: " + player);
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null) return;
        distanceFromPlayer = player.position - this.transform.position;
        playerDir = distanceFromPlayer.normalized;

        // If the enemy's distance is within detection range, it is aware of the player
        isAware = distanceFromPlayer.magnitude <= detectRange;
    }
}
