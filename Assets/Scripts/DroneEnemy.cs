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
    public float m_DistanceToAlert = 5f;
    public float m_MinDistanceToChase = 5f;
    public float m_MinDistanceToAttack = 3f;
    public float m_MaxDistanceToAttack = 5f;
    public float m_MinDistanceToPatrol = 7f;
    public float m_MaxDistanceToPatrol = 15f;
    public LayerMask m_CollisionLayerMask;
    public List<Transform> m_PatrolWayPoints;
    NavMeshAgent m_NavMeshAgent;
    private float m_Timer = 0;
    private float m_MaxTimer = 3f;
    private int m_CurrentWaypointId;
    private int m_Life = 10;
    private int m_RotationSpeed = 20;
    private float m_DistancePlayer => Vector3.Distance(transform.position, GameController.GetGameController().GetPlayer().transform.position);

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
        m_Timer = 0f;
        m_State = IState.IDLE;
    }

    void UpdateIdleState()
    {
        m_Timer += Time.deltaTime;
        if(m_Timer>m_MaxTimer)
            SetPatrolState();
        print(m_Timer);
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
        
        Vector3 rotate = transform.localRotation.eulerAngles + new Vector3(0, m_RotationSpeed * Time.deltaTime, 0);
        transform.localRotation = Quaternion.Euler(rotate);

        if (SeesPlayer())// && m_DistancePlayer <= m_MinDistanceToChase)//&& m_DistancePlayer < m_MaxDistanceToAttack)
            SetChaseState();
        else
            SetPatrolState();
    }

    void SetPatrolState()
    {
        m_State = IState.PATROL;
        MoveToNextPatrolPosition();
    }

    void UpdatePatrolState()
    {
        if (!m_NavMeshAgent.hasPath && m_NavMeshAgent.pathStatus == NavMeshPathStatus.PathComplete)
            MoveToNextPatrolPosition();

        if (m_DistanceToAlert <= m_DistancePlayer)
            SetAlertState();
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

    void MoveToNextPatrolPosition()
    {
        m_NavMeshAgent.destination = m_PatrolWayPoints[m_CurrentWaypointId].position;
        m_NavMeshAgent.isStopped = false;
        ++m_CurrentWaypointId;
        if (m_CurrentWaypointId >= m_PatrolWayPoints.Count)
            m_CurrentWaypointId =0;
    }

    bool SeesPlayer()
    {


        Vector3 l_Player = GameController.GetGameController().GetPlayer().transform.position;
        Vector3 l_EyesDronePos = transform.position + Vector3.up * 1.6f;
        Vector3 l_Direction = l_Player - l_EyesDronePos;
        float l_DistanceToPlayer = l_Direction.magnitude;
        l_Direction.Normalize();
        Ray l_Ray = new Ray(l_EyesDronePos, l_Direction);
        if(l_DistanceToPlayer < m_MaxDistanceToPatrol && Vector3.Dot(transform.forward,l_Direction) >= Mathf.Cos(m_ConeAngle*0.5f*Mathf.Deg2Rad))
            if (Physics.Raycast(l_Ray, l_DistanceToPlayer, m_CollisionLayerMask.value))
                return true;

        return false;





    }

    public void Hit(int amount)
    {
        m_Life -= amount;
        if (m_Life > 0)
            SetHitState();
        else
            SetDieState();
    }
}
