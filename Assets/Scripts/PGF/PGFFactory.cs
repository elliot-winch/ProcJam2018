using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class PGFFactory : MonoBehaviour{

    public TextMeshProUGUI description;
    public static PGFFactory Instance { get; private set; }
    UIText UIText;
    private void Awake()
    {
        Instance = this;
        UIText = new UIText();
    }

    public GameObject pgfPrefab;

    public PGF CreatePGF() {
        var gameObjectInstance = Instantiate(pgfPrefab);
        PGF pgf = gameObjectInstance.GetComponent<PGF>();

        //initialize by calling the generation
        pgf.DamageData = CreateDamage();
        pgf.RateOfFireData = CreateRateOfFire();
        pgf.ProjectileTrajectoryData = CreateProjectileTrajectoryData();
        pgf.MetaData = CreateMetaData();


        UIText.Write(gameObjectInstance, description);
        return pgf;
    }


    private PGFProjectileTrajectoryData CreateProjectileTrajectoryData() {
        //jesus
        return new PGFProjectileTrajectoryData(){
            speed = 5.0f, distanceBeforeSpread = 0.0f, spreadAngle = Mathf.PI / 4.0f
        };
    }

    private PGFMetaData CreateMetaData() {
        return new PGFMetaData()
        {
            name = "TEST w RAND NUM " + UnityEngine.Random.Range(-1000.0f, 1000.0f), type = "shootyboy"
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
