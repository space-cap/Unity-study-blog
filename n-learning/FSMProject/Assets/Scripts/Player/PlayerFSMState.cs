using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFSMState : MonoBehaviour {

    protected PlayerFSMManager manager;
    protected PlayerBlackboard blackboard;

    public virtual void BeginState()
    {

    }

    private void Awake()
    {
        blackboard = GetComponent<PlayerBlackboard>();
        manager = GetComponent<PlayerFSMManager>();
    }

}
