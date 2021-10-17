using System.Collections;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public Gun CurrentGun;
    public FPS Player;

    private int maxBulletSaved;
    private int bulletForCharger;
    public int CurrentBullets;


    public Camera PCamera;
    public GameObject BulletPrefab;
    public LayerMask m_ShootLayerMask;

    //to call event delegate
    private PlayerState playerState;

    public delegate void DelegateUI(string text);
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
        CurrentBullets = CurrentGun.currentBullets;
        maxBulletSaved = CurrentGun.maxBulletSaved;
        bulletForCharger = CurrentGun.bulletForCharger;
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
            CreateShootHitParticles(l_RaycastHit.point, l_RaycastHit.normal);
            if(l_RaycastHit.collider.CompareTag("Enemy"))
                    l_RaycastHit.collider.GetComponent<HitCollider>().Hit();
            UpdateBullets();

            DispersionEffect();
            StartCoroutine(ShootingDelay());
        }
        else { playerState.UpdateShoot(PlayerState.PlayerMode.Idle); }
    }

    private void DispersionEffect()
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
        print("no updateo");
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
        if (maxBulletSaved > 0)
        {
            //si sigue habiendo más balas q las que puede tener cargadas
            if(maxBulletSaved > bulletForCharger)
            {
                maxBulletSaved -= (bulletForCharger - CurrentBullets);
                CurrentBullets = bulletForCharger;
            } 
            else if (maxBulletSaved <= bulletForCharger)
            {
                int total = bulletForCharger -CurrentBullets;

                if (maxBulletSaved > total)
                    maxBulletSaved -= total;
                else if (maxBulletSaved <= total)
                {
                    int t = total;
                    total = maxBulletSaved;
                    maxBulletSaved -= t;
                }
                CurrentBullets += total;
            }
            if (maxBulletSaved < 0)
                maxBulletSaved = 0;            
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
        delegateUI?.Invoke(CurrentBullets.ToString() + " / " + maxBulletSaved.ToString());
    }

    public void AddAmmo(int value)
    {

        maxBulletSaved += value;
        UpdateTextUI();
    }
}
