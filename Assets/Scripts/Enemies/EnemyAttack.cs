using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField]
    private float attackRange = 2.5f;
    // Cooldown time in seconds
    private float cooldown = 2.0f;
    private float wait = 0.0f;
    public bool inRange { get; private set; }
    private PlayerAwareness detection;
    private EnemyMovement move;

    public Transform projectileOrigin;
    public GameObject projectilePrefab;
    private float force = 10f;

    public LayerMask attackable;

    private void Awake()
    {
        inRange = false;
        detection = GetComponent<PlayerAwareness>();
        move = GetComponent<EnemyMovement>();

        projectilePrefab.GetComponent<Projectile>().target = attackable;
    }

    void Update()
    {
        // Checks if player is in attack range
        inRange = detection.distanceFromPlayer.magnitude <= attackRange;

        if (wait > 0.0f)
        {
            wait -= Time.deltaTime;
        }
    }

    public void Attack()
    {
        if (inRange && wait > 0.0f) {
            // Debug.Log("Cooldown: " + (int) CooldownProgress() + "%");
            return;
        }

        // Does the actual attack
        GameObject projectile = Instantiate(projectilePrefab, projectileOrigin.position, projectileOrigin.rotation);
        Rigidbody2D projBody = projectile.GetComponent<Rigidbody2D>();
        projectile.transform.up = move.targetDir;
        projBody.AddForce(move.targetDir.normalized * force, ForceMode2D.Impulse);

        // Sets cooldown
        wait = cooldown;
    }

    public float CooldownProgress()
    {
        // Returns as a percentage
        return ((cooldown - wait) / cooldown) * 100;
    }
}