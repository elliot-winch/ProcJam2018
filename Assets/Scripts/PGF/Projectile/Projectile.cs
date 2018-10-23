using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Projectile : MonoBehaviour {

    //Calculated properties
    private float timeSinceCreation = 0.0f;
    private float distanceTravelled = 0.0f;
    private Vector3? previousPosition = null; //used for previousPosition


    //Random properties - set once at Awake
    //TODO: make random later with UnityEngine.Random(-1.0f, 1.0f) on awake
    private float trajectoryScalarX = 1.0f;
    private float trajectoryScalarY = 1.0f;

    //Data Properties
    public PGFDamageData DamageData { get; set; }
    public PGFProjectileTrajectoryData TrajectoryData { get; set; }

    #region Motion
    private void FixedUpdate()
    {
        //Record travel time
        timeSinceCreation += Time.fixedDeltaTime;

        //Record distance travelled
        if (previousPosition.HasValue)
        {
            distanceTravelled += Vector3.Distance(previousPosition.Value, transform.position);
        }
        previousPosition = transform.position;

        //Follow trajectory defined in data
        transform.position += CalculateProjectileTrajectory();
    }

    private Vector3 CalculateProjectileTrajectory()
    {
        //Move forwards
        Vector3 step = transform.forward;

        //If we have past our d...
        if (distanceTravelled >= TrajectoryData.distanceBeforeSpread)
        {
            //...spread by some angle [0, theta_max)
            step += transform.right * Mathf.Tan(trajectoryScalarX * TrajectoryData.spreadAngle);
            step += transform.up * Mathf.Tan(trajectoryScalarY * TrajectoryData.spreadAngle);
        }

        //Multiply the direction by the speed and time difference (velocity = direction[vec] * speed[scalar] * time[scalar])
        return step.normalized * TrajectoryData.speed * Time.fixedDeltaTime;
    }
    #endregion

    #region On Impact
    /// <summary>
    /// When a projectile impacts some surface
    /// </summary>
    private void OnImpact(){
        //applies DamageFunction float to the object that takes the damage
        float damage = CalculateDamageOnImpact();
    }

    /// <summary>
    /// Uses PGFImpaceDamageData to calculate impace damage. This is non-explosive damage
    /// e.g. piercing, blunt force
    /// </summary>
    /// <returns></returns>
    private float CalculateDamageOnImpact() {

        return DamageData.baseDamage + DamageData.damageDropOff * distanceTravelled;
    }
    #endregion

   
}
