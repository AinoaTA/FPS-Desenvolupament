using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    public Animation Anim;
    public AnimationClip Idle, Shooting, Charging;

    public delegate void ShootDelegate();
    public event ShootDelegate shootDelegate;

    public delegate void ChargingDelegate();
    public event ChargingDelegate chargingDelegate;
    public enum PlayerMode
    {
        Idle,
        Shooting,
        Charging
    }

    public static PlayerMode PlayerStateMode;

    private void Update()
    {
        print(PlayerStateMode);
    }

    private void Start()
    {
        UpdateShoot(PlayerMode.Idle);
    }
    public void UpdateShoot(PlayerMode nextMode)
    {
        PlayerStateMode = nextMode;

        switch (PlayerStateMode)
        {
            case PlayerMode.Idle:
                Anim.CrossFade("Idle", 0f);
                break;

            case PlayerMode.Shooting:
                Anim.CrossFade("Shooting", 0f);
                shootDelegate?.Invoke();
                break;
            case PlayerMode.Charging:
                Anim.CrossFade("Charging", 0f);
                chargingDelegate?.Invoke();
                break;
        }
    }
}
