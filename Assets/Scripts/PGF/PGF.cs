using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PGF : MonoBehaviour
{

    public Projectile Fire(Vector3 direction, Vector3 position, int ammoRemaining)
    {
        var projectileObject = new GameObject()
        {
            name = "projectile", //tag = "projectile"
        };
        var projectileComponent = projectileObject.AddComponent<Projectile>();
        //set projectile components
        projectileComponent.DamageFunction = DamageFunction;
        //we'll calculate the direction you're facing (first parameter) later, barrel's position
        projectileComponent.ResolvePosition = ProjectileTrajectory(Vector3.zero, transform.position);

        return projectileComponent;
    }

    // property of class, private variable public getter
    // not serializable, you need to call the getter
    public Func<float> DamageFunction { get; set; }

    public Func<int, float> RateOfFireFunction { get; set; }

                //parameters      //return value
    public Func<Vector3, Vector3, Func<float, Vector3>> ProjectileTrajectory {get; set;}
}
