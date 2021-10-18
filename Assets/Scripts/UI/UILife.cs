using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UILife : MonoBehaviour
{
    public TMP_Text Life;
    public TMP_Text Shield;
    public HealthSystemPlayer m_HealthSystemPlayer;
    private void OnEnable()
    {
        m_HealthSystemPlayer.delegateUIHealth += UpdateLifeText;
        m_HealthSystemPlayer.delegateUIShield += UpdateShieldText;
    }

    private void OnDisable()
    {
        m_HealthSystemPlayer.delegateUIHealth -= UpdateLifeText;
        m_HealthSystemPlayer.delegateUIShield -= UpdateShieldText;
    }

    private void UpdateLifeText(float value)
    {
        Life.text = value.ToString();
    }
    private void UpdateShieldText(float value)
    {
        Shield.text = value.ToString();
    }
}
