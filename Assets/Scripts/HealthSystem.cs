using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    [SerializeField]private float health = 100f;
    private Drop drop;
    //private float maxHealth = 100f;

    private void Awake()
    {
        drop = GetComponent<Drop>();
    }
    public void GetDamage(float damage)
    {
        health -= damage;
        print(health);
        if(health <= 0)
        {
            drop.DropItem();
            this.gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
            GetDamage(10);
    }
}
