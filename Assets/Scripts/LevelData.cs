using System.Collections.Generic;
using UnityEngine;

public class LevelData: MonoBehaviour
{

    public List<Items> m_items;

    private void Awake()
    {
        GameController.GetGameController().SetLevelata(this);
    }
}

