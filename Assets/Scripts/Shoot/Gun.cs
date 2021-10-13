using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Gun", menuName = "ScriptableObject/Gun", order = 1)]

public class Gun : ScriptableObject
{
    public float maxBulletSaved = 10f;
    public float bulletForCharger = 5f;
    public float currentBullets = 5f;

}
