using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockSkill : MonoBehaviour
{
    public LayerMask player;
    // Between 0 and 3
    public int slot;
    public PlayerState playerState;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject touch = collision.gameObject;

        Debug.Log("Pickup " + touch.name);
        if (1 << touch.layer == player)
        {
            Unlock();
        }
    }

    private void Unlock()
    {
        playerState.skills[slot].isActive = true;
        playerState.skills[slot].fillBar = playerState.fillBars[slot];
        playerState.skills[slot].fillBar.fillAmount = 0;
        playerState.icons[slot].sprite = playerState.activeIcons[slot];
        this.GetComponent<AudioSource>().Play();
        Debug.Log("Sprint: " + playerState.skills[slot].isActive);
        Destroy(gameObject);
    }
}
