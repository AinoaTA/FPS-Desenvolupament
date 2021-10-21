using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterPoints : MonoBehaviour
{
    public int m_CurrentPoints =0;
    static int m_MaxPoints = 15;
    static ShooterPoints shooterPoints;
    public bool CanOpenGate = false;

    public delegate void DelegateGate();
    public static DelegateGate delegateGate;

    public delegate void DelegateUIPoints(int current, int max);
    public static DelegateUIPoints delegateUIPoints;
    static public ShooterPoints GetShooterPoints()
    {
        return shooterPoints;
    }
    public int GetMaxPoints() => m_MaxPoints;
    private void Start()
    {
        shooterPoints = this;
        delegateUIPoints?.Invoke(m_CurrentPoints, m_MaxPoints);
    }
    public bool SetCanOpenGate(bool value)
    {
        return CanOpenGate = value;
    }

    public void ResetPoints()
    {
        m_CurrentPoints = 0;
        delegateUIPoints?.Invoke(m_CurrentPoints, m_MaxPoints);
    }

    public void AddPoints(int value)
    {
        m_CurrentPoints += value;
        delegateUIPoints?.Invoke(m_CurrentPoints, m_MaxPoints);
    }

    private void Update()
    {
        if (CanOpenGate)
            delegateGate?.Invoke();
    }
}



