using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoRestore : MonoBehaviour
{
    public GameObject AmmoPrefab;
    public Transform PoolObjects;
    private PoolElements AmmoPool;
    private float timer;


    public void Awake()
    {
        AmmoPool = new PoolElements(5,PoolObjects, AmmoPrefab);
    }
    private void Update()
    {

        if (GalleryControl.GetGalleryControl().startGallery)
        {
            timer += Time.deltaTime;

            if (timer>=5)
            {
                GameObject Ammo = AmmoPool.GetNextElement();
                Ammo.SetActive(true);
                Ammo.transform.position = transform.position;
                
                timer = 0;
            }
        }
       
        
    }
}
