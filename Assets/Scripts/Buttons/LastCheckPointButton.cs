using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastCheckPointButton : MonoBehaviour
{
    public delegate void DelegateButtonCheckPoint(GameObject gameObject);
    public static DelegateButtonCheckPoint delegateButtonCheckPoint;

    public HudController m_HudController;

    public void LastCheckPoint()
    {
        delegateButtonCheckPoint?.Invoke(gameObject);
    }
}
