using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    //base movement
    public float speed;
    public float moveHorizontal;
    private Rigidbody2D rb;

    //jump
    public float jumpForce;
    private bool jump;

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
        moveHorizontal = Input.GetAxis("Horizontal");

        if(Input.GetKeyDown(KeyCode.W))
        {
            jump = true;
        }

        if(Input.GetKeyDown(KeyCode.Space) && canDash)
        {
            dash = true;
            canDash = false;
        }

        Debug.Log(rb.velocity);

        // Try out this delta time method??
        //rb.transform.position += new Vector3(speed * Time.deltaTime, 0.0f, 0.0f);
    }

    //FixedUpdate is called at a fixed interval and is independent of frame rate. Put physics code here.
    void FixedUpdate()
    {
        if(!dash)
        {
            rb.velocity = new Vector2(moveHorizontal * speed, rb.velocity.y);
        }

        if (jump)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            jump = false;
        }

        if (dash)
        {
            StartCoroutine(Dash()); 
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

}
