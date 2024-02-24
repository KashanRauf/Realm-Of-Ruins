using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 2f;
    private Rigidbody2D rb;
    private PlayerAwareness detection;
    private EnemyAttack combat;
    public Vector2 targetDir { get; private set; }

    // Start is called before the first frame update
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        detection = GetComponent<PlayerAwareness>();
        combat = GetComponent<EnemyAttack>();
    }
    
    private void FixedUpdate()
    {
        updateDirection();

        // Look at player when doing the actual visuals

        // Velocity
        if (targetDir.magnitude == 0 || combat.inRange)
        {
            rb.velocity = Vector2.zero;
        }
        else
        {
            rb.velocity = targetDir * moveSpeed;
        }
        
        if (combat.inRange)
        {
            combat.Attack();
        }
    }

    private void updateDirection()
    {
        if (detection.isAware)
        {
            targetDir = detection.playerDir;
        }
        else
        {
            // Placeholder
            targetDir = Vector2.zero;
        }
    }
}
