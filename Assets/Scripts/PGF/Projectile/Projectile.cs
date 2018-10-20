using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Projectile : MonoBehaviour {

    private float TimeSinceCreation = 0.0f;

    public Func<float> DamageFunction { get; set; }
    public Func<float, Vector3> ResolvePosition { get; set; }

    private void OnImpact(){
        //applies DamageFunction float to the object that takes the damage
        var damage = DamageFunction();
    }

    private void FixedUpdate(){
        TimeSinceCreation += Time.fixedDeltaTime;
        transform.position = ResolvePosition(TimeSinceCreation);
        Debug.Log(DamageFunction());
    }
}
