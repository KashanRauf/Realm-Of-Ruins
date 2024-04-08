using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField]
    public float minAttackRange { get; private set; } = 2.0f;
    [SerializeField]
    public float maxAttackRange { get; private set; } = 5.0f;
    // Cooldown time in seconds
    private float cooldown = 2.0f;
    private float wait = 0.0f;
    public LayerMask attackable;
    public bool inRange { get; private set; }
    private PlayerAwareness detection;
    private EnemyMovement move;
    private EnemyState state;
    public Transform projectileOrigin;
    public GameObject projectilePrefab;
    public GameObject bulletManager;

    // Data published to bullet manager on attack
    public (
        float,
        Vector2,
        Vector2,
        GameObject,
        int,
        int,
        int,
        float,
        float,
        float,
        float
    ) attackData;

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

        if (wait > 0.0f) wait -= Time.deltaTime;

    }

    public void Attack()
    {
        if (inRange && wait > 0.0f) return;

        // Does the actual attack
        Debug.Log("Player direction: " + move.targetDir);
        attackData = (4.0f, gameObject.transform.position, move.targetDir.normalized, projectilePrefab, 0, 3, 2, 20f, 0.25f, 2f, 0f);
        GameObject attackInstance = Instantiate(bulletManager);
        attackInstance.GetComponent<BulletManager>().Initialize(attackData);

        wait = cooldown;
    }

    public float CooldownProgress()
    {
        // Returns as a percentage
        return ((cooldown - wait) / cooldown) * 100;
    }
}
