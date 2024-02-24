using System.Collections;
using System.Collections.Generic;
using UnityEngine;



// GIVE THE SHIELD A BOX COLLIDER ???
public class Shield : Weapon
{

    protected float radius = 0.8f;
    protected Transform pivotPoint;
    protected PlayerMovement playerMovement;
    protected PlayerState player;
    public Animator animator;
    public float stunTime;
    public Transform bashPoint;
    public Vector2 bashRange = new Vector2(1f, 1f);
    public LayerMask blockable;
    public LayerMask bashable;

    private void Awake()
    {
        weaponType = 1;
        stunTime = 2.0f;
        cooldownTime = 1.2f;
        playerMovement = GetComponentInParent<PlayerMovement>();
        player = GetComponentInParent<PlayerState>();
        pivotPoint = transform.parent;

        transform.position += Vector3.up * radius;
    }

    void Update()
    {
        // Follows the mouse in a circle around the player
        Vector3 dir = playerMovement.lookDir.normalized;
        transform.position = pivotPoint.position + dir * radius;

        float xScale = 1;
        if (dir.x < 0)
        {
            xScale = -1;
        }
        transform.localScale = new Vector3(xScale, 1, 1);

        // Decreases cooldown time
        if (wait > 0.0f)
        {
            wait -= Time.deltaTime;
        }
    }

    public override void Attack()
    {
        if (wait > 0.0f)
        {
            Debug.Log(CooldownProgress());
            return;
        }

        Debug.Log("Shield bash!");

        wait = cooldownTime;
        animator.SetTrigger("Attack");
        Collider2D[] hits = Physics2D.OverlapBoxAll(bashPoint.position, bashRange, 0f, bashable);

        // Iterate through `hits` and deal damage
        for (int i = 0; i < hits.Length; i++)
        {
            Debug.Log("Stunned " + hits[i].name + " for " + stunTime + " seconds.");
        }
    }

}
