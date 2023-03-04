using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Frog : MonoBehaviour
{
    public Transform leftPoint,rightPoint;
    public float speed = 10f;
    private Rigidbody2D rb;
    private bool isleft = true;
    private float left, right;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        left = leftPoint.position.x;
        right = rightPoint.position.x;
        Destroy(leftPoint.gameObject);
        Destroy(rightPoint.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void Move()
    {
        if (isleft)
        {
            rb.velocity = new Vector2(-speed, rb.velocity.y);
            if (transform.position.x < left)
            {
                transform.localScale = new Vector3(-1, 1, 1);
                isleft = false;
            }
        } else
        {
            rb.velocity = new Vector2(speed, rb.velocity.y);
            if (transform.position.x > right)
            {
                transform.localScale = new Vector3(1, 1, 1);
                isleft = true;
            }
        }
    }
}
