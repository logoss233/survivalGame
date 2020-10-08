using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 在normal的基础上增加跳跃检验
/// </summary>
public class Normal2Ai : AI
{
    Character cha;
    float dir = 0;

    float changeTimer = 2f;
    float minTimer = 0.5f;
    float maxTimer = 2f;

    float frontCheckFar = 0.5f;
    float jumpCheckFar = 2.5f;
    private void Awake()
    {
        cha = GetComponent<Character>();
        dir = Random.Range(0f, 360f);
        resetTimer();
    }
    void resetTimer()
    {
        changeTimer = Random.Range(minTimer, maxTimer);
    }
    // Update is called once per frame
    void Update()
    {
        changeTimer -= Time.deltaTime;
        if (changeTimer <= 0)
        {
            resetTimer();
            dir = Random.Range(0f, 360f);
        }


        //遇到边界，往里走
        if (transform.position.x < -11 || transform.position.x > 12 || transform.position.z > 6 || transform.position.z < -6)
        {
            float radius = Mathf.Atan2(-transform.position.z, -transform.position.x);
            float a = radius * 180 / Mathf.PI;
            if (a < 0)
            {
                a = a + 360;
            }
            dir = a;
        }

        //检测地面不能走了，换方向
        if (cha.isGround && !frontCheck() && !jumpCheck())
        {
            resetTimer();
            dir = Random.Range(0f, 360f);
        }


        cha.dir = dir;
    }

    private bool frontCheck()
    {
        float x = transform.position.x + Mathf.Cos(dir / 180 * Mathf.PI) * frontCheckFar;
        float z = transform.position.z + Mathf.Sin(dir / 180 * Mathf.PI) * frontCheckFar;
        Ray ray = new Ray(new Vector3(x, transform.position.y, z), Vector3.down);
        return Physics.Raycast(ray, 1f, LayerMask.GetMask("Floor"));
    }
    private bool jumpCheck()
    {
        float x = transform.position.x + Mathf.Cos(dir / 180 * Mathf.PI) * jumpCheckFar;
        float z = transform.position.z + Mathf.Sin(dir / 180 * Mathf.PI) * jumpCheckFar;
        Ray ray = new Ray(new Vector3(x, transform.position.y, z), Vector3.down);
        return Physics.Raycast(ray, 1f, LayerMask.GetMask("Floor"));
    }

    private void OnDrawGizmos()
    {

        float x = transform.position.x + Mathf.Cos(dir / 180 * Mathf.PI) * frontCheckFar;
        float z = transform.position.z + Mathf.Sin(dir / 180 * Mathf.PI) * frontCheckFar;
        Gizmos.DrawLine(new Vector3(x, transform.position.y, z), new Vector3(x, transform.position.y - 1f, z));

        float x2 = transform.position.x + Mathf.Cos(dir / 180 * Mathf.PI) * jumpCheckFar;
        float z2 = transform.position.z + Mathf.Sin(dir / 180 * Mathf.PI) * jumpCheckFar;
        Gizmos.DrawLine(new Vector3(x2, transform.position.y, z2), new Vector3(x2, transform.position.y - 1f, z2));
    }
}
