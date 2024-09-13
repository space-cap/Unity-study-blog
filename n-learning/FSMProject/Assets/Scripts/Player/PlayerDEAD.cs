using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDEAD : PlayerFSMState
{
    public override void BeginState()
    {
        manager.marker.gameObject.SetActive(false);
        manager.attackMarker.parent = null;
        manager.attackMarker.gameObject.SetActive(false);
        manager.cc.enabled = false;
    }

    void Update () {
		
        // Check Transition

        // Action

	}
}
