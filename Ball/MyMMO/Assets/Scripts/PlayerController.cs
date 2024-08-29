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



    float _yAngle = 0;

    // Update is called once per frame
    void Update()
    {

        // Local -> World
        // transform.TransformDirection

        // World -> Local
        // transform.InverseTransformDirection


        //transform.rotation
        
        
        _yAngle += Time.deltaTime * _speed;
        
        // 절대 회전값
        //transform.eulerAngles = new Vector3(0, _yAngle, 0);

        // +- delta
        //transform.Rotate(new Vector3(0, _yAngle, 0));


        //Quaternion qt = transform.rotation;

        //transform.rotation = Quaternion.Euler(new Vector3(0, _yAngle, 0));


        if (Input.GetKey(KeyCode.W))
        {
            //transform.position += transform.TransformDirection(Vector3.forward * Time.deltaTime * _speed);
            //transform.Translate(Vector3.forward * Time.deltaTime * _speed);


            //transform.rotation = Quaternion.LookRotation(Vector3.forward);

            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.forward), 0.3f);
            transform.position += transform.TransformDirection(Vector3.forward * Time.deltaTime * _speed);
        }

        if (Input.GetKey(KeyCode.S))
        {
            //transform.Translate(Vector3.back * Time.deltaTime * _speed);
            //transform.rotation = Quaternion.LookRotation(Vector3.back);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.back), 0.3f);
        }

        if (Input.GetKey(KeyCode.A))
        {
            //transform.Translate(Vector3.left * Time.deltaTime * _speed);
            //transform.rotation = Quaternion.LookRotation(Vector3.left);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.left), 0.3f);
        }

        if (Input.GetKey(KeyCode.D))
        {
            //transform.Translate(Vector3.right * Time.deltaTime * _speed);
            //transform.rotation = Quaternion.LookRotation(Vector3.right);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.right), 0.3f);
        }

    }
}
