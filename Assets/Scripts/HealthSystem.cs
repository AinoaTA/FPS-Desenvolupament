using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    public float m_Life;
    private Drop m_Drop;

    private Enemy m_Enemy;

    private void Awake()
    {
        m_Enemy = GetComponent<Enemy>();
        m_Drop = GetComponent<Drop>();
    }

    private void Start()
    {
        m_Life = 10;//m_Enemy.m_EnemyData.Life;
    }
    public void GetDamage(float damage)
    {
        m_Life -= damage;
   
        if(m_Life <= 0)
        {
            m_Drop.DropItem();
            this.gameObject.SetActive(false);
        }
    }
}
