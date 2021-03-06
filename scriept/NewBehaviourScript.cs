using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
     public float MoveForce = 100.0f;   //角色移动力度
    public float MaxSpeed = 5;   //初始水平速度为5
    public Rigidbody2D HeroBody;
    [HideInInspector]
    public bool bFaceRight = true;  
    [HideInInspector]
    public bool bJump = false;  //角色初始状态是禁止的
    public float JumpForce = 100; //角色跳跃力度
    private Transform mGroundCheck;  //查看是否为地面
    // Start is called before the first frame update
    void Start()
    {
        HeroBody = GetComponent<Rigidbody2D>();  //引用组件Rigidbody2D
        mGroundCheck = transform.Find("GroundCheck");  //是否找到地面
    }

    // Update is called once per frame
    void Update()
    {
         float h = Input.GetAxis("Horizontal");  //获取水平轴
        if(Mathf.Abs(HeroBody.velocity.x)<MaxSpeed)
        {
            HeroBody.AddForce(Vector2.right * h * MoveForce);
        }

        if(Mathf.Abs(HeroBody.velocity.x)>5)
        {
            HeroBody.velocity = new Vector2(Mathf.Sign(HeroBody.velocity.x) * MaxSpeed,
                                            HeroBody.velocity.y);
        }

        if(h>0 && !bFaceRight)
        {
            flip();
        }
        else if(h<0 && bFaceRight)
        {
            flip();
        }

        if (Physics2D.Linecast(transform.position, mGroundCheck.position,
                                1<<LayerMask.NameToLayer("Ground")))
        {
            if(Input.GetButtonDown("Jump"))
            {
                bJump = true;
            }
        }

    }

      private void FixedUpdate()
    {
        if(bJump)
        {
            HeroBody.AddForce(Vector2.up * JumpForce);
            bJump = false;
        }
    }

    private void flip()
    {
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
        bFaceRight = !bFaceRight;
    }
}
