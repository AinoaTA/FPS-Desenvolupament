using UnityEngine;

public class AmmoItem : Items
{
    [SerializeField]private int nRandom;
    public int m_AmmoMinCount = 10;
    public int m_AmmoMaxCount = 51;

    public override bool CanPick()
    {
        if (GameController.GetGameController().GetPlayer().GetComponent<Shoot>().GetMaxBullets() != GameController.GetGameController().GetPlayer().GetComponent<Shoot>().GetCurrentBulletSaved())
            return true;
        return false;
    }

    public override void Pick()
    {
        nRandom = Random.Range(m_AmmoMinCount, m_AmmoMaxCount);
        gameObject.SetActive(false);
        GameController.GetGameController().GetPlayer().GetComponent<Shoot>().AddAmmo(nRandom);
    }
}
