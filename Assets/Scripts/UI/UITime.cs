using TMPro;
using UnityEngine;

public class UITime : MonoBehaviour
{
    private TMP_Text tmp;
    private float m_Timer;

    private GalleryControl galleryControl;

    private void Awake()
    {
        tmp = GetComponent<TMP_Text>();
        galleryControl = FindObjectOfType<GalleryControl>();
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
        if (galleryControl.startGallery)
            tmp.text = m_Timer.ToString();
        else
            tmp.text = "";
    }
}
