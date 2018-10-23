using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public static class UIText {

       // Use this for initialization

	
	// Update is called once per frame
    public static void Write (PGF current, TextMeshProUGUI description) {
        if (current != null && description != null){
            description.text = "";
            description.text += "GUN " + current.MetaData.name + "\n";
            description.text += "Base Damage: " + current.DamageData.baseDamage + "\n";
            description.text += "Damage DropOff: " + current.DamageData.damageDropOff + "\n";
            description.text += "ROF baseRate: " + current.RateOfFireData.baseRate + "\n";
            description.text += "ROF Arr: " + current.RateOfFireData.ROFDataArr.ToString() + "\n";
            description.text += "Distance before spread: " + current.ProjectileTrajectoryData.distanceBeforeSpread + "\n";
            description.text += "Speed: " + current.ProjectileTrajectoryData.speed + "\n";
            description.text += "Spread Angle: " + current.ProjectileTrajectoryData.spreadAngle + "\n";
        }
        else{
            description.text = "NO GUN";
        }

    }

    public static void WriteAdd(string s, TextMeshProUGUI description) {
        description.text += s;
    }

    public static void WriteReplace(string s, TextMeshProUGUI description)
    {
        description.text = s;
    }
}
