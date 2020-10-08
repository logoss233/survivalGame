using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 最傻逼的ai，随机移动
/// </summary>
public class NoobAi : AI
{
    Character cha;
    float dir = 0;

    float changeTimer = 2f;
    float minTimer = 0.5f;
    float maxTimer = 2f;

    private void Awake()
    {
        cha = GetComponent<Character>();
        dir= Random.Range(0f, 360f);
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

        cha.dir = dir;
    }
}
