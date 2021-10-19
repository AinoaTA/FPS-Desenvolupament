using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterPoints : MonoBehaviour
{
    public int m_CurrentPoints =0;
    private int m_MaxPoints = 25;
    static ShooterPoints shooterPoints;

    public delegate void DelegateGate();
    public static DelegateGate delegateGate;

    public delegate void DelegateUIPoints(int current, int max);
    public static DelegateUIPoints delegateUIPoints;
    static public ShooterPoints GetShooterPoints()
    {
        return shooterPoints;
    }
    private void Start()
    {
        shooterPoints = this;
        delegateUIPoints?.Invoke(m_CurrentPoints, m_MaxPoints);
    }

    public void AddPoints(int value)
    {
        m_CurrentPoints += value;
        delegateUIPoints?.Invoke(m_CurrentPoints, m_MaxPoints);

        if (m_CurrentPoints >= m_MaxPoints)
            delegateGate?.Invoke();
    }
}



