using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowingKnives : Skill
{
    public GameObject bulletManager;
    public GameObject knife;
    public GameManager gameManager;

    public (
        float,
        Vector2,
        Vector2,
        GameObject,
        int,
        int,
        int,
        float,
        float,
        float,
        float
    ) attack;

    private void Awake()
    {
        Projectile proj = knife.GetComponent<Projectile>();
        if (gameManager.killed > gameManager.saved)
        {
            proj.purify = false;
        } else
        {
            proj.purify = true;
        }
    }

    public override void Invoke()
    {
        if (usages >= stacks)
        {
            return;
        }

        Debug.Log("Throwing knives");
        attack = (5.0f, transform.parent.position, player.lookDir.normalized, knife, 0, 3, 1, 5f, 0f, 6f, 0);
        GameObject attackInstance = Instantiate(bulletManager);
        attackInstance.GetComponent<BulletManager>().Initialize(attack);

        wait += cooldownTime;
        usages++;
    }
}
