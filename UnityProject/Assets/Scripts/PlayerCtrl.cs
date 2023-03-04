//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCtrl : MonoBehaviour
{
    public float moveSpeed = 40f;
    public float jumpForce = 10f;
    public LayerMask ground;//地面
    public Collider2D coll;
    public Text textCherry;
    public Text textGems;

    private int cherry = 0;
    private int gems = 0;
    private Rigidbody2D rb;
    private Animator anim;



    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        //coll = GetComponent<Collider2D>();
        textCherry.text = "Cherry:" + cherry.ToString();
        textGems.text = "Gems:" + gems.ToString();
    }

    private void FixedUpdate()
    {

    }

    private void Update()
    {
        Move();
        SwitchAnim();

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
            rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);
            anim.SetFloat("running",Mathf.Abs(isFaceRight));
        }
        //角色转向
        if (isFaceRight != 0)
        {
            transform.localScale = new Vector3(isFaceRight, 1,1);
        }

        //角色跳跃
        if (Input.GetButtonDown("Jump") && coll.IsTouchingLayers(ground))
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


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Cherry")
        {
            Destroy(collision.gameObject);
            cherry += 1;
            textCherry.text = "Cherry:" + cherry.ToString();
        } else if (collision.tag == "Gems")
        {
            Destroy(collision.gameObject);
            gems += 1;
            textGems.text = "Gems:" + gems.ToString();
        }
    }



}
