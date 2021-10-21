using UnityEngine;
public class ShieldItem : Items
{

    [SerializeField] private int m_ShieldCount;

    public int m_ShieldMinCount = 20;
    public int m_ShieldMaxCount = 40;
    public override bool CanPick()
    {
        if (GameController.GetGameController().GetPlayer().GetComponent<HealthSystemPlayer>().m_ShieldLifeTime != GameController.GetGameController().GetPlayer().GetComponent<HealthSystemPlayer>().m_MaxShieldLifeTime)
            return true;
        return false;
    }

    public override void Pick()
    {
        TeleportController.GetTeleportController().m_ItemsUsed.Add(gameObject.GetComponent<Items>());
        m_ShieldCount = Random.Range(m_ShieldMinCount, m_ShieldMaxCount);
        gameObject.SetActive(false);

        GameController.GetGameController().GetPlayer().GetComponent<HealthSystemPlayer>().AddShieldLife(m_ShieldCount);
    }

    public override void ResetItem(GameObject item)
    {
        item.SetActive(true);
    }
}
