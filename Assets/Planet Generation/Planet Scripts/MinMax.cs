using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinMax
{
    public float Min;
    public float Max;

    public float Minf;
    public float Maxf;

    public MinMax() 
    {
        Min = float.MaxValue;
        Max = float.MinValue;

        Minf = float.MaxValue;
        Maxf = float.MinValue;
    }

    public void AddValue(float v) 
    {
        if (v > Maxf) {
            Max = v + 0.16f;
            Maxf = v;
        }
        if (v < Minf) {
            Min = v - 0.04f;
            Minf = v;
        }

    }
}
