﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCollision : MonoBehaviour
{

    private void OnCollisionEnter(Collider other)
    {
        Debug.Log("collision!");
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("trigger!");
    }




    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
