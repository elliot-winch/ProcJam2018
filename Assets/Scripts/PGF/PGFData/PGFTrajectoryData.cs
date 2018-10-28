using System;
using UnityEngine;

[Serializable]
public class PGFTrajectoryData : ScoreableData
{
    #region Defintions
    #region Limits
    private const float MAX_SPEED = 200f;
    private const float MIN_SPEED = 50f;

    private const float MAX_SPREAD = Mathf.PI / 4.0f;
    private const float MIN_SPREAD = 0f;

    private const float MAX_DROPOFF = .5f;
    private const float MIN_DROPOFF = 0f;
    #endregion

    #region Score Weights
    //Constants - Score Weights
    [NonSerialized]
    private const float speedWeight = 0.34f;
    [NonSerialized]
    private const float spreadWeight = 0.33f;
    [NonSerialized]
    private const float dropoffWeight = 0.33f;
    #endregion
    #endregion

    public LinScoredFloat initialSpeed;
    public LinScoredFloat maxInitialSpreadAngle; //measured in angle per meter per second

    //Removed - non-continuous acceleration becomes hard to model
    //public float timeBeforeSpread;

    public LinScoredFloat dropOffRatio; // a ratio

    //Range is calculaed by AreaDamage - when it explodes determines the range
    //public float range;

    public PGFTrajectoryData()
    {
        this.maxInitialSpreadAngle = new LinScoredFloat(RandomUtility.RandFloat(MIN_SPREAD, MAX_SPREAD), MIN_SPREAD, MAX_SPREAD, 0f);
        this.initialSpeed = new LinScoredFloat(RandomUtility.RandFloat(MIN_SPEED, MAX_SPEED), MIN_SPEED, MAX_SPEED, MAX_SPEED);
        this.dropOffRatio = new LinScoredFloat(RandomUtility.RandFloat(MIN_DROPOFF, MAX_DROPOFF), MIN_DROPOFF, MAX_DROPOFF, MIN_DROPOFF);
    }

    public override float CalculateScore()
    {
        return initialSpeed.Score * speedWeight + maxInitialSpreadAngle.Score * spreadWeight + dropOffRatio.Score * dropoffWeight;
    }
}
