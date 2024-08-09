using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Tools
{
    /// <summary>
    /// 打乱顺序
    /// </summary>
    public static void Disrupt<T>(T[] array, int n)
    {
        for (int i = 0; i < n; i++)
        {
            int a = Random.Range(0, array.Length);
            int b = Random.Range(0, array.Length);
            T temp = array[a];
            array[a] = array[b];
            array[b] = temp;
        }
    }
}
