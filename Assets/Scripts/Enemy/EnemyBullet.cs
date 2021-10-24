using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    private Vector3 m_Destination;
    private Vector3 m_CurrentVelocity = Vector3.one;
    [SerializeField]private float m_Speed = 70;
    [SerializeField]private int m_Damage = 30;

    private void Start()
    {
        m_Destination = GameController.GetGameController().GetPlayer().transform.position+Vector3.up*1.2f;
        //transform.rotation = transform.parent.localRotation;

    }
    private void Update()
    {
        gameObject.transform.position = Vector3.SmoothDamp(transform.position, m_Destination, ref m_CurrentVelocity, m_Speed*Time.deltaTime);
    }

    public void CreateABullet(GameObject Prefab, Transform ParentRotation)
    {
        GameObject l_Bullet = Instantiate(Prefab, ParentRotation.position, Quaternion.identity);
        l_Bullet.transform.rotation = ParentRotation.rotation;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            GameController.GetGameController().GetPlayer().GetComponent<HealthSystemPlayer>().GetDamage(m_Damage);

            
        gameObject.SetActive(false);
    }
}
