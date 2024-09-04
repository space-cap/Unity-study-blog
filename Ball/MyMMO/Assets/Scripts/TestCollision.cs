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
        // Local <-> World <-> (Viewport <-> Screen) (화면)
        //Debug.Log(Input.mousePosition);
        //Debug.Log(Camera.main.ScreenToViewportPoint(Input.mousePosition));

        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane));
            Vector3 dir = mousePos - Camera.main.transform.position;
            dir = dir.normalized;

            Debug.DrawRay(Camera.main.transform.position, dir * 100, Color.red, 1);

            RaycastHit hit;
            if(Physics.Raycast(Camera.main.transform.position, dir, out hit, 100))
            {
                Debug.Log($"Raycast Camera @ {hit.collider.gameObject.name}");
            }
        }
    }
}
