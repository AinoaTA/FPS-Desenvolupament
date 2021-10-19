using UnityEngine;

public class HitCollider : MonoBehaviour
{
    public enum HitColliderType
    {
        HELIX=0,
        BODY,
        HEAD
    }
    public HitColliderType m_HitColliderType;
    private HealthSystemEnemy m_HealthSystem;
    public DroneEnemy m_Enemy;
    const int m_HelixHitAmount = 20;
    const int m_BodyHitAmount = 10;
    const int m_HeadHitAmount = 50;
    private void Start()
    {
        m_HealthSystem = GetComponentInParent<HealthSystemEnemy>();
    }
    public void Hit()
    {
        int l_HitAmount = m_HeadHitAmount;
        if (m_HitColliderType == HitColliderType.BODY)
            l_HitAmount = m_BodyHitAmount;
        else if (m_HitColliderType == HitColliderType.HELIX)
            l_HitAmount = m_HelixHitAmount;
        m_HealthSystem.GetDamage(l_HitAmount);
        m_Enemy.Hit();
    }
}