using System.Collections;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public Gun CurrentGun;
    public FPS Player;

    private int maxBulletSaved;
    private int CurrentBulletsSaved;
    private int bulletForCharger;
    private int CurrentBullets;
    public int GetMaxBullets()
    {
        return maxBulletSaved;
    }
    public int GetCurrentBullets()
    {
        return CurrentBullets;
    }

    public int GetCurrentBulletSaved()
    {
        return CurrentBulletsSaved;
    }


    public Camera PCamera;
    public GameObject BulletPrefab;
    public LayerMask m_ShootLayerMask;

    //to call event delegate
    private PlayerState playerState;

    public delegate void DelegateUI(int current, int max, int forCharger);//string text);
    public static DelegateUI delegateUI;


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

    private void Awake()
    {
        playerState = FindObjectOfType<PlayerState>();
    }
    private void Start()
    {

        bulletForCharger = CurrentGun.bulletForCharger;
        CurrentBullets = CurrentGun.currentBullets;
        CurrentBulletsSaved = CurrentGun.maxBulletSaved;
        maxBulletSaved = CurrentBulletsSaved;

        UpdateTextUI();

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
            print(l_RaycastHit.collider.name);
            //CreateShootHitParticles(l_RaycastHit.point, l_RaycastHit.normal);
            if (l_RaycastHit.collider.CompareTag("Enemy"))
            {
                l_RaycastHit.collider.GetComponent<HitCollider>().Hit();
            }else if(l_RaycastHit.collider.CompareTag("Gallery"))
            {
                GalleryAnimation temp = l_RaycastHit.collider.GetComponentInParent<GalleryAnimation>();
                temp.SetShot();

                ShooterPoints.GetShooterPoints().AddPoints(temp.m_Points);
            }
                   
            UpdateBullets();

            RecoilEffect();
            StartCoroutine(ShootingDelay());
        }
        else { playerState.UpdateShoot(PlayerState.PlayerMode.Idle); }
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
    private void CreateShootHitParticles(Vector3 HitPos, Vector3 Normal)
    {
        GameObject bullet = Instantiate(BulletPrefab, HitPos, Quaternion.identity);
        bullet.transform.rotation = Quaternion.LookRotation(Normal);

       // Bullet b = new Bullet(transform, Normal, 10f);
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
        if (CurrentBulletsSaved > 0)
        {
            //si sigue habiendo más balas q las que puede tener cargadas
            if(CurrentBulletsSaved > bulletForCharger)
            {
                CurrentBulletsSaved -= (bulletForCharger - CurrentBullets);
                CurrentBullets = bulletForCharger;
            } 
            else if (CurrentBulletsSaved <= bulletForCharger)
            {
                int total = bulletForCharger -CurrentBullets;

                if (CurrentBulletsSaved > total)
                    CurrentBulletsSaved -= total;
                else if (CurrentBulletsSaved <= total)
                {
                    int t = total;
                    total = CurrentBulletsSaved;
                    CurrentBulletsSaved -= t;
                }
                CurrentBullets += total;
            }
            if (CurrentBulletsSaved < 0)
                CurrentBulletsSaved = 0;            
        }
        UpdateTextUI();
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
        CurrentBullets -= 1;
        UpdateTextUI();
    }
    private void UpdateTextUI()
    {
        delegateUI?.Invoke(CurrentBullets,CurrentBulletsSaved,bulletForCharger);
    }

    public void AddAmmo(int value)
    {
        float total = value + CurrentBulletsSaved;
        if (total > maxBulletSaved)
            CurrentBulletsSaved += (maxBulletSaved - CurrentBulletsSaved);
        else
            CurrentBulletsSaved += value;

        UpdateTextUI();
    }
}
