using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PGFInputSlider : MonoBehaviour {

    public enum PGFDataField
    {
        ImpactDamage,
        ImpactDropoff
    }

    public PGFDataField field;
    public Slider slider;
    public TextMeshPro text;

    private LinScoredFloat scoredFloat;

    private void Start()
    {
        this.scoredFloat = GetPGFValue();
    }

    public LinScoredFloat GetPGFValue()
    {
        var pgf = TestPGFOne.Instance.Current;

        switch (field)
        {
            case PGFDataField.ImpactDamage:
                return pgf.Data.projectile.ImpactDamage.baseDamage;
            case PGFDataField.ImpactDropoff:
                return pgf.Data.projectile.ImpactDamage.damageDropOff;
            default:
                return null;
        }
    }

    public void SetPGFValue(float newValue)
    {
        scoredFloat.Value = Mathf.Lerp(scoredFloat.Min, scoredFloat.Max, newValue);

        text.text = UIString();
    }

    public string UIString()
    {
        return string.Format("", );
    }
}
