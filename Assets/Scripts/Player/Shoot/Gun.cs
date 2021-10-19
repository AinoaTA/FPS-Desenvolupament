
using UnityEngine;


[CreateAssetMenu(fileName = "Gun", menuName = "ScriptableObject/Gun", order = 1)]

public class Gun : ScriptableObject
{
    public int maxBulletSaved = 100;
    public int bulletForCharger = 50;
    public int currentBullets = 50;

    public float upDispersion = 0.5f;

}
