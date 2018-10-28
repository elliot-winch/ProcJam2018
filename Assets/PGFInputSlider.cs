using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PGFInputSlider : MonoBehaviour {

    public enum PGFDataField
    {
        ImpactDamage,
        ImpactDamageDropoff
    }

    public PGFDataField field;
    public Slider slider;
    public TextMeshProUGUI text;

    private LinScoredFloat scoredFloat;

    private void Start()
    {
        TestPGFOne.Instance.onSetCurrentPGF.AddListener((pgf) =>
        {
            this.scoredFloat = GetPGFValue(TestPGFOne.Instance.Current);

           
        });

        if(TestPGFOne.Instance.Current != null)
        {
            this.scoredFloat = GetPGFValue(TestPGFOne.Instance.Current);
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
