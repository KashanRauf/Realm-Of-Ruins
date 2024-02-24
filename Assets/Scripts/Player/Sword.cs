using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : Weapon
{
    protected float radius = 0.6f;
    protected Transform pivotPoint;
    protected PlayerMovement playerMovement;
    protected PlayerState player;
    public Animator animator;
    public float damage;
    public Transform attackPoint;
    public Vector2 attackRange = new Vector2(1f, 1f);
    public LayerMask enemies;

    public void Awake()
    {
        weaponType = 0;
        baseDamage = 8.0f;
        cooldownTime = .8f;
        playerMovement = GetComponentInParent<PlayerMovement>();
        player = GetComponentInParent<PlayerState>();
        pivotPoint = transform.parent;

        transform.position += Vector3.up * radius;
    }

    private void Update()
    {   

        animator.SetFloat("Horizontal", playerMovement.lookDir.x);
        
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

        Debug.Log("Sword swung!");

        damage = baseDamage * player.damageMultiplier;
        wait = cooldownTime;
        animator.SetTrigger("Attack");
        Collider2D[] hits = Physics2D.OverlapBoxAll(attackPoint.position, attackRange, 0f, enemies);

        // Iterate through `hits` and deal damage
        for (int i = 0; i < hits.Length; i++)
        {
            Debug.Log("Hit " + hits[i].name + " for " + damage + " damage.");
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireCube(attackPoint.position, attackRange);
    }
}
