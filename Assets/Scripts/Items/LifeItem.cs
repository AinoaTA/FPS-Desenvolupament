using UnityEngine;
public class LifeItem : Items
{

    [SerializeField] private int m_LifeCount;

    public int m_LifeMinCount = 10;
    public int m_LifeMaxCount = 30;

    public override bool CanPick()
    {
        if (GameController.GetGameController().GetPlayer().GetComponent<HealthSystemPlayer>().currentLife != GameController.GetGameController().GetPlayer().GetComponent<HealthSystemPlayer>().maxLife)
            return true;
        return false;
    }

    public override void Pick()
    {
        GameController.GetGameController().GetLevelData().m_ItemsUsed.Add(gameObject.GetComponent<Items>());
        m_LifeCount = Random.Range(m_LifeMinCount, m_LifeMaxCount);
        gameObject.SetActive(false);

        GameController.GetGameController().GetPlayer().GetComponent<HealthSystemPlayer>().AddLife(m_LifeCount);
    }

    public override void ResetItem(GameObject item)
    {
        item.SetActive(true);
    }
}
