using UnityEngine;

public class HealthSystemEnemy : MonoBehaviour
{
    public float m_Life;
    private Drop m_Drop;
    private void Awake()
    {
        m_Drop = GetComponent<Drop>();
    }

    private void Start()
    {
        m_Life = 100f;
    }
    public void GetDamage(float damage)
    {
        m_Life -= damage;
        print("vida drone"+m_Life);
        if(m_Life <= 0)
        {
           // m_Drop.DropItem();
        }
    }
}
