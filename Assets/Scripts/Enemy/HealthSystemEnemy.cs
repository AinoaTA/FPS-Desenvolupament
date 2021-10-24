using UnityEngine;
using UnityEngine.UI;
public class HealthSystemEnemy : MonoBehaviour
{
    public float m_Life;
    private Drop m_Drop;

    public Slider m_HealthBar;
    private float m_MaxLife = 100f;

    private void Awake()
    {
        m_Drop = GetComponent<Drop>();
    }

    private void Start()
    {
        m_Life = m_MaxLife;
        m_HealthBar.minValue = 0;
        m_HealthBar.maxValue = m_MaxLife;
        m_HealthBar.value = m_Life;
    }
    public void GetDamage(float damage)
    {
        m_Life -= damage;

        if(m_HealthBar!=null)
        m_HealthBar.value = m_Life;
        if (m_Life <= 0)
        {
           GameController.GetGameController().GetLevelData().m_EnemiesDeath.Add(gameObject.GetComponent<DroneEnemy>());
           m_Drop.DropItem();
        }
    }

    private void Update()
    {
        if (m_HealthBar != null)
            m_HealthBar.value = m_Life;
    }

    public void ResetEnemy()
    {
        m_Life = 100f;
        m_HealthBar.value = m_Life;
        gameObject.SetActive(true);
    }

}
