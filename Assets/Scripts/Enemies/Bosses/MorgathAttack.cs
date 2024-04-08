using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MorgathAttack : MonoBehaviour
{
    [SerializeField]
    public float minAttackRange { get; private set; } = 3f;
    [SerializeField]
    public float maxAttackRange { get; private set; } = 8f;
    // Cooldown time in seconds
    private float cooldown = 6.5f;
    private float wait = 0.0f;
    public LayerMask attackable;
    public bool inRange { get; private set; }
    private PlayerAwareness detection;
    private MorgathMovement move;
    private EnemyState state;
    public Transform projectileOrigin;
    public GameObject snakeBite;
    public GameObject slither;
    public GameObject whip;
    public GameObject bulletManager;
    public Animator animator;

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
    )[] attacks = new (float, Vector2, Vector2, GameObject, int, int, int, float, float, float, float)[3];

    private void Awake()
    {
        inRange = false;
        detection = GetComponent<PlayerAwareness>();
        move = GetComponent<MorgathMovement>();
        state = GetComponent<EnemyState>();
        animator = GetComponent<Animator>();


        snakeBite.GetComponent<Projectile>().damage = 2.5f;
        snakeBite.GetComponent<Projectile>().target = attackable;
        slither.GetComponent<Projectile>().damage = 4;
        slither.GetComponent<Projectile>().target = attackable;
        whip.GetComponent<Projectile>().damage = 8;
        whip.GetComponent<Projectile>().target = attackable;

        // Wide range, moving
        attacks[0] = (5, gameObject.transform.position, move.targetDir.normalized, slither, 0, 5, 8, 72f, 0.4f, 2f, 6f);
        // Long, fast waves
        attacks[1] = (2.5f, gameObject.transform.position, move.targetDir.normalized, snakeBite, 1, 4, 10, 25f, 0.05f, 6, 0f);
        // This one scares ts out of me
        attacks[2] = (8.0f, gameObject.transform.position, move.targetDir.normalized, whip, 2, 4, 4, 15f, .35f, 4f, 2);
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


        // Animator
        animator.SetTrigger("Attacking");

        GameObject attackInstance = Instantiate(bulletManager);
        int attack = Random.Range(0, 3);
        attacks[attack].Item2 = gameObject.transform.position;
        attacks[attack].Item3 = move.targetDir.normalized;

        attackInstance.GetComponent<BulletManager>().Initialize(attacks[attack]);


        wait = cooldown;
    }

    public float CooldownProgress()
    {
        // Returns as a percentage
        return ((cooldown - wait) / cooldown) * 100;
    }
}
