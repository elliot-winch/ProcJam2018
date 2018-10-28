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
    public PGFData Data { get; set; }

    private int currentAmmo; //Ammo remaining in the clip

    public bool CanFire
    {
        get
        {
            //Right now, a PGF being able to fire it
            //only determined by its ROF
            return waitingForROF == false;
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
    public Projectile Fire(Vector3 direction, Vector3 position)
    {

        if (CanFire)
        {
            //Spawn the projectile
            var projectileObject = Instantiate(projectilePrefab);

            projectileObject.transform.position = barrelTip.position;
            projectileObject.transform.forward = barrelTip.forward;

            var projectileComponent = projectileObject.GetComponent<Projectile>();

            //set projectile data
            projectileComponent.Data = Data.projectile;

            //Controlling ROF
            //CUrrent ammo is decremented before being sent to GetWaitTime to avoid the off by one error
            currentAmmo--;

            if (currentAmmo <= 0)
            {
                Reload();
            }
            else
            {
                float waitTime = GetWaitTime(currentAmmo);
                StartCoroutine(WaitForRateOfFire(waitTime));
            }
            return projectileComponent;
        }
        else
        {
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
        foreach(PGFBurstData x in Data.rateOfFire.burstData){
            if ((ammoRemaining % x.n.Value) == 0) 
            {
                return x.r.Value;
            }
        }
        return Data.rateOfFire.baseRate.Value;
    }

    #endregion

    #region Reloading
    /// <summary>
    /// Resets current ammo to max. TODO: remove ammo from some inventory??
    /// </summary>
    public void Reload()
    {
        StartCoroutine(ReloadCo());
    }

    private IEnumerator ReloadCo()
    {
        waitingForROF = true;

        yield return new WaitForSeconds(Data.rateOfFire.reloadingData.r.Value);

        //for now, we are assuming the Overwatch model of ammo - infinte with reloads
        currentAmmo = (int)Data.rateOfFire.reloadingData.n.Value;

        waitingForROF = false;
    }
    #endregion
}
