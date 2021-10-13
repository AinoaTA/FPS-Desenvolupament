using System.Collections;
using UnityEngine;

public class ShootGun : MonoBehaviour
{
    public Gun currentGun;

    //private int maxBulletSaved => 10;
    //private int bulletForCharger = 5;
    //private int currentBullets = 5;

    //private float timerRaycast=0f;
    //private float maxTimerRaycast = 2f;

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
        UpdateTextUI();
    }

    private void Update()
    {
        //if(PlayerState.PlayerStateMode== PlayerState.PlayerMode.Shooting)
        // timerRaycast += Time.deltaTime;

        //if (timerRaycast >= maxTimerRaycast)
        //{
        //    timerRaycast = 0f;
        //    playerState.UpdateShoot(PlayerState.PlayerMode.Idle);
        //}
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
            UpdateBullets();

            StartCoroutine(ShootingDelay());
        }
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
        if (currentGun.maxBulletSaved > 0)
        {
            //si sigue habiendo más balas q las que puede tener cargadas
            if(currentGun.maxBulletSaved > currentGun.bulletForCharger)
            {
                currentGun.maxBulletSaved -= (currentGun.bulletForCharger - currentGun.currentBullets);
                currentGun.currentBullets = currentGun.bulletForCharger;
            } 
            else if (currentGun.maxBulletSaved <= currentGun.bulletForCharger)
            {
                int total = currentGun.bulletForCharger - currentGun.currentBullets;
                // total = 5-3 = 2
                // total = 5-1 = 4 pero si tenemos solo 3 en el cargador

                if (currentGun.maxBulletSaved > total)
                    currentGun.maxBulletSaved -= total;
                else if (currentGun.maxBulletSaved <= total)
                {
                    int t = total;
                    total = currentGun.maxBulletSaved;
                    currentGun.maxBulletSaved -= t;
                }
                currentGun.currentBullets += total;
            }

            if (currentGun.maxBulletSaved < 0)
                currentGun.maxBulletSaved = 0;            
        }
        UpdateTextUI();
        StartCoroutine(ChargingDelay());
    }
    private IEnumerator ChargingDelay()
    {
        yield return new WaitForSeconds(4.5f);
        playerState.UpdateShoot(PlayerState.PlayerMode.Idle);
    }
    public void Idle() {}

    //**********************************************************
    //UPDATE TEXT AND BULLETS
    //**********************************************************
    public void UpdateBullets()
    {
        currentGun.currentBullets -= 1;
        UpdateTextUI();
    }
    private void UpdateTextUI()
    {
        delegateUI?.Invoke(currentGun.currentBullets.ToString() + " / " + currentGun.maxBulletSaved.ToString());
    }
}
