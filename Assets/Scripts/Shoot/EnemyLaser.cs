using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLaser : MonoBehaviour
{
    private Vector3 m_Destination;
    private Vector3 m_CurrentVelocity = Vector3.one;
    [SerializeField]private float m_Speed = 50;

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

        m_Destination = GameController.GetGameController().GetPlayer().transform.position;
        transform.localRotation = Quaternion.LookRotation(m_Destination);

    }
    private void Update()
    {
        print("soooooooooooooooooooy unaaaaaaaaaaaaaaaaaa laseeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeer");
        gameObject.transform.position = Vector3.SmoothDamp(transform.position, m_Destination, ref m_CurrentVelocity, m_Speed*Time.deltaTime);
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    GameObject.Destroy(gameObject);
    //}
}
