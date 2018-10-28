﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TestPGFOne : MonoBehaviour {

    public TextMeshProUGUI description;

    private PGF currentPGF;

    void Start ()
    {
        GenerateNewPDF();

        UIText.Write(currentPGF, description);
	}


    // Update is called once per frame
	void Update () {

        //Generate new
        if (Input.GetKeyDown(KeyCode.P))
        {
            GenerateNewPDF();
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

    void GenerateNewPDF()
    {
        if(currentPGF != null)
        {
            Destroy(currentPGF);
        }

        currentPGF = PGFFactory.Instance.CreatePGF();

        UIText.Write(currentPGF, description);
    }
}
