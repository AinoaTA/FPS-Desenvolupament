using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UILife : MonoBehaviour
{
    public TMP_Text Life;
    private void OnEnable()
    {
        HealthSystemPlayer.delegateUi += UpdateLifeText;
    }

    private void OnDisable()
    {
        HealthSystemPlayer.delegateUi -= UpdateLifeText;
    }


    private void UpdateLifeText(int value)
    {
        Life.text = value.ToString();
    }
}
