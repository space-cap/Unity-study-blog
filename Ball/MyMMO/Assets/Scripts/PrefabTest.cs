﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabTest : MonoBehaviour
{
    GameObject prefab;


    GameObject tank;
    // Start is called before the first frame update
    void Start()
    {
        prefab = Resources.Load<GameObject>("Prefabs/Tank");


        tank = Object.Instantiate(prefab);        

        Object.Destroy(tank, 3);
    }

}
