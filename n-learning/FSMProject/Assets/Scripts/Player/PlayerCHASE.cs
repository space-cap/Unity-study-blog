using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCHASE : PlayerFSMState
{
    public override void BeginState()
    {
        manager.marker.gameObject.SetActive(false);
        manager.attackMarker.gameObject.SetActive(true);
    }

    void Update () {

        // Check Transition
        if (blackboard.isDead)
        {
            manager.SetState(PlayerState.DEAD);
            return;
        }

        if (blackboard.MoveTrigger)
        {
            manager.SetState(PlayerState.RUN);
            return;
        }

        if (blackboard.target == null)
        {
            manager.SetState(PlayerState.IDLE);
            return;
        }

        if (blackboard.DistanceToTargetXZ <= blackboard.AttackRange)
        {
            manager.SetState(PlayerState.ATTACK);
            return;
        }

        // Action
        GameLib.MoveToTransform(manager.cc, blackboard.target, manager.stat);
    }
}
