using UnityEngine;

public class PlayerState : MonoBehaviour
{
    public Animation Anim;
    public AnimationClip Idle, Shooting, Charging;

    private bool timerBool;
    private float timer;

    public delegate void ShootDelegate();
    public event ShootDelegate shootDelegate;

    public delegate void ChargingDelegate();
    public event ChargingDelegate chargingDelegate;
    private void Update()
    {
        print(PlayerStateMode);
        //if (timerBool)
        //      BackToIdle();
    }
    public enum PlayerMode
    {
        Idle,
        Shooting,
        Charging
    }

    public static PlayerMode PlayerStateMode;
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
                timerBool = true;
                break;

            case PlayerMode.Charging:
                Anim.CrossFade("Charging", 0f);
                chargingDelegate?.Invoke();
                break;
        }
    }

    //private void BackToIdle()
    //{
    //    timer += Time.deltaTime;
    //    if (timer > 2f)
    //    {
    //        PlayerStateMode = PlayerMode.Idle;
    //        timerBool = false;
    //        timer = 0;
    //    }
    //}
}
