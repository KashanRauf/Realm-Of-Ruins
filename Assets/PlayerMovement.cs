using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float moveSpeed = 7f;
    public Rigidbody2D rb;
    public Animator animator;
    public new Camera camera;
    Vector2 movement;
    Vector2 mousePos;


    // Update is called once per frame
    void Update()
    {
        // Handle input
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        
        mousePos = camera.ScreenToWorldPoint(Input.mousePosition);
        
        Vector2 lookDir = mousePos - rb.position;
        // float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        
        animator.SetFloat("Horizontal", lookDir.x);
        animator.SetFloat("Vertical", lookDir.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);

    }

    void FixedUpdate()
    {
        // Actual movement/physics
        rb.MovePosition(rb.position + (movement * moveSpeed * Time.fixedDeltaTime));

        
    }
}
