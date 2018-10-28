using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class LinScoredFloat : IComparable
{
    //The only serialisable field (for now)
    //The score can be recalculated from anywhere, since the max and min are consts
    [SerializeField]
    private float value;

    //Properties are not serialised
    public float Value { get
        {
            return value;
        }
        private set
        {
            this.value = value;
        }
    }

    public float Score { get; private set; }

    public LinScoredFloat(float value, float min, float max, float idealValue)
    {
        this.Value = value;

        //The score is calculated by:
        //1) Find the larger distance between ideal and min and max and ideal.
        //2) Calculate the gradient for x0 = ideal, x1= min / max (dep. on step 1), y0 = 0, y1 = 1
        //3) Multiply by the disntance the value is from the ideal (i.e. use the gradient to give a y for some x)
        this.Score = Mathf.Abs(value - idealValue) / Mathf.Max(idealValue - min, max - idealValue);
    }

    public int CompareTo(object obj)
    {
        return Value.CompareTo(((LinScoredFloat)obj).Value);
    }
}