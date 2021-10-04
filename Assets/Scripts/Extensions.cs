using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public static class Extensions
{
    public static bool IsBetweenRange(this float thisValue, float value1, float value2)
    {
        return thisValue >= Mathf.Min(value1, value2) && thisValue <= Mathf.Max(value1, value2);
    }

    public static AudioClip GetRandomItem(this List<AudioClip> list)
    {
        return list.Count != 0 ? list[Random.Range(0, list.Count - 1)] : null;
    }
    
    public static void Shuffle<T>(this IList<T> list)  
    {  
        int n = list.Count;  
        while (n > 1) {  
            n--;  
            int k = Random.Range(0, n + 1);  
            T value = list[k];  
            list[k] = list[n];  
            list[n] = value;  
        }  
    }
}
