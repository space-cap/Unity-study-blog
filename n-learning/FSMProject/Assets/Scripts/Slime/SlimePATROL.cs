using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimePATROL : SlimeFSMState
{

    public override void BeginState()
    {
        base.BeginState();
        blackboard.patrolDestination = new Vector3(Random.Range(-10, 10), 0, Random.Range(-10, 10));
    }

    void Update ()
    {
        // Check Transition
        if (blackboard.isDead)
        {
            manager.SetState(SlimeState.DEAD);
            return;
        }

        if (blackboard.IsPlayerDetected)
        {
            manager.SetState(SlimeState.CHASE);
            return;
        }

        if(blackboard.DistanceToDestinationXZ < 0.1f)
        {
            manager.SetState(SlimeState.IDLE);
            return;
        }

        // Action
        blackboard.isTargetInSight = GameLib.DetectCharacter(manager.sight, manager.playerCC);
        GameLib.MoveToPosition(manager.cc, blackboard.patrolDestination, manager.stat);
	}
}
