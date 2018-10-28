﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class TestPGFOne : MonoBehaviour {

    public static TestPGFOne Instance { get; private set; }

    public UnityEvent<PGF> onSetCurrentPGF;

    public PGF Current
    {
        get
        {
            return currentPGF;
        }
        private set
        {
            currentPGF = value;

            onSetCurrentPGF.Invoke(value);
        }
    }

    public TextMeshProUGUI description;
    private PGF currentPGF;
    public TextMeshProUGUI ammo;

    void Start ()
    {
        GenerateNewPGF();

        UIText.Write(currentPGF, description);
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

        currentPGF = PGFFactory.Instance.CreatePGF();

        UIText.Write(currentPGF, description);
    }
}
