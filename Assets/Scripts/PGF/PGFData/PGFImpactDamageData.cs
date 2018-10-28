using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PGFImpactDamageData : ScoreableData
{
    //Constants - Ranges
    private const float MIN_IMPACT_DAMAGE = 1f;
    private const float MAX_IMPACT_DAMAGE = 100f;
    private const float MIN_DROPOFF = 0f;
    private const float MAX_DROPOFF = 100f;

    //Constants - Score Weights
    [NonSerialized]
    private const float damageWeight = 0.9f;
    [NonSerialized]
    private const float damageDropOffWeight = 0.1f;

    //Variables
    // baseDamage : -1 -> 1 (negative values mean healing)
    [SerializeField]
    public LinScoredFloat baseDamage;

    //A rate of damage change over distance travelled
    [SerializeField]
    public LinScoredFloat damageDropOff;

    public PGFImpactDamageData()
    {
        //For now, using a no args constructor and poission distribution select a value
        this.baseDamage = new LinScoredFloat(value: RandomUtility.RandFloat(MIN_IMPACT_DAMAGE, MAX_IMPACT_DAMAGE),min: MIN_IMPACT_DAMAGE,max: MAX_IMPACT_DAMAGE, idealValue: MAX_IMPACT_DAMAGE);
        this.damageDropOff = new LinScoredFloat(RandomUtility.RandFloat(MIN_DROPOFF, MAX_DROPOFF), MIN_DROPOFF, MAX_DROPOFF, MIN_DROPOFF);
    }

    public override float CalculateScore()
    {
        return baseDamage.Score * damageWeight
            + damageDropOff.Score * damageDropOffWeight;
    }
}
