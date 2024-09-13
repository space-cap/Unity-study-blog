using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBlackboard : MonoBehaviour
{
    public Transform target;
    public bool hasNewDestination;
    public bool hasNewTarget;
    public bool isDead;

    [HideInInspector]
    private CharacterStat stat;

    private Transform marker;

    private void Awake()
    {
        stat = GetComponent<CharacterStat>();

        marker = GameObject.FindGameObjectWithTag("Marker").transform;
        isDead = false;
    }

    public float DistanceToMarkerXZ
    {
        get
        {
            Vector3 diff = marker.position - transform.position;
            diff.y = 0.0f;
            return diff.magnitude;
        }
    }

    public float DistanceToTargetXZ
    {
        get
        {
            Vector3 diff = target.position - transform.position;
            diff.y = 0.0f;
            return diff.magnitude;
        }
    }

    public bool MoveTrigger
    {
        get
        {
            if (target != null)
                return false;

            if(hasNewDestination)
            {
                hasNewDestination = false;
                return true;
            }

            return false;
        }

        set
        {
            hasNewDestination = value;
            hasNewTarget = false;
        }
    }

    public bool AttackTrigger
    {
        get
        {
            if (target == null)
                return false;

            if (hasNewTarget)
            {
                hasNewTarget = false;
                return true;
            }

            return false;
        }

        set
        {
            hasNewTarget = value;
            hasNewDestination = false;
        }
    }

    public float AttackRange
    {
        get
        {
            return stat.attackRange;
        }
    }
}
