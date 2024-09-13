using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeCHASE : SlimeFSMState
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

        if(blackboard.DistanceToTargetXZ <= blackboard.AttackRange)
        {
            manager.SetState(SlimeState.ATTACK);
            return;
        }

        // Action
        GameLib.MoveToTransform(manager.cc, blackboard.target, manager.stat);
        blackboard.isTargetInSight = GameLib.DetectCharacter(manager.sight, manager.playerCC);
    }
}
