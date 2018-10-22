using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PGFFactory : MonoBehaviour{

    //Singleton
    public static PGFFactory Instance { get; private set; }

    //Assign singleton
    private void Awake()
    {
        Instance = this;
    }

    [Tooltip("The Prefab for any PGF")]
    public GameObject pgfPrefab;

    /// <summary>
    /// Creates a new PGF
    /// </summary>
    /// <returns></returns>
    public PGF CreatePGF() {
        //Create the PGF
        var gameObjectInstance = Instantiate(pgfPrefab);
        PGF pgf = gameObjectInstance.GetComponent<PGF>();

        //initialize by calling the generation
        pgf.DamageData = CreateDamage();
        pgf.RateOfFireData = CreateRateOfFire();
        pgf.ProjectileTrajectoryData = CreateProjectileTrajectoryData();

        return pgf;
    }


    private PGFProjectileTrajectoryData CreateProjectileTrajectoryData() {
        //jesus
        return new PGFProjectileTrajectoryData(){

            //TODO: roll the dice
            speed = 1.0f,
            distanceBeforeSpread = 5.0f,
            spreadAngle = Mathf.PI / 4.0f
        };
    }

    private PGFDamageData CreateDamage(){

        return new PGFDamageData(){

            baseDamage = UnityEngine.Random.Range(-1.0f, 1.0f),
            damageDropOff = UnityEngine.Random.Range(-1.0f, 1.0f)
        };
    }

    private PGFRateOfFireData CreateRateOfFire(){

        return new PGFRateOfFireData()
        {
            baseRate = UnityEngine.Random.Range(0.0f, 1.0f),

            //TODO: roll the dice
            ROFDataArr = new PGFBurstData[] {
                new PGFBurstData (){
                    n = 3,
                    r = 2.0f,
                }
            }
        };
    }
}
