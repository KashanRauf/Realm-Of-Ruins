using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    // public GameObject hitEffect;

    public bool purify;
    public float damage;
    public LayerMask target;
    // Default -1
    public int maxHits = 1;
    public int hits = 0;
    public int spoke = -1;
    public bool used = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {

        // The relation between a LayerMask and Layer: Layermask = 1 << Layer

        // Play an effect on hit (e.g. explosion)
        // GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
        // Destroy(effect, 2f);

        GameObject hit = collision.collider.gameObject;
        // If it hits it's own type then ignore the collision
        // if (this.gameObject.layer == hit.layer) return;
        // If 

        // If the object hit is a target, deal damage to it
        Debug.Log(hit.name);
        if (1 << hit.layer == target)
        {
            if (hit.name.Equals("zog-left"))
            {
                Debug.Log("Hit zog");
                if (purify)
                {
                    ZogState state = hit.GetComponent<ZogState>();
                    state.corruption -= damage;
                } else
                {
                    ZogState state = hit.GetComponent<ZogState>();
                    state.currentHealth -= damage;
                }

            } else
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
            
        }

        hits++;
        if (hits >= maxHits) used = true;

        if (spoke == -1)
        {
            Destroy(gameObject);
        }
    }

}
