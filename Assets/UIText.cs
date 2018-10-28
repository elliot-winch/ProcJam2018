using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public static class UIText {

       // Use this for initialization

	
	// Update is called once per frame
    public static void Write (PGF current, TextMeshProUGUI description) {
        if (current != null && description != null){
            /*
            description.text = "";
            description.text += "GUN " + current.MetaData.name + "\n";
            description.text += "Base Damage: " + current.ImpactDamageData.baseDamage + "\n";
            description.text += "Damage DropOff: " + current.ImpactDamageData.damageDropOff + "\n";
            description.text += "ROF baseRate: " + current.RateOfFireData.baseRate + "\n";
            description.text += "ROF Arr: " + current.RateOfFireData.burstData.ToString() + "\n";
            //description.text += "Distance before spread: " + current.ProjectileTrajectoryData.timeBeforeSpread + "\n";
            description.text += "Speed: " + current.ProjectileTrajectoryData.initialSpeed + "\n";
            description.text += "Spread Angle: " + current.ProjectileTrajectoryData.maxSpreadAngle + "\n";
            */

            string gunText = JsonUtility.ToJson(current.Data);

            description.text = gunText;
        }
        else{
            description.text = "NO GUN";
        }

    }
}
