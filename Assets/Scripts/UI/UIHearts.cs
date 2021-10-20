using TMPro;
using UnityEngine;

public class UIHearts : MonoBehaviour
{
    private TMP_Text Hearts;

    private void Awake()
    {
        Hearts = GetComponent<TMP_Text>();
    }
    private void OnEnable()
    {
        HealthSystemPlayer.delegatUIHearts += UpdateHeartsText;
    }

    private void OnDisable()
    {
        HealthSystemPlayer.delegatUIHearts -= UpdateHeartsText;
    }

    private void UpdateHeartsText(int value)
    {
        if(value==1)
           Hearts.text = value.ToString() + " Heart";
        else
            Hearts.text = value.ToString() + " Hearts";
    }
}
