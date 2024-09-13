using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState
{
    IDLE = 0,
    RUN,
    CHASE,
    ATTACK,
    DEAD
}

public class PlayerFSMManager : MonoBehaviour, IStatNotify
{
    public PlayerState currentState;
    public PlayerState startState;

    [HideInInspector]
    private Animator anim;
    [HideInInspector]
    private PlayerBlackboard blackboard;
    [HideInInspector]
    public CharacterStat stat;
    [HideInInspector]
    public CharacterController cc;

    private int layerMask;
    public Transform marker;
    public Transform attackMarker;

    Dictionary<PlayerState, PlayerFSMState> states = new Dictionary<PlayerState, PlayerFSMState>();

    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        blackboard = GetComponent<PlayerBlackboard>();
        stat = GetComponent<CharacterStat>();
        cc = GetComponent<CharacterController>();

        layerMask = (1 << 9) + (1 << 10);
        marker = GameObject.FindGameObjectWithTag("Marker").transform;
        attackMarker = GameObject.FindGameObjectWithTag("AttackMarker").transform;

        states.Add(PlayerState.IDLE,GetComponent<PlayerIDLE>());
        states.Add(PlayerState.RUN, GetComponent<PlayerRUN>());
        states.Add(PlayerState.CHASE, GetComponent<PlayerCHASE>());
        states.Add(PlayerState.ATTACK, GetComponent<PlayerATTACK>());
        states.Add(PlayerState.DEAD, GetComponent<PlayerDEAD>());
    }

    public void SetState(PlayerState newState)
    {
        if (currentState == PlayerState.DEAD)
            return;

        foreach(PlayerFSMState fsm in states.Values)
        {
            fsm.enabled = false;
        }

        states[newState].enabled = true;
        states[newState].BeginState();
        currentState = newState;
        anim.SetInteger("CurrentState", (int)currentState);
    }

    // Use this for initialization
    void Start ()
    {
        SetState(startState);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray r = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(r, out hit, 1000, layerMask))
            {
                if (hit.transform.gameObject.layer == 9)
                {
                    marker.position = hit.point;
                    blackboard.target = null;
                    blackboard.MoveTrigger = true;
                }
                else if (hit.transform.gameObject.layer == 10)
                {
                    blackboard.target = hit.transform;
                    blackboard.AttackTrigger = true;
                    attackMarker.parent = hit.transform;
                    attackMarker.localPosition = Vector3.zero;
                }
            }
        }
    }

    public void AttackCheck()
    {
        if (currentState != PlayerState.ATTACK)
            return;

        CharacterStat targetStat = blackboard.target.GetComponent<CharacterStat>();
        targetStat.ApplyDamage(stat);
    }

    public void NotifyImDead()
    {
        blackboard.isDead = true;
    }

    public void NotifyTargetDead()
    {
        blackboard.target = null;
    }
}
