using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PGFInputSlider : MonoBehaviour {

    public enum PGFDataField
    {
        ImpactDamage,
        ImpactDamageDropoff,
        AreaDamage,
        BlastRadius,
        ImpactsToDetonation,
        InitialSpeed,
        SpreadAngle,
        Dropoff,
        RateOfFire,
        AmmoCapacity,
        ReloadTime
    }

    public PGFDataField field;
    public Slider slider;
    public TextMeshProUGUI text;

    private LinScoredFloat scoredFloat;
    private LinScoredFloat ScoredFloat
    {
        get
        {
            return scoredFloat;
        }
        set
        {
            scoredFloat = value;
            this.slider.value = Mathf.Lerp(scoredFloat.Min, scoredFloat.Max, scoredFloat.Value);
        }
    }

    private void Start()
    {
        TestPGFOne.Instance.onSetCurrentPGF += (pgf) =>
        {
            this.ScoredFloat = GetPGFValue(pgf);
        };

        if(TestPGFOne.Instance.Current != null)
        {
            this.ScoredFloat = GetPGFValue(TestPGFOne.Instance.Current);
        }
    }

    public LinScoredFloat GetPGFValue(PGF pgf)
    {

        switch (field)
        {
            case PGFDataField.ImpactDamage:
                return pgf.Data.projectile.ImpactDamage.baseDamage;
            case PGFDataField.ImpactDamageDropoff:
                return pgf.Data.projectile.ImpactDamage.damageDropOff;
            case PGFDataField.AreaDamage:
                return pgf.Data.projectile.AreaDamage.maxDamage;
            case PGFDataField.BlastRadius:
                return pgf.Data.projectile.AreaDamage.maxBlastRange;
            case PGFDataField.ImpactsToDetonation:
                return pgf.Data.projectile.AreaDamage.numImpactsToDetonate;
            case PGFDataField.SpreadAngle:
                return pgf.Data.projectile.Trajectory.maxInitialSpreadAngle;
            case PGFDataField.InitialSpeed:
                return pgf.Data.projectile.Trajectory.initialSpeed;
            case PGFDataField.Dropoff:
                return pgf.Data.projectile.Trajectory.dropOffRatio;
            case PGFDataField.RateOfFire:
                return pgf.Data.rateOfFire.baseRate;
            case PGFDataField.AmmoCapacity:
                return pgf.Data.rateOfFire.reloadingData.n;
            case PGFDataField.ReloadTime:
                return pgf.Data.rateOfFire.reloadingData.r;
            default:
                return null;
        }
    }

    public void SetPGFValue(float newValue)
    {
        scoredFloat.Value = Mathf.Lerp(scoredFloat.Min, scoredFloat.Max, newValue);

        UpdateUIText();
    }

    public void UpdateUIText()
    {
        text.text = string.Format("{0}: Value: {1}, Min: {2}, Max: {3}, Score : {4}", this.field.ToString(), this.scoredFloat.Value, this.scoredFloat.Min, this.scoredFloat.Max, this.scoredFloat.Score);
    }
}
