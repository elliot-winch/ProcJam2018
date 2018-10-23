using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PGF : MonoBehaviour
{
    [Header("Projectile Spawning")]
    public GameObject projectilePrefab;
    [Tooltip("Where the projectile will spawn from and its initial direction (z-axis)")]
    public Transform barrelTip;

    //Data Properties
    //Generated and set by the factory
    public PGFDamageData DamageData { get; set; }
    public PGFRateOfFireData RateOfFireData { get; set; }
    public PGFProjectileTrajectoryData ProjectileTrajectoryData { get; set; }
    public PGFMetaData MetaData { get; set; }

    public bool CanFire
    {
        get
        {
            //Right now, a PGF being able to fire it
            //only determined by its ROF
            return waitingForROF;
        }
    }

    //Used to control rate of fire
    private bool waitingForROF = true;
    
    /// <summary>
    /// Fire the PGF. Produces a projectile based on PGF Data
    /// </summary>
    /// <param name="direction"></param>
    /// <param name="position"></param>
    /// <param name="ammoRemaining"></param>
    /// <returns></returns>
    public Projectile Fire(Vector3 direction, Vector3 position, int ammoRemaining)
    {
        if (waitingForROF)
        {

            //Spawn the projectile
            var projectileObject = Instantiate(projectilePrefab);
            projectileObject.transform.position = barrelTip.position;
            var projectileComponent = projectileObject.GetComponent<Projectile>();

            //set projectile data
            projectileComponent.DamageData = DamageData;
            projectileComponent.TrajectoryData = ProjectileTrajectoryData;

            //Controlling ROF
            float waitTime = GetWaitTime(ammoRemaining);
            StartCoroutine(WaitForRateOfFire(waitTime));

            return projectileComponent;
        }
        else
        {
            //Debug.Log("Waiting for Rate of Fire");
            return null;
        }
    }

    #region Rate Of Fire
    /// <summary>
    /// Prevents the PGF for firing. Used to control rate of fire
    /// </summary>
    /// <param name="waitTime"></param>
    /// <returns></returns>
    IEnumerator WaitForRateOfFire(float waitTime)
    {
        waitingForROF = false;
        yield return new WaitForSeconds(waitTime);
        waitingForROF = true;
    }

    /*
     * N is the number of bullets in a burst. 
     * A burst can be a single bullet, or 
     * represent burst fire (e.g. triple shot)
     * or represent reload time. In fact, the 
     * largest N is the clip size
     * 
     * For decreasing N, we see if the ammo
     * remaining is divisible by N. If so, we return
     * the associated wait time. 
     * We run by decreasing values, since if a number
     * is divisible by x, then it will be divisible
     * by x * y, so larger N would be ignored.
     * 
     */ 
    /// <summary>
    /// Finds the correct wait time from the provided possible times.
    /// </summary>
    /// <param name="ammoRemaining"></param>
    /// <returns></returns>
    private float GetWaitTime(int ammoRemaining){
        foreach(PGFBurstData x in RateOfFireData.ROFDataArr){
            if ((ammoRemaining % x.n) == 0) 
            {
                return x.r;
            }
        }
        return RateOfFireData.baseRate;
    }

    #endregion
}
