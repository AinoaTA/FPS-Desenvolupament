using System.Collections;
using UnityEngine;

public class ShootGun : MonoBehaviour, IGun
{
    private float maxBulletSaved = 10f;
    private float bulletForCharger = 5f;
    private float currentBullets = 5f;

    private float timerRaycast=0f;
    private float maxTimerRaycast = 2f;

    public Camera PCamera;
    public GameObject BulletPrefab;
    public LayerMask m_ShootLayerMask;

    //to call event delegate
    private PlayerState playerState;

    public delegate void DelegateUI(string text);
    public static DelegateUI delegateUI;

    public float CurrentBullets => currentBullets;

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
        if(PlayerState.PlayerStateMode== PlayerState.PlayerMode.Shooting)
         timerRaycast += Time.deltaTime;

        if (timerRaycast >= maxTimerRaycast)
        {
            timerRaycast = 0f;
            playerState.UpdateShoot(PlayerState.PlayerMode.Idle);
        }
    }
    //SHOOTING
    public void Shooting()
    {
        bool ray = false;
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
        yield return new WaitForSeconds(1f);
        playerState.UpdateShoot(PlayerState.PlayerMode.Idle);
    }


    //CHARGING

    public void Charging()
    {
        if (maxBulletSaved > 0)
        {
            //si sigue habiendo más balas q las que puede tener cargadas
            if(maxBulletSaved >= bulletForCharger)
            {
                maxBulletSaved -= (bulletForCharger - currentBullets);
                currentBullets = bulletForCharger;
            } 
            else if(currentBullets<bulletForCharger)
            {
                float total = currentBullets + maxBulletSaved;
                print("total: " + total);
                float toCharge = total - bulletForCharger;

                currentBullets += Mathf.Abs(toCharge);
                maxBulletSaved -= Mathf.Abs(toCharge);

                if (total == bulletForCharger)
                {
                    currentBullets = bulletForCharger;
                    maxBulletSaved = 0;
                }
                    


            }

            if (maxBulletSaved < 0)
                maxBulletSaved = 0;

            UpdateTextUI();
            StartCoroutine(ChargingDelay());
        }
    }
    private IEnumerator ChargingDelay()
    {
        yield return new WaitForSeconds(4.5f);
        playerState.UpdateShoot(PlayerState.PlayerMode.Idle);
    }
    public void Idle()
    {

    }

    //UPDATE TEXT AND BULLETS
    public void UpdateBullets()
    {
        currentBullets -= 1;
        UpdateTextUI();

    }
    private void UpdateTextUI()
    {
        delegateUI?.Invoke(currentBullets.ToString() + " / " + maxBulletSaved.ToString());
    }
}
