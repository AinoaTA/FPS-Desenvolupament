using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindGCButton : MonoBehaviour
{
    public delegate void DelegateButton();
    public static DelegateButton delegateButton;

    public void PressButton()
    {
        delegateButton?.Invoke();
    }
}
