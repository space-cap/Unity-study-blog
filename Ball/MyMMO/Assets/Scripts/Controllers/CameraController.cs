using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    Define.CamerMode _mode = Define.CamerMode.QuarterView;

    [SerializeField]
    Vector3 _delta = new Vector3(0,6,-5);

    [SerializeField]
    GameObject _player = null;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (_mode == Define.CamerMode.QuarterView)
        {
            transform.position = _player.transform.position + _delta;
            transform.LookAt(_player.transform);
        }
    }


    public void SetQuaterView(Vector3 delta)
    {
        _mode = Define.CamerMode.QuarterView;
        _delta = delta;
    }
}
