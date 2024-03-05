using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField]
    public float minAttackRange { get; private set; } = 2.5f;
    [SerializeField]
    public float maxAttackRange { get; private set; } = 5.0f;
    // Cooldown time in seconds
    private float cooldown = 2.0f;
    private float wait = 0.0f;
    private float force = 10f;
    public LayerMask attackable;
    public bool inRange { get; private set; }
    private PlayerAwareness detection;
    private EnemyMovement move;
    private EnemyState state;
    public Transform projectileOrigin;
    public GameObject projectilePrefab;



    private void Awake()
    {
        inRange = false;
        detection = GetComponent<PlayerAwareness>();
        move = GetComponent<EnemyMovement>();
        state = GetComponent<EnemyState>();

        projectilePrefab.GetComponent<Projectile>().damage = state.damageOnAttack;
        projectilePrefab.GetComponent<Projectile>().target = attackable;
    }

    void Update()
    {
        // Checks if player is in attack range
        inRange = detection.distanceFromPlayer.magnitude >= minAttackRange && detection.distanceFromPlayer.magnitude <= maxAttackRange;

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
