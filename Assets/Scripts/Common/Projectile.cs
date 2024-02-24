using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    // public GameObject hitEffect;

    public float damage;
    public LayerMask target;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // The thing it hit, will be useful for implementing damage
        // collision.collider

        // Play an effect on hit (e.g. explosion)
        // GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
        // Destroy(effect, 2f);

        Destroy(gameObject);
        // Debug.Log(collision.collider.name);
    }
}
