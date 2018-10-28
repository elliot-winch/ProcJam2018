using System;


[Serializable]
public class PGFAreaDamageData : ScoreableData
{
    #region Defintions
    #region Limits
    private const float MAX_DAMAGE = 100f;
    private const float MIN_DAMAGE = 1f;

    private const float MAX_BLAST_RANGE = 1f;
    private const float MIN_BLAST_RANGE = 0.01f;

    private const float MAX_NUM_IMPACTS = 10f;
    private const float MIN_NUM_IMPACTS = 1f;


    //get rid of these, calculate range based on Vo and G, 
    private const float MAX_TIME_TO_DETONATE = 10F;
    private const float IDEAL_TIME_TO_DETONATE = 3F;
    private const float MIN_TIME_TO_DETONATE = 1F;

    private const float MAX_DISTANCE_TO_DETONATE = 10F;
    private const float MIN_DISTANCE_TO_DETONATE = 1F;

    private const float MAX_LOW_SPEED_DETONATION = 0.2F;
    private const float MIN_LOW_SPEED_DETONATION = 0.01F;
    #endregion

    #region Score Weights
    //Constants - Score Weights
    [NonSerialized]
    private const float damageWeight = 0.34f;
    [NonSerialized]
    private const float blastRangeWeight = 0.33f;
    [NonSerialized]
    private const float rangeWeight = 0.33f;
    #endregion
    #endregion

    public LinScoredFloat maxDamage;
    public LinScoredFloat maxBlastRange;

    //N.B. these together make a single feature
    //When to explode
    //THe number of times this object can collider before exploding
    //1 = rocket like behaviour - explodes on 1st impact
    public LinScoredFloat numImpactsToDetonate;
    public LinScoredFloat timeToDetonate;
    public LinScoredFloat maxDistanceBeforeDetonation;
    public LinScoredFloat minSpeedBeforeDetonation;

    public PGFAreaDamageData()
    {
        this.maxDamage = new LinScoredFloat(RandomUtility.RandFloat(MIN_DAMAGE, MAX_DAMAGE), MIN_DAMAGE, MAX_DAMAGE, MAX_DAMAGE);
        this.maxBlastRange = new LinScoredFloat(RandomUtility.RandFloat(MIN_BLAST_RANGE, MAX_BLAST_RANGE), MIN_BLAST_RANGE, MAX_BLAST_RANGE, MAX_BLAST_RANGE);

        this.numImpactsToDetonate = new LinScoredFloat(RandomUtility.RandFloat(MIN_NUM_IMPACTS, MAX_NUM_IMPACTS), MIN_NUM_IMPACTS, MAX_NUM_IMPACTS, MAX_NUM_IMPACTS);
        this.timeToDetonate = new LinScoredFloat(RandomUtility.RandFloat(MIN_TIME_TO_DETONATE, MAX_TIME_TO_DETONATE), MIN_TIME_TO_DETONATE, MAX_TIME_TO_DETONATE, IDEAL_TIME_TO_DETONATE);

        this.maxDistanceBeforeDetonation = new LinScoredFloat(RandomUtility.RandFloat(MIN_DISTANCE_TO_DETONATE, MAX_DISTANCE_TO_DETONATE), MIN_DISTANCE_TO_DETONATE, MAX_DISTANCE_TO_DETONATE, MAX_DISTANCE_TO_DETONATE);

        this.minSpeedBeforeDetonation = new LinScoredFloat(RandomUtility.RandFloat(MIN_LOW_SPEED_DETONATION, MAX_LOW_SPEED_DETONATION), MIN_LOW_SPEED_DETONATION, MAX_LOW_SPEED_DETONATION, MIN_LOW_SPEED_DETONATION);
    }

    public override float CalculateScore()
    {
        //Normalised to remain in range 0 to 1
        float rangeScore = numImpactsToDetonate.Score + timeToDetonate.Score + maxDistanceBeforeDetonation.Score + minSpeedBeforeDetonation.Score / 4f;

        return maxDamage.Score * damageWeight + maxBlastRange.Score * blastRangeWeight + rangeScore * rangeWeight;
    }
}
