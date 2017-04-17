using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour {



    // Use this for initialization

    public Animator MyAnimator { get; private set; }

    [SerializeField]
    protected float movementSpeed;
    protected bool rightDir;

    public abstract bool IsDead { get; }

    [SerializeField]
    protected int health;

    [SerializeField]
    private EdgeCollider2D feetCollider;

    [SerializeField]
    private EdgeCollider2D enedge1;
    [SerializeField]
    private EdgeCollider2D enedge2;

    public virtual void Start() {
        rightDir = true;
        MyAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update() {

    }

    public abstract void ChangeDirection();

    public void JumpAttack()
    {
        feetCollider.enabled = !feetCollider.enabled;
    }

    public void EnemyAttack()
    {
        enedge1.enabled = !enedge1.enabled;
        enedge2.enabled = !enedge2.enabled;
    }

    public abstract IEnumerator TakeDamage();

    public virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Feet" || other.tag == "SideEdge")
        {
            StartCoroutine(TakeDamage());
        }
    }
}
