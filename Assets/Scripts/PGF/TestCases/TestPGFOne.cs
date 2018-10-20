using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPGFOne : MonoBehaviour {
    PGF PGFInstance;
	// Use this for initialization
    void Start () {
        PGFInstance = PGFFactory.CreatePGF();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Space)){
            PGFInstance.Fire(transform.forward, transform.position, 100);
        }
	}
}
