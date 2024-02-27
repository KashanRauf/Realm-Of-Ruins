using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Blast : MonoBehaviour
{
    public bool purify;
    public float damage;
    public float maxRadius = 2f;
    public float defaultRadius = 0.01f;
    private float step;
    public float holdTime = .5f;
    public float hold = 0f;
    public float radius;
    public bool active;
    // Should p use an array
    public LayerMask first_target;
    public LayerMask second_target;
    public Transform origin;
    private CircleCollider2D coll;

    private void Awake()
    {
        damage = 0;
        radius = defaultRadius;
        step = maxRadius * 1.5f;
        transform.localScale = new Vector2(radius, radius);
        origin = transform.parent.parent;
        coll = GetComponent<CircleCollider2D>();
    }

    // Something is wrong here but not wrong enough to fix
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // The relation between a LayerMask and Layer: Layermask = 1 << Layer

        // Play an effect on hit (e.g. explosion)
        // GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
        // Destroy(effect, 2f);

        GameObject hit = collision.gameObject;
        Debug.Log("Detected a collision: " + hit.layer);

        // If the object hit is an enemy, deal damage to it
        if (1 << hit.layer == first_target)
        {
            Debug.Log("Hit an enemy");
            EnemyState enemyState = hit.GetComponent<EnemyState>();
            if (purify)
            {
                // If the projectile is for purifying, lowers corruption instead
                enemyState.corruption -= damage;
            }
            else
            {
                // Otherwise deals damage
                enemyState.currentHealth -= damage;
            }
            // Stuns either way
            enemyState.stunTime += GetComponentInParent<Staff>().stunTime;

        }
        else if (1 << hit.layer == second_target)
        {
            Debug.Log("Hit a bullet");
            // Is a bullet otherwise, may not need to handle this
            Destroy(hit);
        }
    }

    private void Update()
    {
        if (active)
        {
            Expand();
        } 
        else if (hold <= 0f)
        {
            radius = defaultRadius;
            UpdateRadius(radius);
        }
        else
        {
            hold -= Time.deltaTime;
        }
    }

    public void Activate()
    {
        active = true;
    }

    private void Expand()
    {
        float newRadius = radius + (step * Time.deltaTime);
        if (newRadius >= maxRadius)
        {
            newRadius = maxRadius;
            hold = holdTime;
            active = false;
        }
        UpdateRadius(newRadius);
        radius = newRadius;
    }

    private void UpdateRadius(float r)
    {
        transform.localScale = new Vector2(r, r);
        coll.radius = r;
    }
}
