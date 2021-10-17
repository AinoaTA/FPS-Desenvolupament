using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLaser : MonoBehaviour
{
    private Vector3 m_Initial, m_Destination;
    private Vector3 m_CurrentVelocity = Vector3.one;
    private float m_Speed = 1;

    //public EnemyLaser(GameObject prefab, Vector3 initial, Vector3 destination, float speed)
    //{
    //    m_Laser = prefab;
    //    print("new object");
    //    m_Initial = initial;
    //    m_Destination = destination;
    //    m_Speed = speed;

    //    Instantiate(m_Laser, m_Initial, Quaternion.identity);

    //}

    private void Start()
    {
        m_Initial = transform.position;
        m_Destination = GameController.GetGameController().GetPlayer().transform.position;
        transform.localRotation = Quaternion.LookRotation(m_Destination);

    }
    private void Update()
    {
        print("soooooooooooooooooooy unaaaaaaaaaaaaaaaaaa laseeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeer");
        this.transform.position = Vector3.SmoothDamp(m_Initial, m_Destination, ref m_CurrentVelocity, m_Speed*Time.deltaTime);
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    GameObject.Destroy(gameObject);
    //}
}
