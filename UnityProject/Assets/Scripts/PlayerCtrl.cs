//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.UI;

public class PlayerCtrl : MonoBehaviour
{
    public float moveSpeed = 40f;
    public float jumpForce = 10f;
    public LayerMask ground;//地面
    public Collider2D coll;

    private Rigidbody2D rb;
    private Animator anim;



    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        //coll = GetComponent<Collider2D>();
    }

    private void FixedUpdate()
    {
        Move();
        SwitchAnim();
    }

    private void Update()
    {


    }

    /// <summary>
    /// 角色移动控制
    /// </summary>
    private void Move()
    {
        float moveInput = Input.GetAxis("Horizontal");
        float isFaceRight = Input.GetAxisRaw("Horizontal");
        //角色移动 
        if (moveInput != 0)
        { 
            rb.velocity = new Vector2(moveInput * moveSpeed * Time.deltaTime, rb.velocity.y);
            anim.SetFloat("running",Mathf.Abs(isFaceRight));
        }
        //角色转向
        if (isFaceRight != 0)
        {
            transform.localScale = new Vector3(isFaceRight, 1,1);
        }

        //角色跳跃
        if (Input.GetButtonDown("Jump"))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            anim.SetBool("jumping", true);
        }

    }

    /// <summary>
    /// 角色状态动画切换
    /// </summary>
    void SwitchAnim()
    {
        anim.SetBool("idle", false);

        //跳跃转下落
        if (anim.GetBool("jumping"))
        {
            if (rb.velocity.y < 0)
            {
                anim.SetBool("jumping", false);
                anim.SetBool("failing", true);
            }
        }
        //落地
        else if (coll.IsTouchingLayers(ground))
        {
            anim.SetBool("failing",false);
            anim.SetBool("idle", true);
        }
    }



}
