
using UnityEngine;


[CreateAssetMenu(fileName = "Gun", menuName = "ScriptableObject/Gun", order = 1)]

public class Gun : ScriptableObject
{
    public int MaxBulletsHold;
    public int CurrentBulletHold;
    public int BulletForCharger;
    public int CurrentBullets;

    public float upDispersion;

}
