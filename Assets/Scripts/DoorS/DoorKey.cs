using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorKey : MonoBehaviour
{
    public Animator m_Door;
    public GameObject Key;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            m_Door.SetBool("Open", true);
            m_Door.SetBool("Close", false);
            Key.SetActive(false);
        }
        
    }
    public void ResetKeyDoor()
    {
        m_Door.SetBool("Open", false);
        m_Door.SetBool("Close", true);
        Key.SetActive(true);
    }

}
