using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character {

    private IEnemyState currentState;

    public override bool IsDead
    {
        get
        {
            return health <= 0;
        }
    }

    // Use this for initialization
    public override void Start () {
        base.Start();
        ChangeState(new IdleState());
	}
	
	// Update is called once per frame
	void Update () {
        currentState.Execute();
        //Edge();
	}

    public void ChangeState(IEnemyState newState)
    { 
        if (currentState != null)
        {
            currentState.Exit();
        }
        currentState = newState;
        currentState.Enter(this);
    }

    public void Move()
    {
        if (!IsDead)
        {
            MyAnimator.SetFloat("speed", 1);

            transform.Translate(GetDirection() * (movementSpeed * Time.deltaTime));
        }
    }

    public Vector2 GetDirection()
    {
        return rightDir ? Vector2.right : Vector2.left;
    }

    public override void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);
        currentState.OnTriggerEnter(other); 
    }

    public override void ChangeDirection()
    {
        rightDir = !rightDir;
        transform.localScale = new Vector3(transform.localScale.x * -1, (float)0.4327621, (float)0.6725666);
    }

    void Edge()
    {
        if (transform.position.x == (float)18.58 || transform.position.x == (float)7.68)
            ChangeDirection();
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
