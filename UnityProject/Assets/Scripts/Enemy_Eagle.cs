using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Eagle : Enemy
{
    public Transform TopPoint, BottomPoint;
    public float speed = 10f;
    //private Rigidbody2D rb;
    //private Animator anim;
    private bool isUp = true;
    private float top, bottom;

    protected override void Start()
    {
        base.Start();
        //rb = GetComponent<Rigidbody2D>();
        //anim = GetComponent<Animator>();
        top = TopPoint.position.y;
        bottom = BottomPoint.position.y;
        Destroy(TopPoint.gameObject);
        Destroy(BottomPoint.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }


    void Move() 
    {
        if (isUp)
        {
            rb.velocity = new Vector2(rb.velocity.x, speed);
            if (transform.position.y > top)
            {
                isUp = false;
            }
        }
        else
        {
            rb.velocity = new Vector2(rb.velocity.x, -speed);
            if (transform.position.y < bottom)
            {
                isUp = true;
            }
        }
    }

}
