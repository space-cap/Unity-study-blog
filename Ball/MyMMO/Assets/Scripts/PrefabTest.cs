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
        tank = Managers.Resource.Instantiate("Tank");
        Object.Destroy(tank, 3);
    }

}
