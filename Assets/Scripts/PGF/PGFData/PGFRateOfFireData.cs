using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PGFRateOfFireData : ScoreableData
{
    //NOt a score based range
    private const int MIN_NUM_BURSTS = 0;
    private const int MAX_NUM_BURSTS = 5;

    private const float baseRateWeight = 0.5f;
    private const float reloadingWeight = 0.3f;
    private const float burstWeight = 0.2f;

    //r0
    public LinScoredFloat baseRate;
    //Separated out for clarity, since players can trigger the reload manually
    //and when we call this data, we have to reset the ammo count
    public PGFBurstData reloadingData;
    [SerializeField]
    public List<PGFBurstData> burstData;

    public PGFRateOfFireData()
    {
        this.baseRate = new LinScoredFloat(RandomUtility.RandFloat(PGFBurstData.MIN_SECONDS_BETWEEN_SHOTS, PGFBurstData.MAX_SECONDS_BETWEEN_SHOTS), PGFBurstData.MIN_SECONDS_BETWEEN_SHOTS, PGFBurstData.MAX_SECONDS_BETWEEN_SHOTS, PGFBurstData.MIN_SECONDS_BETWEEN_SHOTS);

        this.reloadingData = new PGFBurstData();

        this.burstData = new List<PGFBurstData>();

        int numBurstDatas = (int)RandomUtility.RandFloat(MIN_NUM_BURSTS, MAX_NUM_BURSTS);

        for (int i = 0; i < numBurstDatas; i++)
        {
            burstData.Add(new PGFBurstData());
        }

        //Sort by N
        burstData.Sort((A, B) => { return A.n.CompareTo(B.n); });
    }

    public override float CalculateScore()
    {
        float score = baseRate.Score * baseRateWeight + reloadingData.CalculateScore() * reloadingWeight;

        float bd_score = 0;
        foreach(var bd in burstData)
        {
            bd_score += bd.CalculateScore();
        }

        bd_score /= burstData.Count;

        return score + bd_score;
    }
}


[Serializable]
public class PGFBurstData : ScoreableData
{
    private const float MIN_BURST_SIZE = 15;
    private const float MAX_BURST_SIZE = 50;

    public const float MIN_SECONDS_BETWEEN_SHOTS = 0.01F;
    public const float MAX_SECONDS_BETWEEN_SHOTS = 0.5F;

    private const float nWeight = 0.5f;
    private const float rWeight = 0.5f;

    public LinScoredFloat n;//umberOfShotsInBurst
    public LinScoredFloat r;//SecondsBetweenShots

    public PGFBurstData()
    {
        this.n = new LinScoredFloat((int)RandomUtility.RandFloat(MIN_BURST_SIZE, MAX_BURST_SIZE), MIN_BURST_SIZE, MAX_BURST_SIZE, MAX_BURST_SIZE);
        this.r = new LinScoredFloat(RandomUtility.RandFloat(MIN_SECONDS_BETWEEN_SHOTS, MAX_SECONDS_BETWEEN_SHOTS), MIN_SECONDS_BETWEEN_SHOTS, MAX_SECONDS_BETWEEN_SHOTS, MIN_SECONDS_BETWEEN_SHOTS);
    }

    public override float CalculateScore()
    {
        return n.Score * nWeight + r.Score * rWeight;
    }
}
