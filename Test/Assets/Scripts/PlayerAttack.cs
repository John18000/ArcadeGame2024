using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    //Input Variables
    private float movementInput;

    //Ground Check
    private bool isGrounded;
    public LayerMask groundLayer;
    public float circleRadius;
    public GameObject groundCheck;

    //Attack Variables
    public float attkTimer;
    public bool isAttacking;
    public bool comboHit;
    public bool comboDone;
    public float attkCooldown;
    public bool canRotate;
    public int attkType; //int which uses numbers to describe type of attack (0 is normal, 1 is side aerial, 2 is down aerial, 3 is up aerial)
    private Rigidbody2D rb;
    [SerializeField] private float attkLunge;
    public int attkNum;
    public bool attkStart;
    [SerializeField] GameObject attkPoint1;
    [SerializeField] GameObject attkPoint2;
    [SerializeField] GameObject attkPointUp;
    [SerializeField] GameObject attkPointDown;
    [SerializeField] float attkRadius;
    [SerializeField] LayerMask enemyLayer;
    [SerializeField] int playerDamage;
    [SerializeField] int playerKnockback;
    [SerializeField] float attkPushback;
    Vector2 attkPos;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        isAttacking = false;
        comboDone = false;
        comboHit = false;
        attkTimer = 0f;
        attkNum = 0;
        attkStart = false;
    }

    // Update is called once per frame
    void Update()
    {
        GroundCheck();

        //attack code (animator)
        // if(Input.GetKeyDown("x") && !isAttacking && !comboDone && ground && Input.GetKey("up")) {
        //     isAttacking = true;
        //     attkType = 2;
        // } else if(Input.GetKeyDown("x") && !isAttacking && !comboDone && ground) {
        //     isAttacking = true;
        //     attkType = 0;
        // } else if(Input.GetKeyDown("x") && !isAttacking && !comboDone && !ground && Input.GetKey("up")) {
        //     isAttacking = true;
        //     attkType = 2;
        // } else if(Input.GetKeyDown("x") && !isAttacking && !comboDone && !ground && Input.GetKey("down")) {
        //     isAttacking = true;
        //     attkType = 3;
        // } else if(Input.GetKeyDown("x") && !isAttacking && !comboDone && !ground) {
        //     isAttacking = true;
        //     attkType = 1;
        // }

        if(comboDone == true) {
            attkTimer += Time.deltaTime;

            if(attkTimer >= attkCooldown) {
                if(attkNum > 1) {
                    attkRadius /= 1.2f;
                }

                comboDone = false;
                attkTimer = 0f;
                attkNum = 0;
                if(isGrounded) {
                    rb.velocity = Vector3.zero;
                }
            }
        }

        if(attkStart) {
            Attack();
        }
    }
    
    void Attack()
    {

    }

    void GroundCheck()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.transform.position, circleRadius, groundLayer);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>().x;
    }

    public void OnAttack(InputAction.CallbackContext context)
    {

    }
}
