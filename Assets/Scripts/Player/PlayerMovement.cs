using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    
    public float moveSpeed = 7f;
    public float dashSpeed = 56f;
    public float dashDuration = 0.0625f;
    public Rigidbody2D rb;
    public Animator animator;
    public new Camera camera;
    public PlayerState state;
    Vector2 movement;
    Vector2 mousePos;
    public Vector2 lookDir { get; private set; }
    public GameObject dashEffect;
    public Transform footPosition;

    private void Awake()
    {
        state = GetComponent<PlayerState>();
    }

    // Update is called once per frame
    void Update()
    {
        // Input controls
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        
        mousePos = camera.ScreenToWorldPoint(Input.mousePosition);
        
        lookDir = mousePos - rb.position;
        // float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;

        // Animation controls
        animator.SetFloat("Horizontal", lookDir.x);
        animator.SetFloat("Vertical", lookDir.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);

        // Lower dash cooldown and add stacks
        if (state.dashesUsed > 0)
        {
            state.dashWait -= Time.deltaTime;
            if (state.dashWait <= state.dashCooldownTime*(state.dashesUsed))
            {
                state.dashesUsed--;
            }
        }

        if (Input.GetKeyDown(KeyCode.Space) && state.dashesUsed < state.maxDashes)
        {
            StartCoroutine(Dash());
        }
    }

    void FixedUpdate()
    {
        if (state.isDashing)
        {
            return;
        }

        // Actual movement/physics
        rb.MovePosition(rb.position + (movement * moveSpeed * Time.fixedDeltaTime));

    }

    private IEnumerator Dash()
    {
        if (state.dashesUsed >= state.maxDashes || state.isDashing || state.skillOccupied)
        {
            yield return new WaitForSeconds(0);
        }


        Debug.Log("Dashing");

        state.dashesUsed++;
        state.dashWait += state.dashCooldownTime;

        Instantiate(dashEffect, transform);

        state.isDashing = true;
        rb.velocity = movement * dashSpeed/2;
        yield return new WaitForSeconds(dashDuration);
        rb.velocity = Vector2.zero;
        state.isDashing = false;
    }
}
