using UnityEngine;
public class LifeItem : Items
{

    public int m_LifeCount;

    public override bool CanPick()
    {
        return true;
    }

    public override void Pick()
    {
        gameObject.SetActive(false);

        GameController.GetGameController().GetPlayer().AddLife(m_LifeCount);
       // print("nw life");
    }
}
