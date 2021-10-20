using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public Vector3 m_ToSpawn;
    public bool CheckPoint;
    public Material m_On;
    public GameObject m_Teleport;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !CheckPoint )
        {
            CheckPoint = true;
            m_Teleport.GetComponent<MeshRenderer>().material = m_On;
            TeleportController.GetTeleportController().m_Activated.Add(this);//.gameObject.GetComponent<Teleport>());
        }      
    }
}
