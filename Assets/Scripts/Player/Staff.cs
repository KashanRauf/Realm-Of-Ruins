using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Staff : Weapon
{

    public Transform projectileOrigin;
    public GameObject projectilePrefab;
    public GameObject altProjectilePrefab;
    public float force = 15.0f;
    protected Transform pivotPoint;
    protected float radius = 0.5f;
    protected PlayerMovement playerMovement;
    protected PlayerState player;
    public LayerMask enemies;
    public LayerMask defendAgainst;
    public float stunTime = 0.5f;
    //public Blast blast;
    public BulletManager bulletManager;


    private void Awake()
    {
        weaponType = 2;
        baseDamage = 5.0f;
        cooldownTime = 0.3f;
        altCooldownTime = 1.8f;
        playerMovement = GetComponentInParent<PlayerMovement>();
        player = GetComponentInParent<PlayerState>();
        pivotPoint = transform.parent;
        

        transform.position += Vector3.up * radius;
        projectilePrefab.GetComponent<Projectile>().target = enemies;
    }

    public override void Attack()
    {
        if (wait > 0.0f)
        {
            Debug.Log(CooldownProgress());
            return;
        }

        // Debug.Log("Attack performed!");

        projectilePrefab.GetComponent<Projectile>().damage = baseDamage * player.damageMultiplier;
        GameObject projectile = Instantiate(projectilePrefab, projectileOrigin.position, projectileOrigin.rotation);
        Rigidbody2D projBody = projectile.GetComponent<Rigidbody2D>();
        projectile.transform.up = playerMovement.lookDir;
        projBody.AddForce(playerMovement.lookDir.normalized * force, ForceMode2D.Impulse);

        wait = cooldownTime;
    }

    public override void AltAttack()
    {
        if (wait > 0.0f)
        {
            Debug.Log(CooldownProgress());
            return;
        }

        // Radial blast, destroy bullets and slight stun: Gives staves a more balanced play style
        //blast.Activate();

        (float, Vector2, Vector2, GameObject, int, int, int, float, float, float, float) attackData
            = (0.25f, playerMovement.transform.position, playerMovement.lookDir.normalized, altProjectilePrefab, 0, 12, 1, 30f, 0, 3, 18);
        var attackInstance = Instantiate(bulletManager);
        attackInstance.GetComponent<BulletManager>().Initialize(attackData);
        attackInstance.transform.position = playerMovement.transform.position;

        wait = altCooldownTime;
    }

    private void Update()
    {
        // Follows the mouse in a circle around the player
        Vector3 dir = playerMovement.lookDir.normalized;
        transform.position = pivotPoint.position + dir * radius;

        float xScale = 1;
        if (dir.x < 0)
        {
            xScale = -1;
        }
        transform.localScale = new Vector3(xScale, 1, 1);

        // Decreases cooldown time
        if (wait > 0.0f)
        {
            wait -= Time.deltaTime;
        }
    }
}
