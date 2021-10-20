using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLaser : MonoBehaviour
{
    private Vector3 m_Destination;
    private Vector3 m_CurrentVelocity = Vector3.one;
    [SerializeField]private float m_Speed = 50;
    [SerializeField]private int m_Damage = 30;

    private void Start()
    {
        m_Destination = GameController.GetGameController().GetPlayer().transform.position;
        transform.localRotation = Quaternion.LookRotation(m_Destination);

    }
    private void Update()
    {
        gameObject.transform.position = Vector3.SmoothDamp(transform.position, m_Destination, ref m_CurrentVelocity, m_Speed*Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameController.GetGameController().GetPlayer().GetComponent<HealthSystemPlayer>().GetDamage(m_Damage);
            gameObject.SetActive(false);
        }
        else if (other.CompareTag("Default") || other.CompareTag("Ignore Raycast"));
            gameObject.SetActive(false);   
    }
}
