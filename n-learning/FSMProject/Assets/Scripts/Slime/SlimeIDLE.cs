using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeIDLE : SlimeFSMState
{
    public float elapsedTime = 0;

    public override void BeginState()
    {
        base.BeginState();
        elapsedTime = 0;
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

        if(elapsedTime >= blackboard.idleTime)
        {
            manager.SetState(SlimeState.PATROL);
            return;
        }

        // Action
        elapsedTime += Time.deltaTime;
        blackboard.isTargetInSight = GameLib.DetectCharacter(manager.sight, manager.playerCC);

    }
}
