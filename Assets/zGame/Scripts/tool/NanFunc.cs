using UnityEngine;
using UnityEditor;
using System;
using System.Collections;
using System.Collections.Generic;

public class NanFunc 
{
    /// <summary>
    /// 洗牌算法
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="listtemp"></param>
    public static void Reshuffle<T>(List<T> listtemp)
    {
        //随机交换
        System.Random ram = new System.Random();
        int currentIndex;
        T tempValue;
        for (int i = 0; i < listtemp.Count; i++)
        {
            currentIndex = ram.Next(0, listtemp.Count - i);
            tempValue = listtemp[currentIndex];
            listtemp[currentIndex] = listtemp[listtemp.Count - 1 - i];
            listtemp[listtemp.Count - 1 - i] = tempValue;
        }
    }
}