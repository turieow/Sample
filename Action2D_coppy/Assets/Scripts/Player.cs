using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpPower = 10f;
    Rigidbody2D rb;
    bool isJump, isRun;
    SpriteRenderer sr;

    Animator anim;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        anim.GetBool("isRun");
        anim.GetBool("isJump");
        sr = GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        Vector2 v = rb.velocity;
        Vector2 scale = transform.localScale;

        // 左右の十字キーで移動        
        v.x = Input.GetAxis("Horizontal") * moveSpeed;
        // スペースキーでジャンプ    
        
        if (Input.GetButtonDown("Jump") && !isJump)
        {
            isJump = true;
            v.y = jumpPower;
        }

        // 移動量をRigidbodyの速度に反映        
        rb.velocity = v;

        if(v.x > 0)
        {
            sr.flipX = false;
            isRun = true;
        }
        else if(v.x < 0)
        {
            sr.flipX = true;
            isRun = true;
        }
        else
        {
            isRun = false;
        }

        anim.SetBool("isRun", isRun);
        anim.SetBool("isJump", isJump);

        Debug.Log(rb.velocity.magnitude);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isJump = false;
        }
    }
}
