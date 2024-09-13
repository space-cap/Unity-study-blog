using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIDLE : PlayerFSMState
{
    public override void BeginState()
    {
        manager.marker.gameObject.SetActive(false);
        manager.attackMarker.parent = null;
        manager.attackMarker.gameObject.SetActive(false);
    }

    void Update () {

        // Check Transition
        if(blackboard.isDead)
        {
            manager.SetState(PlayerState.DEAD);
            return;
        }

        if (blackboard.AttackTrigger)
        {
            manager.SetState(PlayerState.CHASE);
            return;
        }

        if (blackboard.MoveTrigger)
        {
            manager.SetState(PlayerState.RUN);
            return;
        }

        // Action

    }
}
