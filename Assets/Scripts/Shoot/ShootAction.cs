using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootAction : MonoBehaviour
{

    public GameObject BulletPrefab;
    public LayerMask m_ShootLayerMask;
    public ShootGun ShootGun;

    private Camera PCamera;
    //to call event delegate
    private PlayerState playerState;
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

    private void Shooting()
    {
        Ray l_ray = PCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit l_RaycastHit;
        if (Physics.Raycast(l_ray, out l_RaycastHit, 200.0f, m_ShootLayerMask))
        {
            CreateShootHitParticles(l_RaycastHit.point, l_RaycastHit.normal);
            ShootGun.UpdateBullets();
            print(PlayerState.PlayerStateMode);
            StartCoroutine(ShootingDelay());
        }
    }
    private IEnumerator ShootingDelay()
    {
        yield return new WaitForSeconds(1f);
        playerState.UpdateShoot(PlayerState.PlayerMode.Idle);
    }
    private void CreateShootHitParticles(Vector3 HitPos, Vector3 Normal)
    {
        GameObject bullet = Instantiate(BulletPrefab, HitPos, Quaternion.identity);
        bullet.transform.rotation = Quaternion.LookRotation(Normal);
    }
    private void Charging()
    {
        StartCoroutine(ChargingDelay());
    }
    private IEnumerator ChargingDelay()
    {

        yield return new WaitForSeconds(4.5f);
        playerState.UpdateShoot(PlayerState.PlayerMode.Idle);
    }
}
