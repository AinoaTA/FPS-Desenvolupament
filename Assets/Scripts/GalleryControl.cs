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
    private void Update()
    {
        if (startGallery)
        {
            timer -= Time.deltaTime;

            delegateUITimer?.Invoke(Mathf.Round(timer)); //sets ui text
            if (timer>=0 && ShooterPoints.GetShooterPoints().m_CurrentPoints >= ShooterPoints.GetShooterPoints().GetMaxPoints()) //reaches max points in time
            {
                ShooterPoints.GetShooterPoints().SetCanOpenGate(true);
                StartCoroutine(RestartGallery());

            }
            else if(timer<=0 && ShooterPoints.GetShooterPoints().m_CurrentPoints < ShooterPoints.GetShooterPoints().GetMaxPoints())//no reaches max points in time
            {
                ShooterPoints.GetShooterPoints().SetCanOpenGate(false);
                ShooterPoints.GetShooterPoints().ResetPoints();

                StartCoroutine(RestartGallery());
            }
        }


        
    }

    private IEnumerator RestartGallery()
    {
        yield return new WaitForSeconds(2f);
        timer = 25;
        startGallery = false;
    }
}
