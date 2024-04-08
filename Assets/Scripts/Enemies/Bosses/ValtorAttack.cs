using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ValtorAttack : MonoBehaviour
{
    [SerializeField]
    public float minAttackRange { get; private set; } = 2f;
    [SerializeField]
    public float maxAttackRange { get; private set; } = 7f;
    // Cooldown time in seconds
    private float cooldown = 5.6f;
    private float wait = 0.0f;
    public LayerMask attackable;
    public bool inRange { get; private set; }
    private PlayerAwareness detection;
    private ValtorMovement move;
    private EnemyState state;
    public Transform projectileOrigin;
    public GameObject daylight;
    public GameObject battleCry;
    public GameObject eclipse;
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
    )[] attacks = new (float, Vector2, Vector2, GameObject, int, int, int, float, float, float, float)[4];

    private void Awake()
    {
        inRange = false;
        detection = GetComponent<PlayerAwareness>();
        move = GetComponent<ValtorMovement>();
        state = GetComponent<EnemyState>();
        animator = GetComponent<Animator>();


        daylight.GetComponent<Projectile>().damage = 1.5f;
        daylight.GetComponent<Projectile>().target = attackable;
        battleCry.GetComponent<Projectile>().damage = 2;
        battleCry.GetComponent<Projectile>().target = attackable;
        eclipse.GetComponent<Projectile>().damage = 6;
        eclipse.GetComponent<Projectile>().target = attackable;

        // Looks like sunbeams
        attacks[0] = (7, gameObject.transform.position, move.targetDir.normalized, daylight, 0, 10, 8, 36f, 0.4f, 2f, 4f);
        // Fully encircling AOE
        attacks[1] = (4.5f, gameObject.transform.position, move.targetDir.normalized, battleCry, 0, 24, 15, 10f, 0.7f, 6, 0f);
        // Helix attack
        attacks[2] = (5.0f, gameObject.transform.position, move.targetDir.normalized, eclipse, 3, 1, 5, 0f, .35f, 3f, 0);
        // attacks[3] is the opposite half of attacks[2]
        attacks[3] = (5.0f, gameObject.transform.position, move.targetDir.normalized, eclipse, 4, 1, 5, 0f, .35f, 3f, 0);
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

        int attack = Random.Range(0, 3);

        if (attack == 2)
        {
            GameObject attackInstance1 = Instantiate(bulletManager);
            GameObject attackInstance2 = Instantiate(bulletManager);
            attacks[2].Item2 = gameObject.transform.position;
            attacks[2].Item3 = move.targetDir.normalized;
            attacks[3].Item2 = gameObject.transform.position;
            attacks[3].Item3 = move.targetDir.normalized;
            attackInstance1.GetComponent<BulletManager>().Initialize(attacks[2]);
            attackInstance2.GetComponent<BulletManager>().Initialize(attacks[3]);
        } else
        {
            GameObject attackInstance = Instantiate(bulletManager);
            attacks[attack].Item2 = gameObject.transform.position;
            attacks[attack].Item3 = move.targetDir.normalized;

            attackInstance.GetComponent<BulletManager>().Initialize(attacks[attack]);
        }

        wait = cooldown;
    }

    public float CooldownProgress()
    {
        // Returns as a percentage
        return ((cooldown - wait) / cooldown) * 100;
    }
}
