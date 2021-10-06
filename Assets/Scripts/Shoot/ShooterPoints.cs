using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterPoints : MonoBehaviour
{
    private int points =0;

    public delegate void DelegateGate();
    public static DelegateGate delegateGate;

    private void Update()
    {
        if (points == 10)
            delegateGate?.Invoke();
        if (Input.GetKeyDown(KeyCode.T))
            points += 2;
    }
}



