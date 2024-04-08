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
    public AudioClip hitEffect;

    private void OnCollisionEnter2D(Collision2D collision)
    {

        // The relation between a LayerMask and Layer: Layermask = 1 << Layer

        // Play an effect on hit (e.g. explosion)
        // GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
        // Destroy(effect, 2f);

        GameObject hit = collision.collider.gameObject;

        // If the object hit is a target, deal damage to it
        Debug.Log(this.name + " hit a " + hit.name);
        if (1 << hit.layer == target)
        {
            AudioSource sound = GetComponent<AudioSource>();
            sound.clip = hitEffect;
            sound.Play();

            if (hit.name.Equals("zog") || hit.name.Equals("morgath") || hit.name.Equals("valtor"))
            {
                Debug.Log("Hit a boss");
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
                    sound.Play();
                }
                else
                {
                    State targetState = hit.GetComponent<State>();
                    // Otherwise deals damage
                    targetState.currentHealth -= damage;
                    sound.Play();
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
