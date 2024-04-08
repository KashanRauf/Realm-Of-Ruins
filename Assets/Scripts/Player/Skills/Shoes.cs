using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoes : Skill
{
    public float multiplier;
    public float duration;
    public float timeLeft;
    public bool inUse;
    public override void Invoke()
    {
        if (usages >= stacks)
        {
            return;
        }

        Debug.Log("Sprinting");
        this.GetComponent<AudioSource>().Play();
        timeLeft = duration;
        inUse = true;

        wait += cooldownTime;
        usages++;
    }

    private void Update()
    {
        if (!isActive) return;
        wait -= Time.deltaTime;
        timeLeft -= Time.deltaTime;

        if (timeLeft <= 0)
        {
            player.speedUp = 1f;
            inUse = false;
            return;
        }

        player.speedUp = multiplier;
    }
}
