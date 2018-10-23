using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

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
        pgf.MetaData = CreateMetaData();

        return pgf;
    }


    private PGFProjectileTrajectoryData CreateProjectileTrajectoryData() {
        return new PGFProjectileTrajectoryData(){
            speed = UnityEngine.Random.Range(1.0f, 100.0f), distanceBeforeSpread = UnityEngine.Random.Range(0.0f, 10.0f), spreadAngle = 0.0f
        };
    }

    private PGFMetaData CreateMetaData() {
        return new PGFMetaData()
        {
            name = "NAME PLACEHOLDER", type = "shootyboy"

        };
    }

    private PGFDamageData CreateDamage(){

        return new PGFDamageData(){
            baseDamage = UnityEngine.Random.Range(0.0f, 100.0f),
            damageDropOff = UnityEngine.Random.Range(-15.0f, 15.0f)

        };
    }

    private PGFRateOfFireData CreateRateOfFire(){

        return new PGFRateOfFireData()
        {
            baseRate = UnityEngine.Random.Range(1.0f, 2.0f),
            ROFDataArr = new PGFBurstData[] {
                new PGFBurstData (){
                    n = 3,
                    r = 1,
                }
            }
        };
    }
}
