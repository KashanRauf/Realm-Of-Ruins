using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MorgathMovement : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed;
    private Rigidbody2D rb;
    private PlayerAwareness detection;
    private MorgathAttack combat;
    private ZogState state;
    public Vector2 targetDir { get; private set; }
    public Animator animator;

    // Start is called before the first frame update
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        detection = GetComponent<PlayerAwareness>();
        combat = GetComponent<MorgathAttack>();
        state = GetComponent<ZogState>();
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        if (state.stunTime > 0)
        {
            state.stunTime -= Time.deltaTime;
            return;
        }

        updateDirection();

        // Look at player when doing the actual visuals

        // Velocity
        if (targetDir.magnitude == 0 || combat.inRange)
        {
            rb.velocity = Vector2.zero;
        }
        else if (detection.distanceFromPlayer.magnitude < combat.minAttackRange)
        {
            rb.MovePosition(rb.position + (targetDir * moveSpeed * Time.deltaTime * -0.3f));
        }
        else
        {
            rb.MovePosition(rb.position + (targetDir * moveSpeed * Time.deltaTime));
        }

        if (combat.inRange)
        {
            combat.Attack();
        }

        animator.SetFloat("Horizontal", targetDir.x);
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
