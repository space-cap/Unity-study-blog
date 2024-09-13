using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeBlackboard : MonoBehaviour
{
    public Transform target;
    public bool isDead;
    public float idleTime;
    public bool isTargetInSight;
    public Vector3 patrolDestination;

    [HideInInspector]
    private CharacterStat stat;

    private void Awake()
    {
        stat = GetComponent<CharacterStat>();
        target = GameObject.FindGameObjectWithTag("Player").transform;

        isDead = false;
        isTargetInSight = false;
        idleTime = 3.0f;
    }

    public float DistanceToDestinationXZ
    {
        get
        {
            Vector3 diff = patrolDestination - transform.position;
            diff.y = 0.0f;
            return diff.magnitude;
        }
    }

    public bool IsPlayerDetected
    {
        get
        {
            if (target == null)
                return false;

            return isTargetInSight;
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

    public float AttackRange
    {
        get
        {
            return stat.attackRange;
        }
    }
}
