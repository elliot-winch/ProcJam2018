using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PGF : MonoBehaviour
{
    private bool canFire = true;

    public GameObject projectilePrefab;
    public Projectile Fire(Vector3 direction, Vector3 position, int ammoRemaining)
    {
        if (canFire)
        {
            var ProjectileObject = Instantiate(projectilePrefab);
            var projectileComponent = ProjectileObject.GetComponent<Projectile>();
            //set projectile components
            projectileComponent.DamageData = DamageData;
            //we'll calculate the direction you're facing (first parameter) later, barrel's position
            projectileComponent.TrajectoryData = ProjectileTrajectoryData;
            float r = GetWaitTime(ammoRemaining);
            StartCoroutine(WaitForRateOfFire(r));
            return projectileComponent;
        }
        else
        {
            Debug.Log("Waiting for Rate of Fire");
            return null;
        }
    }

    IEnumerator WaitForRateOfFire(float waitTime)
    {
        canFire = false;
        yield return new WaitForSeconds(waitTime);
        canFire = true;
    }

    private float GetWaitTime(int ammoRemaining){
        foreach(PGFBurstData x in RateOfFireData.ROFDataArr){
            if ((ammoRemaining % x.n) == 0) 
            {
                return x.r;
            }
        }
        return RateOfFireData.baseRate;
    }


    // property of class, private variable public getter
    // not serializable, you need to call the getter
    public PGFDamageData DamageData { get; set; }

    //metadata
    public PGFMetaData MetaData { get; set; }

    public PGFRateOfFireData RateOfFireData { get; set; }

                //parameters      //return value
    public PGFProjectileTrajectoryData ProjectileTrajectoryData {get; set;}
}
