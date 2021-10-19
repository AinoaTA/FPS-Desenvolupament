using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaShootGallery : MonoBehaviour
{
    public GameObject UIpointer;
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        UIpointer.gameObject.SetActive(true);
    }

    private void OnTriggerExit(Collider other)
    {
        UIpointer.gameObject.SetActive(false);
    }
}
