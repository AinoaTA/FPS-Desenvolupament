using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementStatues : MonoBehaviour
{
    private Vector3 moveToY = new Vector3(0, -100f, 0);
    private Vector3 DefaultPlace;


    private void Start()
    {
        DefaultPlace = transform.position;
    }
    private void Update()
    {
        
    }



}
