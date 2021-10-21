using TMPro;
using UnityEngine;

public class UITime : MonoBehaviour
{
    private TMP_Text tmp;
    private float m_Timer;

    private void Awake()
    {
        tmp = GetComponent<TMP_Text>();
    }
    private void OnEnable()
    {
        GalleryControl.delegateUITimer += GetTimerUI;
    }

    private void OnDisable()
    {
        GalleryControl.delegateUITimer -= GetTimerUI;
    }

    private void GetTimerUI(float value)
    {
        m_Timer = value;
    }

    private void Update()
    {
        if (m_Timer <= 0)
            tmp.text = "";
        else
            tmp.text = m_Timer.ToString();
    }
}
