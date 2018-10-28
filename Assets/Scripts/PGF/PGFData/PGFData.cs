using System;
using UnityEngine;

public abstract class ScoreableData
{
    /// <summary>
    /// Provides a value from 0 to 1 determining how 'good' this data is
    /// </summary>
    /// <returns></returns>
    public abstract float CalculateScore();
}

[Serializable]
public class PGFData : ScoreableData
{
    [SerializeField]
    public PGFMetaData meta;
    [SerializeField]
    public PGFProjectileData projectile;
    [SerializeField]
    public PGFRateOfFireData rateOfFire;

    //Weights should sum to one
    private const float projectileScoreWeight = 0.5f;
    private const float rateOfFireScoreWeight = 0.5f;

    public PGFData()
    {
        this.projectile = new PGFProjectileData();
        this.rateOfFire = new PGFRateOfFireData();
    }

    public override float CalculateScore()
    {        
        return projectile.CalculateScore() * projectileScoreWeight + rateOfFire.CalculateScore() * rateOfFireScoreWeight;
    }
}

[Serializable]
public class PGFProjectileData : ScoreableData
{
    [SerializeField]
    public PGFImpactDamageData ImpactDamage;
    [SerializeField]
    public PGFAreaDamageData AreaDamage;
    [SerializeField]
    public PGFTrajectoryData Trajectory;

    //Weights should sum to one
    private const float impactScoreWeight = 0.33f;
    private const float areaScoreWeight = 0.33f;
    private const float trajectoryScoreWeight = 0.34f;

    public PGFProjectileData()
    {
        this.ImpactDamage = new PGFImpactDamageData();
        this.AreaDamage = new PGFAreaDamageData();
        this.Trajectory = new PGFTrajectoryData();
    }

    public override float CalculateScore()
    {
        return ImpactDamage.CalculateScore() * impactScoreWeight
            + AreaDamage.CalculateScore() * areaScoreWeight
            + Trajectory.CalculateScore() * trajectoryScoreWeight;
    }
}

[Serializable]
public class PGFMetaData
{
    public string name;
    public string type;
}