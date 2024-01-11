using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
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
        GroundCheck();

        moveHorizontal = Input.GetAxis("Horizontal");

        //jump input
        if(Input.GetKeyDown(KeyCode.W))
        {
            if (isGrounded || jumpCount <= maxJumps)
            {
                jump = true;
                isGrounded = false;
                jumpCount++;
            }
            
        }

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

        if (jump)
        {
            Jump();
        }

        if (dash)
        {
            StartCoroutine(Dash()); 
        }
    }

    void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, 0);
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        jump = false;
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
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(groundCheck.transform.position, circleRadius);
    }
}
