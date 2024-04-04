using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZogAttack : MonoBehaviour
{
    [SerializeField]
    public float minAttackRange { get; private set; } = 4f;
    [SerializeField]
    public float maxAttackRange { get; private set; } = 7;
    // Cooldown time in seconds
    private float cooldown = 5.0f;
    private float wait = 0.0f;
    public LayerMask attackable;
    public bool inRange { get; private set; }
    private PlayerAwareness detection;
    private ZogMovement move;
    private EnemyState state;
    public Transform projectileOrigin;
    public GameObject infernoBall;
    public GameObject crescentBeam;
    public GameObject mangle;
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
    )[] attacks = new (float, Vector2, Vector2, GameObject, int, int, int, float, float, float, float)[3];

    private void Awake()
    {
        inRange = false;
        detection = GetComponent<PlayerAwareness>();
        move = GetComponent<ZogMovement>();
        state = GetComponent<EnemyState>();


        crescentBeam.GetComponent<Projectile>().damage = 12;
        crescentBeam.GetComponent<Projectile>().target = attackable;
        mangle.GetComponent<Projectile>().damage = 12;
        mangle.GetComponent<Projectile>().target = attackable;
        infernoBall.GetComponent<Projectile>().damage = 12;
        infernoBall.GetComponent<Projectile>().target = attackable;

        // Forward blast * 3
        attacks[0] = (1.5f, gameObject.transform.position, move.targetDir.normalized, crescentBeam, 0, 3, 4, 20f, 0.33f, 4f, 0f);
        // Wave skulls * 2
        attacks[1] = (4.0f, gameObject.transform.position, move.targetDir.normalized, mangle, 1, 2, 1, 40f, 0.25f, 1.5f, 0f);
        // AOE
        attacks[2] = (8.0f, gameObject.transform.position, move.targetDir.normalized, infernoBall, 0, 12, 4, 30, 0.5f, 3f, 18f);
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
        // Debug.Log("Player direction: " + move.targetDir);
        //attackData = (4.0f, gameObject.transform.position, move.targetDir.normalized, projectilePrefab, 0, 3, 2, 20f, 0.25f, 2f, 0f);
        //Debug.Log("Check");
        //Debug.Log(gameObject.transform.position);
        //Debug.Log(move.targetDir.normalized);
        //Debug.Log(crescentBeam);
        //Debug.Log(mangle);
        //Debug.Log(infernoBall);
        Debug.Log(attacks[0]);

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
