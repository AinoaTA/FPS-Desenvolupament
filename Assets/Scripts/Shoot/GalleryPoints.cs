using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GalleryPoints : MonoBehaviour
{
    private int point = 1;
    private ShooterPoints m_ShooterPoints;
    private Animator anim;

 
    private void Awake()
    {
        anim = GetComponent<Animator>();
        m_ShooterPoints = FindObjectOfType<ShooterPoints>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {

            ShooterPoints.GetShooterPoints().AddPoints(point);
        }
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.K))
            {
           

                ShooterPoints.GetShooterPoints().AddPoints(1);
            
        }
    }
}
