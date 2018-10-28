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
    #endregion

    #region Score Weights
    //Constants - Score Weights
    [NonSerialized]
    private const float damageWeight = 0.34f;
    [NonSerialized]
    private const float blastRangeWeight = 0.33f;
    [NonSerialized]
    private const float impactCountWeight = 0.33f;
    #endregion
    #endregion

    public LinScoredFloat maxDamage;
    public LinScoredFloat maxBlastRange;

    //The number of times this object can collider before exploding
    //1 = rocket like behaviour - explodes on 1st impact
    public LinScoredFloat numImpactsToDetonate;

    public PGFAreaDamageData()
    {
        this.maxDamage = new LinScoredFloat(RandomUtility.RandFloat(MIN_DAMAGE, MAX_DAMAGE), MIN_DAMAGE, MAX_DAMAGE, MAX_DAMAGE);
        this.maxBlastRange = new LinScoredFloat(RandomUtility.RandFloat(MIN_BLAST_RANGE, MAX_BLAST_RANGE), MIN_BLAST_RANGE, MAX_BLAST_RANGE, MAX_BLAST_RANGE);

        this.numImpactsToDetonate = new LinScoredFloat(RandomUtility.RandFloat(MIN_NUM_IMPACTS, MAX_NUM_IMPACTS), MIN_NUM_IMPACTS, MAX_NUM_IMPACTS, MAX_NUM_IMPACTS);
    }

    public override float CalculateScore()
    {
        //Normalised to remain in range 0 to 1
        return maxDamage.Score * damageWeight + maxBlastRange.Score * blastRangeWeight + numImpactsToDetonate.Score * impactCountWeight;
    }
}
