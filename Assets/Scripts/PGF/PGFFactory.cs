using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class PGFFactory {

    public static PGF CreatePGF() {
        var gameObject = new GameObject();
        var pgf = gameObject.AddComponent<PGF>();

        //initialize by calling the generation
        pgf.DamageFunction = CreateDamage();
        pgf.RateOfFireFunction = CreateRateOfFire();
        pgf.ProjectileTrajectory = CreateProjectileTrajectory();


        return pgf;
    }


    private static Func<Vector3, Vector3, Func<float, Vector3>> CreateProjectileTrajectory() {
        //jesus
        return (direction, startPos) => { return (time) => {
           //Debug.Log("CreateProjectileTrajectory is being called");
                return Vector3.one * 0.6f; 
            }; 
        };
    }

    private static Func<float> CreateDamage(){
        //lambda expression
        Func<float> function = () => {

            return 1.0f;
        };

        return function;
    }

    private static Func<int, float> CreateRateOfFire(){
        Func<int, float> function = (ammoRemaining) => { return 1.0f; };

        return function;
    }
}
