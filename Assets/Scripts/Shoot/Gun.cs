
using UnityEngine;


[CreateAssetMenu(fileName = "Gun", menuName = "ScriptableObject/Gun", order = 1)]

public class Gun : ScriptableObject
{
    public int maxBulletSaved = 10;
    public int bulletForCharger = 5;
    public int currentBullets = 5;

    public float upDispersion = 0.5f;

}
