using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaneOfEvil : Skill
{
    // Purity is to corruption what damage is to health
    public float purity = 4;
    public GameObject talismanPrefab;
    public Transform origin;

    //public override void Initialize()
    //{
    //    cooldownTime = 2f;
    //    wait = 0f;
    //    stacks = 3;
    //    usages = 0;
    //    combat = false;
    //}

    public override void Invoke()
    {
        // Debug.Log("Trying");
        if (usages >= stacks)
        {
            return;
        }

        // Instantiate a talisman and fire at the enemy, just a projectile
        talismanPrefab.GetComponent<Projectile>().damage = purity;
        GameObject talisman = Instantiate(talismanPrefab, origin.position, origin.rotation);
        Rigidbody2D projBody = talisman.GetComponent<Rigidbody2D>();
        talisman.transform.up = player.lookDir;
        projBody.AddForce(player.lookDir.normalized * 18, ForceMode2D.Impulse);

        usages++;
        // Debug.Log("Performed " + usages + "/" + stacks + " times");
        wait += cooldownTime;
    }
}