using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGun
{
    public void Shooting();

    public void Charging();

    public void Idle();

    public float CurrentBullets { get; }
}
