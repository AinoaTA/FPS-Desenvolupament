using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeBarEnemy : MonoBehaviour
{
    public RectTransform m_LifeBarEnemy;
    public Camera m_Camera;
    public void SetLifeBarEnemy(Vector3 WorldPosition)
    {
        Vector3 l_ViewporPosition = m_Camera.WorldToViewportPoint(WorldPosition);
        if (l_ViewporPosition.z > 0.0f)
        {
            m_LifeBarEnemy.gameObject.SetActive(true);
            m_LifeBarEnemy.anchoredPosition = new Vector2(l_ViewporPosition.x * Screen.width, -(1.0f-l_ViewporPosition.y) * Screen.height);

        }
        else
            m_LifeBarEnemy.gameObject.SetActive(false);
           
    }
}
