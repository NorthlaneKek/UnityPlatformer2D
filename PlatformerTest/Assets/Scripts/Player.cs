using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    private Rigidbody2D myRigidBody;




    [SerializeField]
    private float groundRadius;

    private bool isGrounded;

    private bool jump;

    [SerializeField]
    private float jumpForce;

    [SerializeField] 
    private LayerMask whatIsGround;

    [SerializeField]
    private Transform[] groundPoints;

    public override bool IsDead
    {
        get
        {
            return health <= 0;
        }
    }





    // Use this for initialization
    public override void Start()
    {
        base.Start();
        myRigidBody = GetComponent<Rigidbody2D>();


    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!IsDead)
        {
            float horizontal = Input.GetAxis("Horizontal");

            isGrounded = IsGrounded();

            HandleInput();

            HandleMovement(horizontal);

            ResetValues();

            Restart();

            Flip(horizontal);
        }

    }

    private void HandleMovement(float horizontal)
    {
        myRigidBody.velocity = new Vector2(horizontal * movementSpeed, myRigidBody.velocity.y);
        if (isGrounded && jump)
        {
            isGrounded = false;
            myRigidBody.AddForce(new Vector2(0, jumpForce));
        }
        MyAnimator.SetFloat("speed", Mathf.Abs(horizontal));
    }

    void Flip(float horizontal)
    {
        if (horizontal > 0 && !rightDir || horizontal < 0 && rightDir)
        {
            ChangeDirection();
        }

    }

    public override void ChangeDirection()
    {
        rightDir = !rightDir;
        transform.localScale = new Vector3(transform.localScale.x * -1, (float)0.8633305, (float)0.6725666);
    }

    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            jump = true;
        }
    }

    private bool IsGrounded()
    {
        if (myRigidBody.velocity.y <= 0)
        {
            foreach (Transform point in groundPoints)
            {
                Collider2D[] colliders = Physics2D.OverlapCircleAll(point.position, groundRadius, whatIsGround);

                for(int i = 0; i < colliders.Length; i++)
                {
                    if (colliders[i].gameObject != gameObject)
                    {
                        return true;
                    }
                }
            }
        }
        return false;
    }

    private void ResetValues()
    {
        jump = false;
    }

    private void Restart()
    {
        if (transform.position.y <= -5)
        {
            transform.position = new Vector3((float)-13.42, (float)0.27, 0);
        }
    }

    public override IEnumerator TakeDamage()
    {
        health -= 10;
        if (!IsDead)
        {
            MyAnimator.SetTrigger("damage");
        }
        else
        {
            MyAnimator.SetTrigger("die");
            yield return null;
        }

    }
}
