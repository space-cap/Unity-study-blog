﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    float _speed = 10.0f;

    bool _moveToDest = false;
    Vector3 _destPos;

    // Start is called before the first frame update
    void Start()
    {
        Managers.Input.KeyAction -= OnKeyBoard;
        Managers.Input.KeyAction += OnKeyBoard;
        Managers.Input.MouseAction -= OnMouseClicked;
        Managers.Input.MouseAction += OnMouseClicked;
    }



    float _yAngle = 0;

    // Update is called once per frame
    void Update()
    {
        if (_moveToDest)
        {
            Vector3 dir = _destPos - transform.position;
            if (dir.magnitude < 0.01)
            {
                _moveToDest = false;
            }
            else 
            { 
                float moveDist = Mathf.Clamp( _speed * Time.deltaTime, 0, dir.magnitude );

                transform.position = transform.position + dir.normalized * moveDist;
                transform.LookAt(_destPos);
            }
        }

    }


    void OnKeyBoard()
    {
        _yAngle += Time.deltaTime * _speed;

        if (Input.GetKey(KeyCode.W))
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.forward), 0.3f);
            transform.position += transform.TransformDirection(Vector3.forward * Time.deltaTime * _speed);
        }

        if (Input.GetKey(KeyCode.S))
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.back), 0.3f);
            transform.position += transform.TransformDirection(Vector3.back * Time.deltaTime * _speed);
        }

        if (Input.GetKey(KeyCode.A))
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.left), 0.3f);
            transform.position += transform.TransformDirection(Vector3.left * Time.deltaTime * _speed);
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.right), 0.3f);
            transform.position += transform.TransformDirection(Vector3.right * Time.deltaTime * _speed);
        }

        _moveToDest = false;
    }

    void OnMouseClicked(Define.MouseEvent e)
    {
        if (e != Define.MouseEvent.Click)
        {
            return;
        }

        //Debug.Log("OnMouseClicked");

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(Camera.main.transform.position, ray.direction * 100, Color.red, 1);
        LayerMask mask = LayerMask.GetMask("Wall");

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100, mask))
        {
            _destPos = hit.point;
            _moveToDest = true;

            //Debug.Log($"Raycast Camera @ {hit.collider.gameObject.name}");
        }
    }
}
