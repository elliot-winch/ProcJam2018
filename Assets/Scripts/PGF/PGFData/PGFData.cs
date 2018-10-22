using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class PGFData{

    public PGFDamageData PGFDamageData;
}

[System.Serializable]
public class PGFDamageData {

    // baseDamage : -1 -> 1 (negative values mean healing)
    public float baseDamage;

    // damageDropOff : baseDamage * distanceTravelled
    public float damageDropOff;
	
}

[System.Serializable]
public class PGFRateOfFireData {

    //r0
    public float baseRate;
    public PGFBurstData[] ROFDataArr;
}

[System.Serializable]
public struct PGFBurstData {
    public int n;//umberOfShotsInBurst
    public float r;//ateOfSecondsBetweenShots
}

[System.Serializable]
public class PGFProjectileTrajectoryData
{
    public float speed;
    public float distanceBeforeSpread;
    public float spreadAngle;
}

[System.Serializable]
public class PGFMetaData
{
    public string name;
    public string type;
}