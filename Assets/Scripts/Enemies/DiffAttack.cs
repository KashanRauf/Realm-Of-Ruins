using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiffAttack : MonoBehaviour
{
    [SerializeField]
    public float minAttackRange { get; private set; } = 2.5f;
    [SerializeField]
    public float maxAttackRange { get; private set; } = 5.0f;
    // Cooldown time in seconds
    private float cooldown = 1.0f;
    private float wait = 0.0f;
    public LayerMask attackable;
    public bool inRange { get; private set; }
    private PlayerAwareness detection;
    private DiffMovement move;
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
        move = GetComponent<DiffMovement>();
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

        StartCoroutine(ShootBulletsSequence());

        // Sets cooldown
        wait = cooldown * 1.5f;
    }

    private IEnumerator ShootBulletsSequence()
    {
        for (int i = 0; i < 3; i++)
        {
            // Shoot one shot towards the player
            Vector2 shotDirection = move.targetDir.normalized;
            attackData = (4.0f, gameObject.transform.position, shotDirection, projectilePrefab, 0, 1, 1, 20f, 2.5f, 5f, 0f);
            GameObject attackInstance = Instantiate(bulletManager);
            attackInstance.GetComponent<BulletManager>().Initialize(attackData);

            // Add a slight delay between each shot
            yield return new WaitForSeconds(0.25f);
        }
    }



    public float CooldownProgress()
    {
        // Returns as a percentage
        return ((cooldown - wait) / cooldown) * 100;
    }
}
