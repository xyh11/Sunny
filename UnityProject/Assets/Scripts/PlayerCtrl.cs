//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerCtrl : MonoBehaviour
{
    public float moveSpeed = 40f;
    public float jumpForce = 10f;
    public LayerMask ground;//����
    public Collider2D coll;
    public Text textCherry;
    public Text textGems;

    private int cherry = 0;
    private int gems = 0;
    private Rigidbody2D rb;
    private Animator anim;
    private bool isHurt = false;


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
        if (!isHurt)
        {
            Move();
        }
        SwitchAnim();
    }

    private void Update()
    {
        Jump();

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
            rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);
            anim.SetFloat("running",Mathf.Abs(isFaceRight));
        }
        //��ɫת��
        if (isFaceRight != 0)
        {
            transform.localScale = new Vector3(isFaceRight, 1,1);
        }

    }

    void Jump() 
    {
        //��ɫ��Ծ
        if (Input.GetButton("Jump") && coll.IsTouchingLayers(ground))
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
        //Debug.LogWarning(Mathf.Abs(rb.velocity.x));
        //Debug.LogWarning(anim.GetBool("ilde"));


        //anim.SetBool("idle", false);
        //����
        if (rb.velocity.y < 0.1f && !coll.IsTouchingLayers(ground))
        {
            anim.SetBool("failing", true);
        }

        //��Ծת����
        if (anim.GetBool("jumping"))
        {
            if (rb.velocity.y < 0)
            {
                anim.SetBool("jumping", false);
                anim.SetBool("failing", true);
            }
        }
        //����
        else if (isHurt)
        {
            anim.SetBool("hurt", true);
            anim.SetFloat("running", 0);
            if (Mathf.Abs(rb.velocity.x) < 0.1f)
            {
                anim.SetBool("hurt", false);
                //anim.SetBool("idle", true);
                isHurt = false;
                //Debug.LogError(isHurt);
            }
        }
        //���
        else if (coll.IsTouchingLayers(ground))
        {
            anim.SetBool("failing", false);
            anim.SetBool("idle", true);
        }
    }

    /// <summary>
    /// �ռ���Ʒ
    /// </summary>
    /// <param name="collision"></param>
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
        if (collision.tag == "DeadLine") 
        {
            Invoke("Restart", 1f);
        }
    }

    /// <summary>
    /// �������
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            if (anim.GetBool("failing"))
            {
                //Destroy(collision.gameObject);
                enemy.JumpOn();
                rb.velocity = new Vector2(rb.velocity.x, jumpForce / 2);
                anim.SetBool("jumping", true);
            } else if(transform.position.x < collision.transform.position.x)
            {
                rb.velocity = new Vector2(-10, rb.velocity.y);
                isHurt = true;
            }else if (transform.position.x > collision.transform.position.x)
            {
                rb.velocity = new Vector2(10, rb.velocity.y);
                isHurt = true;
            }
        }
        
    }
    void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
