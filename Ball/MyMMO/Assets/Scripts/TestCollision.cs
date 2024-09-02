using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCollision : MonoBehaviour
{

    void OnCollisionEnter(Collision collision)
    {
        // 충돌 시 실행할 코드
        Debug.Log("Collision detected with: " + collision.gameObject.name);
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
