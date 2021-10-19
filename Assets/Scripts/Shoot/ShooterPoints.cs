using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterPoints : MonoBehaviour
{
    public int m_CurrentPoints =0;
    private int m_MaxPoints = 10;
    static ShooterPoints shooterPoints;

    private float timerControl;

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
        if (timerControl >= 1)
            m_CurrentPoints += value;
        
        
        delegateUIPoints?.Invoke(m_CurrentPoints, m_MaxPoints);
    }
    private void Update()
    {
        if (m_CurrentPoints == m_MaxPoints)
            delegateGate?.Invoke(); //open the gate
        if (Input.GetKeyDown(KeyCode.T))
            m_CurrentPoints += 2;

        timerControl += Time.deltaTime;
    }
}



