using System.Collections;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public Camera PCamera;
    public GameObject BulletPrefab;
    public LayerMask m_ShootLayerMask;
    public ShootGun ShootGun;
    private Animator animator;
    public enum ShootMode
    {
        Idle,
        Shooting,
        Charging
    }

    public ShootMode sMode;
    private void Start()
    {
        animator = ShootGun.GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            sMode = ShootMode.Charging;
            print("a");
        }
        UpdateShoot();
        print(sMode);
    }
    private void UpdateShoot()
    {
        switch (sMode)
        {
            case ShootMode.Idle:

                animator.SetBool("Idle", true);
                animator.SetBool("Charging", false);

                //
                ShootIdle();
                break;

            case ShootMode.Shooting:

                animator.SetBool("Shooting", true);
                //
                ShootAction();
                break;

            case ShootMode.Charging:
                animator.SetBool("Charging", true);
                animator.SetBool("Idle", false);

                StartCoroutine(ShootCharging());
                break;
        }
    }
    private void ShootIdle()
    {}

    private void ShootAction()
    {
        Ray l_ray = PCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit l_RaycastHit;
        if (Physics.Raycast(l_ray, out l_RaycastHit, 200.0f, m_ShootLayerMask))
        {
            CreateShootHitParticles(l_RaycastHit.point, l_RaycastHit.normal);
            ShootGun.UpdateBullets();
            sMode = ShootMode.Idle;
        }
    }

    private void CreateShootHitParticles(Vector3 HitPos, Vector3 Normal)
    {
        GameObject instance = Instantiate(BulletPrefab, HitPos, Quaternion.identity);
        instance.transform.rotation = Quaternion.LookRotation(Normal); 
    }

    private IEnumerator ShootCharging()
    {
        ShootGun.Charger();
     //   yield return new WaitForSeconds(animator.SetBool();
        sMode = ShootMode.Idle;
    }
}
