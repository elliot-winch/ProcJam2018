using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class TestPGFOne : MonoBehaviour {

    public static TestPGFOne Instance { get; private set; }

    public Action<PGF> onSetCurrentPGF;

    public PGF Current
    {
        get
        {
            return currentPGF;
        }
        private set
        {
            currentPGF = value;
            
            if(onSetCurrentPGF != null)
            {
                onSetCurrentPGF.Invoke(value);
            }
        }
    }

    public TextMeshProUGUI gunName;
    public TextMeshProUGUI ammo;
    public TextMeshProUGUI description;

    private PGF currentPGF;


    private void Awake()
    {
        Instance = this;
    }

    void Start ()
    {
        GenerateNewPGF();
	}


    // Update is called once per frame
	void Update () {

        //Generate new
        if (Input.GetKeyDown(KeyCode.P))
        {
            GenerateNewPGF();
        };

        //If we have no pgf, return from here
        if(currentPGF == null)
        {
            return;
        }

        //Fire
        if (Input.GetMouseButtonDown(0)){
            Debug.Log("FIRE IN THE HOLE");
            currentPGF.Fire(transform.forward, transform.position);
        }

        //Reload
        if (Input.GetKeyDown(KeyCode.R))
        {
            currentPGF.Reload();
        }
	}

    void GenerateNewPGF()
    {
        if(currentPGF != null)
        {
            Destroy(currentPGF);
        }
        foreach (GameObject x in GameObject.FindGameObjectsWithTag("Projectile"))
        {
            Destroy(x);
        };

        Current = PGFFactory.Instance.CreatePGF();

        UIText.Write(currentPGF, description);

        gunName.text = Current.Data.meta.name;
    }
}
