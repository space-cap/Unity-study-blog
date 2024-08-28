using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : MonoBehaviour
{
    static Managers Instance;
    static Managers GetInstance() {  return Instance; }



    // Start is called before the first frame update
    void Start()
    {
        //Instance = this; 
        GameObject go = GameObject.Find("@Managers");
        Instance = go.GetComponent<Managers>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
