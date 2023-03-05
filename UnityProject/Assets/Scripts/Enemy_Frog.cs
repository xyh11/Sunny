using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Frog : Enemy
{
    public Transform leftPoint,rightPoint;
    public float speed = 10f, jump = 10f;
    //private Rigidbody2D rb;
    public LayerMask ground;
    private bool isleft = true;
    private float left, right;
    //private Animator anim;
    private Collider2D coll;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        //rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
        //anim = GetComponent<Animator>();

        left = leftPoint.position.x;
        right = rightPoint.position.x;
        Destroy(leftPoint.gameObject);
        Destroy(rightPoint.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        //Move();
        SwitchAnim();
    }

    void Move()
    {
        if (isleft)
        {
            if (coll.IsTouchingLayers(ground))
            {
                anim.SetBool("jumping", true);
                rb.velocity = new Vector2(-speed, jump);
            }
            if (transform.position.x < left)
            {
                transform.localScale = new Vector3(-1, 1, 1);
                isleft = false;
            }
        } else
        {
            if (coll.IsTouchingLayers(ground))
            {
                anim.SetBool("jumping", true);
                rb.velocity = new Vector2(speed, jump);
            }
            if (transform.position.x > right)
            {
                transform.localScale = new Vector3(1, 1, 1);
                isleft = true;
            }
        }
    }

    void SwitchAnim() 
    {
        if (anim.GetBool("jumping"))
        {
            if (rb.velocity.y < 0)
            {
                anim.SetBool("jumping", false);
                anim.SetBool("failing", true);
            }
        }
        if (coll.IsTouchingLayers(ground) && anim.GetBool("failing")) 
        {
            anim.SetBool("failing", false);
        }
    }
}
