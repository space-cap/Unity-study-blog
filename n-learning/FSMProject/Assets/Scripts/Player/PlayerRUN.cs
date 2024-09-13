using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRUN : PlayerFSMState
{
    public override void BeginState()
    {
        manager.marker.gameObject.SetActive(true);
        manager.attackMarker.parent = null;
        manager.attackMarker.gameObject.SetActive(false);
    }
	
	// Update is called once per frame
	void Update ()
    {
        // Check Transition
        if (blackboard.isDead)
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

        if(blackboard.DistanceToMarkerXZ < 0.1f)
        {
            manager.SetState(PlayerState.IDLE);
            return;
        }

        // Action
        GameLib.MoveToTransform(manager.cc, manager.marker, manager.stat);
    }
}
