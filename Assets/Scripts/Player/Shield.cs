using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



// GIVE THE SHIELD A BOX COLLIDER ???
public class Shield : Weapon
{

    protected float radius = 0.8f;
    protected Transform pivotPoint;
    protected PlayerMovement playerMovement;
    protected PlayerState player;
    public float stunTime;
    public Transform bashPoint;
    public Vector2 bashRange = new Vector2(1f, 1f);
    public LayerMask blockable;
    public LayerMask bashable;
    private bool bashing = false;
    private bool altMove = false;
    private Vector2 currentPosition;
    private Vector2 destination;
    public float bashSpeed = 0.3f;
    private float angle = 0f;

    private void Awake()
    {
        weaponType = 1;
        stunTime = 2.0f;
        cooldownTime = 1.2f;
        altCooldownTime = 4.5f;
        playerMovement = GetComponentInParent<PlayerMovement>();
        player = GetComponentInParent<PlayerState>();
        pivotPoint = transform.parent;
        currentPosition = transform.position;

        transform.position += Vector3.up * radius;
    }

    void Update()
    {
        if (altMove)
        {
            transform.position = pivotPoint.position + new Vector3(Mathf.Cos(angle)*(radius*1.1f), Mathf.Sin(angle)*radius*1.1f);
            angle += Time.deltaTime * 18;

            if (angle >= Mathf.Deg2Rad * 360)
            {
                altMove = false;
            }
            else
            {
                wait -= Time.deltaTime;
                return;
            }
        }


        // Follows the mouse in a circle around the player
        Vector3 dir = playerMovement.lookDir.normalized;
        transform.position = pivotPoint.position + dir * radius;

        // Adjusts the bashPoint similarly (essentially just a larger circle
        bashPoint.position = pivotPoint.position + dir * 2 * radius;

        // Does the bashing "animation"
        if (bashing)
        {
            // Debug.Log(Time.deltaTime);
            currentPosition = Vector2.MoveTowards(currentPosition, destination, bashSpeed/10);
            transform.position = currentPosition;

            // Collider2D[] hits = Physics2D.OverlapBoxAll(bashPoint.position, bashRange, 0f, bashable);
            // Range between shield at original position and at bashpoint
            // Collider2D[] bashed = Physics2D.OverlapBoxAll(transform.position, )

            Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, 1f/3f, bashable);

            // Iterate through `hits` and stun them
            for (int i = 0; i < hits.Length; i++)
            {
                Debug.Log("Stunned " + hits[i].name + " for " + stunTime + " seconds.");
                hits[i].gameObject.GetComponent<EnemyState>().stunTime = stunTime;
            }

            if (currentPosition.Equals(destination))
            {
                Debug.Log("Bashed");
                bashing = false;
                currentPosition = transform.position;
                pivotPoint = transform.parent;
            }
        }


        // Decreases cooldown time
        if (wait > 0.0f)
        {
            wait -= Time.deltaTime;
        }
    }

    public override void Attack()
    {
        if (wait > 0.0f)
        {
            Debug.Log(CooldownProgress());
            return;
        }

        Debug.Log("Shield bash!");

        wait = cooldownTime;
        bashing = true;
        destination = bashPoint.position;

        currentPosition = transform.position;
    }

    // Messes up the cooldown for an unknown reason
    public override void AltAttack()
    {
        if (wait > 0.0f)
        {
            Debug.Log(CooldownProgress());
            return;
        }

        // AOE boosted stun with purification
        // Spins around the player rapidly and uses an overlapcircleall with a massive radius
        altMove = true;
        wait += altCooldownTime;
    }
}
