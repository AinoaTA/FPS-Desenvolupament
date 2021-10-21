using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolElements
{

    List<GameObject> m_Elements;
    private int m_CurrentElementID;
    public PoolElements(int Count, Transform parent, GameObject Prefab)
    {
        m_Elements = new List<GameObject>();
        for (int i = 0; i < Count; i++)
        {
            m_CurrentElementID = 0;
            GameObject l_Elemet = GameObject.Instantiate(Prefab);
            l_Elemet.SetActive(false);
            m_Elements.Add(l_Elemet);
        }


    }

    public GameObject GetNextElement()
    {
        GameObject l_Element = m_Elements[m_CurrentElementID];
        ++m_CurrentElementID;
        if (m_CurrentElementID >= m_Elements.Count)
            m_CurrentElementID = 0;
        return l_Element;
    }

}
