using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystemPlayer : MonoBehaviour
{
    private int maxLife = 100;
    static int currentLife = 100;

    public delegate void DelegateUiLife(int value);
    public static DelegateUiLife delegateUi;

    static public int GetCurrentLife()
    {
        return currentLife;
    }

    public void AddLife(int value)
    {
        int total = value + currentLife;
        if (total>maxLife)
        {
            currentLife += maxLife - currentLife;
        }else
        currentLife += value;
    }

    static public void RemoveLife(int value)
    {
       
        if (currentLife<=0)
        {
            //die moment
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            AddLife(25);
            print("AA");
            delegateUi?.Invoke(currentLife);
        }
            
        if (Input.GetKeyDown(KeyCode.X))
        {
            currentLife -= 15;
            print("EE");
            delegateUi?.Invoke(currentLife);
        }
        print("ui health");

        
    }
}
