using UnityEngine;

public class AmmoItem : Items
{

    public int m_AmmoCount = 10;

    public override bool CanPick()
    {
        return true;
    }

    public override void Pick()
    {
        gameObject.SetActive(false);
        GameController.GetGameController().GetPlayer().GetComponent<Shoot>().AddAmmo(m_AmmoCount);
    }
}
