using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Projectile : MonoBehaviour {

    private float TimeSinceCreation = 0.0f;


    //make random later with UnityEngine.Random(-1.0f, 1.0f) on awake
    private float trajectoryScalarX = 1.0f;
    private float trajectoryScalarY = 1.0f;

    private float distanceTravelled = 0.0f;
    private Vector3? previousPosition = null;

    public PGFDamageData DamageData { get; set; }
    public PGFProjectileTrajectoryData TrajectoryData { get; set; }

    private void OnImpact(){
        //applies DamageFunction float to the object that takes the damage
        float damage = CalculateDamageOnImpact();
    }

    private float CalculateDamageOnImpact() {

        return DamageData.baseDamage + DamageData.damageDropOff * distanceTravelled;
    }

    private void FixedUpdate(){
        TimeSinceCreation += Time.fixedDeltaTime;

        if (previousPosition.HasValue) {
            distanceTravelled += Vector3.Distance(previousPosition.Value, transform.position);
        }

        previousPosition = transform.position;

        CalculateProjectileTrajectory();
        //Debug.Log(CalculateDamageOnImpact());
    }

    private void CalculateProjectileTrajectory()
    {
        var distanceThisFrame = Time.fixedDeltaTime * TrajectoryData.speed;
        transform.position += distanceThisFrame * transform.forward;
        if (distanceTravelled >= TrajectoryData.distanceBeforeSpread)
        {
            transform.position += transform.right * Mathf.Tan(trajectoryScalarX * TrajectoryData.spreadAngle);
            transform.position += transform.up * Mathf.Tan(trajectoryScalarY * TrajectoryData.spreadAngle);
        }
    }

}
