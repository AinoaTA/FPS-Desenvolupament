using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public Transform m_ToSpawn;
    public bool CheckPoint;
    public Material m_On;
    public GameObject m_TeleportRenderer;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !CheckPoint )
        {
            CheckPoint = true;
            m_TeleportRenderer.GetComponent<MeshRenderer>().material = m_On;
            TeleportController.GetTeleportController().m_Activated.Add(this);//.gameObject.GetComponent<Teleport>());
        }      
    }

  
}
