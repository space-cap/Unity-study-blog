using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeFSMState : MonoBehaviour
{
    protected SlimeFSMManager manager;
    protected SlimeBlackboard blackboard;

    public virtual void BeginState()
    {

    }

    private void Awake()
    {
        manager = GetComponent<SlimeFSMManager>();
        blackboard = GetComponent<SlimeBlackboard>(); 
    }

}
