using System.Collections;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    private int ResetCurrent;
    private int ResetCurrentHold;

    public Gun CurrentGun;
    public Gun CurrentValues;
    public FPS Player;
    public ParticleSystem Smoke;
    public PoolElements AmmoPool;
    public Transform PoolObjects;
    public int m_MaxDecans = 10;

    public int GetMaxBulletsHold()
    {
        return CurrentValues.MaxBulletsHold;
    }
    public int GetCurrentBullets()
    {
        return CurrentValues.CurrentBullets;
    }

    public int GetCurrentBulletHold()
    {
        return CurrentValues.CurrentBulletHold;
    }




    public Camera PCamera;
    public GameObject BulletPrefab;
    public LayerMask m_ShootLayerMask;
    //to call event delegate
    private PlayerState playerState;

    public delegate void DelegateUI(int current, int max, int forCharger);
    public static DelegateUI delegateUI;
    private void Awake()
    {
        AmmoPool = new PoolElements(m_MaxDecans, PoolObjects, BulletPrefab);
        playerState = GetComponent<PlayerState>();
    }

    private void OnEnable()
    {
        playerState.shootDelegate += Shooting;
        playerState.chargingDelegate += Charging;
    }

    private void OnDisable()
    {
        playerState.shootDelegate -= Shooting;
        playerState.chargingDelegate -= Charging;
    }

    private void Start()
    {
        CurrentValues.BulletForCharger = CurrentGun.BulletForCharger;
        CurrentValues.CurrentBullets = CurrentGun.CurrentBullets;
        CurrentValues.CurrentBulletHold = CurrentGun.CurrentBulletHold;

        ResetCurrent = CurrentValues.CurrentBullets;
        ResetCurrentHold = CurrentValues.CurrentBulletHold;

        UpdateTextUI(CurrentValues.CurrentBullets, CurrentValues.CurrentBulletHold, CurrentValues.BulletForCharger);

    }
    //**********************************************************
    //**********************************************************
    //**********************************************************
    //SHOOTING
    //**********************************************************
    //**********************************************************
    //**********************************************************
    public void Shooting()
    {
        Ray l_ray = PCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit l_RaycastHit;
        if (Physics.Raycast(l_ray, out l_RaycastHit, 200.0f, m_ShootLayerMask))
        {
            Smoke.gameObject.SetActive(true);
            Smoke.Play();

            if (l_RaycastHit.collider.CompareTag("Enemy"))
                l_RaycastHit.collider.GetComponent<HitCollider>().Hit();
            else if (l_RaycastHit.collider.CompareTag("Gallery"))
            {
                GalleryAnimation temp = l_RaycastHit.collider.GetComponentInParent<GalleryAnimation>();
                temp.SetShot();
                
                ShooterPoints.GetShooterPoints().AddPoints(temp.m_Points);
            }
            else
                CreateShootHitParticles(l_RaycastHit.point, l_RaycastHit.normal, l_RaycastHit.transform);

            MusicControllerFPS.GetMusicController().PlayerShoot();
            

            UpdateBullets();

            RecoilEffect();
            StartCoroutine(ShootingDelay());
        }
        else
            playerState.UpdateShoot(PlayerState.PlayerMode.Idle);
    }

    private void RecoilEffect()
    {
        bool dispersion = true;
        if (dispersion)
        {
            Player.Recoil = CurrentGun.upDispersion;
            StartCoroutine(DispersionDelay());
        }
    }
    private IEnumerator DispersionDelay()
    {
        yield return new WaitForSeconds(0.1f);
        Player.Recoil = 0;
    }
    private void CreateShootHitParticles(Vector3 HitPos, Vector3 Normal, Transform parent)
    {
        GameObject bullet = AmmoPool.GetNextElement();
        bullet.SetActive(true);
        bullet.transform.rotation = Quaternion.LookRotation(Normal);
        bullet.transform.position = HitPos;
        bullet.transform.parent = parent;

    }
    private IEnumerator ShootingDelay()
    {
        yield return new WaitForSeconds(0.2f);
        playerState.UpdateShoot(PlayerState.PlayerMode.Idle);
    }
    //**********************************************************
    //**********************************************************
    //**********************************************************
    //CHARGING
    //**********************************************************
    //**********************************************************
    //**********************************************************
    public void Charging()
    {
        if (CurrentValues.CurrentBulletHold > 0)
        {
            //si sigue habiendo más balas q las que puede tener cargadas
            if (CurrentValues.CurrentBulletHold > CurrentValues.BulletForCharger)
            {
                CurrentValues.CurrentBulletHold -= (CurrentValues.BulletForCharger - CurrentValues.CurrentBullets);
                CurrentValues.CurrentBullets = CurrentValues.BulletForCharger;
            }
            else if (CurrentValues.CurrentBulletHold <= CurrentValues.BulletForCharger)
            {
                int total = CurrentValues.BulletForCharger - CurrentValues.CurrentBullets;

                if (CurrentValues.CurrentBulletHold > total)
                    CurrentValues.CurrentBulletHold -= total;
                else if (CurrentValues.CurrentBulletHold <= total)
                {
                    int t = total;
                    total = CurrentValues.CurrentBulletHold;
                    CurrentValues.CurrentBulletHold -= t;
                }
                CurrentValues.CurrentBullets += total;
            }
            if (CurrentValues.CurrentBulletHold < 0)
                CurrentValues.CurrentBulletHold = 0;
        }
        
        UpdateTextUI(CurrentValues.CurrentBullets, CurrentValues.CurrentBulletHold, CurrentValues.BulletForCharger);
        StartCoroutine(ChargingDelay());
    }
    private IEnumerator ChargingDelay()
    {
        yield return new WaitForSeconds(4.5f);
        playerState.UpdateShoot(PlayerState.PlayerMode.Idle);
    }
    //**********************************************************
    //UPDATE TEXT AND BULLETS
    //**********************************************************
    public void UpdateBullets()
    {
        CurrentValues.CurrentBullets -= 1;
        
        UpdateTextUI(CurrentValues.CurrentBullets, CurrentValues.CurrentBulletHold, CurrentValues.BulletForCharger);
    }
    public void UpdateTextUI(int c, int m, int f)
    {
        delegateUI?.Invoke(c,m,f);
    }
    public void AddAmmo(int value)
    {
        float total = value + CurrentValues.CurrentBulletHold;

        if (total > CurrentValues.MaxBulletsHold)
        {
            CurrentValues.CurrentBulletHold += (CurrentValues.MaxBulletsHold - CurrentValues.CurrentBulletHold);
        }
        else
            CurrentValues.CurrentBulletHold += value;

        UpdateTextUI(CurrentValues.CurrentBullets, CurrentValues.CurrentBulletHold, CurrentValues.BulletForCharger);
    }

    public void ResetsShootStates()
    {
        CurrentValues.CurrentBullets =ResetCurrent;
        CurrentValues.CurrentBulletHold=ResetCurrentHold;
        UpdateTextUI(CurrentValues.CurrentBullets, CurrentValues.CurrentBulletHold, CurrentValues.BulletForCharger);
    }
}
