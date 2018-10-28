using System;


[Serializable]
public class PGFTrajectoryData : ScoreableData
{
    #region Defintions
    #region Limits
    private const float MAX_SPEED = 100f;
    private const float MIN_SPEED = 1f;

    private const float MAX_SPREAD = 1f;
    private const float MIN_SPREAD = 0.01f;

    private const float MAX_DROPOFF = 10f;
    private const float MIN_DROPOFF = 1f;
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
        this.maxInitialSpreadAngle = new LinScoredFloat(RandomUtility.RandFloat(MIN_SPREAD, MAX_SPREAD), MIN_SPREAD, MAX_SPREAD, MAX_SPREAD);
        this.initialSpeed = new LinScoredFloat(RandomUtility.RandFloat(MIN_SPEED, MAX_SPEED), MIN_SPEED, MAX_SPEED, MAX_SPEED);
        this.dropOffRatio = new LinScoredFloat(RandomUtility.RandFloat(MIN_DROPOFF, MAX_DROPOFF), MAX_DROPOFF, MIN_DROPOFF, MIN_DROPOFF);
    }

    public override float CalculateScore()
    {
        return initialSpeed.Score * speedWeight + maxInitialSpreadAngle.Score * spreadWeight + dropOffRatio.Score * dropoffWeight;
    }
}
