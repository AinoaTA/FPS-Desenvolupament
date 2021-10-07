using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float speed;
    private Transform position;
    private Vector3 normal;
    
    public Bullet(Transform startPosition, Vector3 normal, float _speed )
    {
        speed = _speed;
        position = startPosition;
        this.normal = normal;
    }

    private void Update()
    {
        


    }



}
