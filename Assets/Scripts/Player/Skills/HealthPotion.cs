using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPotion : Skill
{
    public override void Invoke()
    {
        if (usages >= stacks)
        {
            return;
        }

        Debug.Log("Healing player");
        this.GetComponent<AudioSource>().Play();
        state.currentHealth += 10;

        wait += cooldownTime;
        usages++;
    }
}
