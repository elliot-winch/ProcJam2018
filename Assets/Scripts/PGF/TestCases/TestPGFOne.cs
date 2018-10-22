using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPGFOne : MonoBehaviour {


    PGF PGFInstance;
	// Use this for initialization
    void Start () {
        PGFInstance = PGFFactory.Instance.CreatePGF();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.P)){
            Destroy(GameObject.FindGameObjectWithTag("Generated"));
            foreach (GameObject x in GameObject.FindGameObjectsWithTag("Projectile")){
                Destroy(x);
            };
            PGFInstance = PGFFactory.Instance.CreatePGF();
        };
        if (Input.GetMouseButtonDown(0)){
            PGFInstance.Fire(transform.forward, transform.position, 99);
        }
	}
}
