using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class DroneEnemy : MonoBehaviour
{   enum IState
    {
        IDLE=0,
        CHASE,
        ALERT,
        PATROL,
        ATTACK,
        HIT,
        DIE
    }

    private IState m_State;

    public float m_ConeAngle = 60f;
    public float m_MinDistanceToAttack = 3f;
    public float m_MaxDistanceToAttack = 5f;
    public float m_MinDistanceToPatrol = 7f;
    public float m_MaxDistanceToPatrol = 15f;
    public LayerMask m_CollisionLayerMask;
    public List<Transform> m_PatrolWayPoints;
    private int m_CurrentWaypointId;
    NavMeshAgent m_NavMeshAgent;

    private void Awake()
    {
        m_NavMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        SetIdleState();
    }
    private void Update()
    {
        switch (m_State)
        {
            case IState.IDLE:
                UpdateIdleState();
                break;
            case IState.CHASE:
                UpdateChaseState();
                break;
            case IState.ALERT:
                UpdateAlertState();
                break;
            case IState.PATROL:
                UpdatePatrolState();
                break;
            case IState.ATTACK:
                UpdateAttackState();
                break;
            case IState.HIT:
                UpdateHitState();
                break;
            case IState.DIE:
                UpdateDieState();
                break;
            default:
                break;
        }
    }
    void SetIdleState()
    {
        m_State = IState.IDLE;

    }

    void UpdateIdleState()
    {
    }

    void SetChaseState()
    {
        m_State = IState.CHASE;

    }

    void UpdateChaseState()
    {
    }

    void SetAlertState()
    {
        m_State = IState.ALERT;

    }

    void UpdateAlertState()
    {
    }

    void SetPatrolState()
    {
        m_State = IState.PATROL;

    }

    void UpdatePatrolState()
    {
    }

    void SetAttackState()
    {
        m_State = IState.ATTACK;

    }

    void UpdateAttackState()
    {
    }

    void SetHitState()
    {
        m_State = IState.HIT;

    }

    void UpdateHitState()
    {
    }

    void SetDieState()
    {
        m_State = IState.DIE;

    }

    void UpdateDieState()
    {
    }

    void SetNextChasePosition()
    {
        Vector3 l_Player = GameController.GetGameController().GetPlayer().transform.position;
        Vector3 l_DirectionToPlayer = l_Player - transform.position;
        l_DirectionToPlayer.y = 0;
        l_DirectionToPlayer.Normalize();
        Vector3 l_Destination = l_Player - l_DirectionToPlayer * m_MinDistanceToAttack;
        m_NavMeshAgent.destination = l_Destination;
    }

    void NoveToNextPatrolPosition()
    {
        m_NavMeshAgent.destination = m_PatrolWayPoints[m_CurrentWaypointId].position;
    }
}
