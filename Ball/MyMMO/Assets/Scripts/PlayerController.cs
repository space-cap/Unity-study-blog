using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    float _speed = 10.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        // Local -> World
        // transform.TransformDirection

        // World -> Local
        // transform.InverseTransformDirection

        if (Input.GetKey(KeyCode.W))
        {
            //transform.position += transform.TransformDirection(Vector3.forward * Time.deltaTime * _speed);
            transform.Translate(Vector3.forward * Time.deltaTime * _speed);
        }

        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(Vector3.back * Time.deltaTime * _speed);
        }

        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector3.left * Time.deltaTime * _speed);
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.right * Time.deltaTime * _speed);
        }

    }
}
