using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeATTACK : SlimeFSMState
{
    public override void BeginState()
    {
        base.BeginState();
    }

    void Update ()
    {
        // Check Transition
        if (blackboard.isDead)
        {
            manager.SetState(SlimeState.DEAD);
            return;
        }

        if (blackboard.target == null)
        {
            manager.SetState(SlimeState.IDLE);
            return;
        }

        if (!blackboard.IsPlayerDetected)
        {
            manager.SetState(SlimeState.IDLE);
            return;
        }

        if (blackboard.DistanceToTargetXZ > blackboard.AttackRange)
        {
            manager.SetState(SlimeState.CHASE);
            return;
        }

        // Action
        GameLib.RotateToTarget(transform, blackboard.target.position, manager.stat);
    }
}
