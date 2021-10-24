using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
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
            m_LifeBarEnemy.anchoredPosition = new Vector2(l_ViewporPosition.x * Screen.width, -(1.0f - l_ViewporPosition.y) * Screen.height);

            //m_LifeBarEnemy.sizeDelta = new Vector3(.5f, .5f, .5f);
        }
        else
            m_LifeBarEnemy.gameObject.SetActive(false);
    }

    public void UnSetLifeBarEnemy()
    {
        m_LifeBarEnemy.gameObject.SetActive(false);
    }
}
