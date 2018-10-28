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
        set
        {
            this.value = value;

            CalcScore();
        }
    }

    public float Min { get; private set; }
    public float Max { get; private set; }
    public float Ideal { get; private set; }
    public float Score { get; private set; }

    public LinScoredFloat(float value, float min, float max, float idealValue)
    {
        this.Value = value;

        this.Min = min;
        this.Max = max;
        this.Ideal = idealValue;

        CalcScore();
    }

    private void CalcScore()
    {

        //The score is calculated by:
        //1) Find the larger distance between ideal and min and max and ideal.
        //2) Calculate the gradient for x0 = ideal, x1= min / max (dep. on step 1), y0 = 0, y1 = 1
        //3) Multiply by the disntance the value is from the ideal (i.e. use the gradient to give a y for some x)
        this.Score = 1 - (Mathf.Abs(value - this.Ideal) / Mathf.Max(this.Ideal - this.Min, this.Max - this.Ideal));
    }

    public int CompareTo(object obj)
    {
        return Value.CompareTo(((LinScoredFloat)obj).Value);
    }

    public override string ToString()
    {
        return string.Format("Value: {0}, Min: {1}, Max: {2}, Score: {3}", this.Value, this.Min, this.Max, this.Score);
    }
}