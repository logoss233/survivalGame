 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [HideInInspector]
    public float dir = -1; //方向 -1表示停止不动
    public float moveSpeed = 2f;
    public float jumpSpeed = 4f;
    public float rayLength = 1.1f;
    [HideInInspector]
    public bool isGround = false;
    Rigidbody rb;
    Animator ani;
    Transform model;

    private float face = 0;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        ani = transform.Find("Model").GetComponent<Animator>();
        model=transform.Find("Model");
        
    }
    private void FixedUpdate()
    {
        bool preIsGround = isGround;
        isGround=checkGround();
        if (dir == -1)
        {
            rb.velocity = new Vector3(0, rb.velocity.y,0);
        }
        else
        {
            rb.velocity = new Vector3(moveSpeed * Mathf.Cos(dir * Mathf.PI / 180f),
                rb.velocity.y,
                moveSpeed*Mathf.Sin(dir*Mathf.PI/180f));
        }
        if (dir >= 0)
        {
            face = dir;
            model.rotation = Quaternion.Euler(0, -face+90, 0);
        }
        //自动起跳
        if(preIsGround && !isGround)
        {
            rb.velocity = new Vector3(rb.velocity.x, jumpSpeed, rb.velocity.y);
            playAni("Jump");
        }

        //动画
        if (isGround)
        {
            if (dir < 0)
            {
                playAni("Idle");
            }
            else
            {
                playAni("Walk");
            }
        }
        else
        {
            if (currentAni != "Jump")
            {
                currentAni = "Jump";
                ani.Play("Jump", 0, 1);
            }
        }
        
    }

    private string currentAni = "";
    void playAni(string aniName)
    {
        if (currentAni == aniName)
        {
            return;
        }
        currentAni = aniName;
        ani.Play(aniName);
    }

    private bool checkGround()
    {
        Ray ray = new Ray(transform.position, Vector3.down);
        return Physics.Raycast(ray, rayLength, LayerMask.GetMask("Floor"));
    }

    private void OnGUI()
    {
        //GUI.TextArea(new Rect(0, 0, 200, 30), "dir:" + dir);
        //GUI.TextArea(new Rect(0, 30, 200, 30), "isGround:" + isGround);
        //GUI.TextArea(new Rect(0, 60, 200, 30), "face:" + face);
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down*rayLength);
        
    }
}
