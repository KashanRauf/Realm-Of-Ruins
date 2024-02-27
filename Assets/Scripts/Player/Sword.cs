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
    private float damage;
    public Transform attackPoint;
    public Vector2 attackRange;
    public LayerMask enemies;

    public void Awake()
    {
        weaponType = 0;
        baseDamage = 8.0f;
        cooldownTime = .8f;
        altCooldownTime = 4f;
        playerMovement = GetComponentInParent<PlayerMovement>();
        player = GetComponentInParent<PlayerState>();
        pivotPoint = transform.parent.parent;

        transform.parent.position += Vector3.up * radius;
    }

    private void Update()
    {   
        animator.SetFloat("Horizontal", playerMovement.lookDir.x);
        
        // Follows the mouse in a circle around the player
        Vector3 dir = playerMovement.lookDir.normalized;
        transform.parent.position = pivotPoint.position + dir * radius;

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
        Collider2D[] hits = Physics2D.OverlapBoxAll(attackPoint.position - new Vector3(0, attackPoint.localPosition.y/2, 0), attackRange, Mathf.Atan2(playerMovement.lookDir.x, playerMovement.lookDir.y) * Mathf.Rad2Deg, enemies);

        // Iterate through `hits` and deal damage
        for (int i = 0; i < hits.Length; i++)
        {
            Debug.Log("Hit " + hits[i].name + " for " + damage + " damage.");
            hits[i].gameObject.GetComponent<EnemyState>().currentHealth -= damage;
        }
    }

    // Has none for now
    public override void AltAttack()
    {
        if (wait > 0.0f)
        {
            Debug.Log(CooldownProgress());
            return;
        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.parent.position, attackRange);
    }
}
