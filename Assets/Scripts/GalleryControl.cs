using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GalleryControl : MonoBehaviour
{
    public GameObject[] m_Statues;

    private float timer = 25;
    public bool startGallery = false;


    public delegate void DelegateUITimer(float timer);
    public static DelegateUITimer delegateUITimer;

    private void OnEnable()
    {
        GalleryAnimation.startGallery += StartGallery;
    }

    private void OnDisable()
    {
        GalleryAnimation.startGallery -= StartGallery;
    }


    private void StartGallery()
    {
        startGallery = true;
    }

   
    private void ControlStatues()
    {
        
    }
    private void Update()
    {
        if (startGallery)
        {
            timer -= Time.deltaTime;
            delegateUITimer?.Invoke(timer);
            if (timer>=0 && ShooterPoints.GetShooterPoints().m_CurrentPoints >= ShooterPoints.GetShooterPoints().GetMaxPoints())
            {
                ShooterPoints.GetShooterPoints().SetCanOpenGate(true);
                timer = 25;
                startGallery = false;
            }
            else if(timer<=0 && ShooterPoints.GetShooterPoints().m_CurrentPoints < ShooterPoints.GetShooterPoints().GetMaxPoints())
            {
                ShooterPoints.GetShooterPoints().SetCanOpenGate(false);
                ShooterPoints.GetShooterPoints().ResetPoints();
                timer = 25;
                startGallery = false;
            }
        }
        else
        {
            ControlStatues();
        }
    }
}
