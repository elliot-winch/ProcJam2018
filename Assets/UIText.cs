using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIText : MonoBehaviour {

       // Use this for initialization

	
	// Update is called once per frame
    public void Write (GameObject current, TextMeshProUGUI description) {
        if (current != null && description != null){
            description.text += "GUN " + current.GetComponent<PGF>().MetaData.name + "\n";
            description.text += "Base Damage: " + current.GetComponent<PGF>().DamageData.baseDamage + "\n";
            description.text += "Damage DropOff: " + current.GetComponent<PGF>().DamageData.damageDropOff + "\n";
            description.text += "ROF baseRate: " + current.GetComponent<PGF>().RateOfFireData.baseRate + "\n";
            description.text += "ROF Arr: " + current.GetComponent<PGF>().RateOfFireData.ROFDataArr.ToString() + "\n";
            description.text += "Distance before spread: " + current.GetComponent<PGF>().ProjectileTrajectoryData.distanceBeforeSpread + "\n";
            description.text += "Speed: " + current.GetComponent<PGF>().ProjectileTrajectoryData.speed + "\n";
            description.text += "Spread Angle: " + current.GetComponent<PGF>().ProjectileTrajectoryData.spreadAngle + "\n";
        }

    }

    public void Reset(TextMeshProUGUI description)
    {
        description.text = "";
    }
}
