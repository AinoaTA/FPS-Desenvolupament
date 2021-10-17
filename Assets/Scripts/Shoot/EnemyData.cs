using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "ScriptableObject/EnemyData", order = 1)]
public class EnemyData : ScriptableObject
{
    public int Life, Damage;
}
