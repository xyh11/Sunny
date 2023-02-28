//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.UI;

public class PlayerCtrl : MonoBehaviour
{
    public float moveSpeed = 40f;
    public float jumpForce = 10f;
    public LayerMask ground;//����
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
    /// ��ɫ�ƶ�����
    /// </summary>
    private void Move()
    {
        float moveInput = Input.GetAxis("Horizontal");
        float isFaceRight = Input.GetAxisRaw("Horizontal");
        //��ɫ�ƶ� 
        if (moveInput != 0)
        { 
            rb.velocity = new Vector2(moveInput * moveSpeed * Time.deltaTime, rb.velocity.y);
            anim.SetFloat("running",Mathf.Abs(isFaceRight));
        }
        //��ɫת��
        if (isFaceRight != 0)
        {
            transform.localScale = new Vector3(isFaceRight, 1,1);
        }

        //��ɫ��Ծ
        if (Input.GetButtonDown("Jump"))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            anim.SetBool("jumping", true);
        }

    }

    /// <summary>
    /// ��ɫ״̬�����л�
    /// </summary>
    void SwitchAnim()
    {
        anim.SetBool("idle", false);

        //��Ծת����
        if (anim.GetBool("jumping"))
        {
            if (rb.velocity.y < 0)
            {
                anim.SetBool("jumping", false);
                anim.SetBool("failing", true);
            }
        }
        //���
        else if (coll.IsTouchingLayers(ground))
        {
            anim.SetBool("failing",false);
            anim.SetBool("idle", true);
        }
    }



}
