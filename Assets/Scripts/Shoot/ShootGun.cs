using System.Collections;
using UnityEngine;

public class ShootGun : MonoBehaviour, IGun
{
    private float maxBulletSaved = 100f;
    private float bulletForCharger = 50f;
    public float currentBullets = 5f;

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

    //SHOOTING
    public void Shooting()
    {
        Ray l_ray = PCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit l_RaycastHit;
        if (Physics.Raycast(l_ray, out l_RaycastHit, 200.0f, m_ShootLayerMask))
        {
            CreateShootHitParticles(l_RaycastHit.point, l_RaycastHit.normal);
            UpdateBullets();
            print(PlayerState.PlayerStateMode);
            StartCoroutine(ShootingDelay());
        }
    }
    private void CreateShootHitParticles(Vector3 HitPos, Vector3 Normal)
    {
        GameObject bullet = Instantiate(BulletPrefab, HitPos, Quaternion.identity);
        bullet.transform.rotation = Quaternion.LookRotation(Normal);

        Bullet b = new Bullet(transform, Normal, 10f);
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
            maxBulletSaved -= (bulletForCharger-currentBullets);
            if (maxBulletSaved <= 0)
                maxBulletSaved = 0;

            currentBullets = bulletForCharger;
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
