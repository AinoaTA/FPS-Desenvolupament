using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystemPlayer : MonoBehaviour
{
    public float maxLife = 100;
    public float m_MaxShieldLifeTime = 100;
    public float m_ShieldLifeTime = 100;
    public float currentLife = 100f;

    public delegate void DelegateUiLife(float value);
    public event DelegateUiLife delegateUIHealth;
    public delegate void DelegateUIShield(float value);
    public event DelegateUIShield delegateUIShield;

    public void AddLife(float value)
    {
        float total = value + currentLife;
        if (total>maxLife)
        {
            currentLife += maxLife - currentLife;
        }else
        currentLife += value;

        delegateUIHealth?.Invoke(currentLife);
    }

    public void AddShieldLife(float value)
    {
        float total = value + m_ShieldLifeTime;

        if (total > m_MaxShieldLifeTime)
        {
            m_ShieldLifeTime += m_MaxShieldLifeTime - m_ShieldLifeTime;
        }
        else
            m_ShieldLifeTime += value;


        delegateUIShield.Invoke(m_ShieldLifeTime);
       // delegateUi?.Invoke(m_ShieldLifeTime);
    }

    public void GetDamage(float value)
    {
        if (m_ShieldLifeTime > 0)
        {
            currentLife -= value * 0.25f;
            m_ShieldLifeTime -= value * 0.75f;
        }
        if (m_ShieldLifeTime <= 0)
        {
            m_ShieldLifeTime = 0;
            currentLife -= value;
        }

        if (currentLife <= 0)
        {
            currentLife = 0;
            //die anim
        }

        delegateUIHealth?.Invoke(currentLife);
        delegateUIShield.Invoke(m_ShieldLifeTime);
    }
}
