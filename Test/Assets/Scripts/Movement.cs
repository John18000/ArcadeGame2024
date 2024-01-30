using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{

    private Vector2 movementInput = Vector2.zero;
    private bool isFacingRight = true;
    
    //base movement
    public float speed;
    private float moveHorizontal;
    private Rigidbody2D rb;

    //jump
    public float jumpForce;
    private bool jump;
    private int jumpCount;
    public int maxJumps;
    private bool isGrounded;
    public LayerMask groundLayer;
    public float circleRadius;
    public GameObject groundCheck;

    //dash
    public float dashForce;
    private bool dash;
    public float dashTime;
    private bool canDash = true;
    public float dashCooldown;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        //Modifies isGrounded variable
        GroundCheck();

        moveHorizontal = Input.GetAxis("Horizontal");

        //jump input
       

        //dash input
        if(Input.GetKeyDown(KeyCode.Space) && canDash)
        {
            dash = true;
            canDash = false;
        }

    }

    //FixedUpdate is called at a fixed interval and is independent of frame rate.
    void FixedUpdate()
    {
        if(!dash)
        {
            rb.velocity = new Vector2(moveHorizontal * speed, rb.velocity.y);
        }

        if (dash)
        {
            StartCoroutine(Dash()); 
        }

        if (!isFacingRight && moveHorizontal > 0f)
        {
            Flip();
        }
        else if (isFacingRight && moveHorizontal < 0f)
        {
            Flip();
        }
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (context.performed && (isGrounded || jumpCount > 0))
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            jumpCount--;
        }

        if (context.canceled && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }

    }

    IEnumerator Dash()
    {

        if(moveHorizontal < 0)
        {
            rb.AddForce(Vector2.left * dashForce, ForceMode2D.Impulse);
        }
        
        if(moveHorizontal > 0)
        {
            rb.AddForce(Vector2.right * dashForce, ForceMode2D.Impulse);

        }
        
        //bool to stop base movement while dashing
        yield return new WaitForSeconds(dashTime);
        dash = false;

        //bool for dash cooldown
        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }

    void GroundCheck()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.transform.position, circleRadius, groundLayer);

        if(isGrounded) {
            jumpCount = maxJumps;
        }

        if(!isGrounded)
        {
            //take away a jump if player falls off a platform
            if(jumpCount == maxJumps) {
                jumpCount = maxJumps - 1;
            }
        }
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1f;
        transform.localScale = localScale;

    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveHorizontal = context.ReadValue<Vector2>().x;
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(groundCheck.transform.position, circleRadius);
    }
}
