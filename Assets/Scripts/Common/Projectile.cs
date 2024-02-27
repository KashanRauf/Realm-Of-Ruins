using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    // public GameObject hitEffect;

    public bool purify;
    public float damage;
    public LayerMask target;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // The relation between a LayerMask and Layer: Layermask = 1 << Layer

        // Play an effect on hit (e.g. explosion)
        // GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
        // Destroy(effect, 2f);

        GameObject hit = collision.collider.gameObject;

        // If the object hit is a target, deal damage to it
        if (1 << hit.layer == target)
        {
            if (purify)
            {
                // If the projectile is for purifying, lowers corruption instead
                EnemyState enemyState = hit.GetComponent<EnemyState>();
                enemyState.corruption -= damage;
            }
            else
            {
                State targetState = hit.GetComponent<State>();
                // Otherwise deals damage
                targetState.currentHealth -= damage;
            }
        }

        Destroy(gameObject);
    }
}
