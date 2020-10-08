using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 遇到边界会往里走
/// </summary>
public class NoobPlusAi : AI
{
    Character cha;
    float dir = 0;

    float changeTimer = 2f;
    float minTimer = 0.5f;
    float maxTimer = 2f;

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
        if (transform.position.x < - 11 || transform.position.x > 12 || transform.position.z > 6 || transform.position.z < -6)
        {
            float radius=Mathf.Atan2(-transform.position.z, -transform.position.x);
            float a = radius * 180 / Mathf.PI;
            if (a < 0)
            {
                a = a + 360;
            }
            dir = a;
        }

        cha.dir = dir;
    }
}
