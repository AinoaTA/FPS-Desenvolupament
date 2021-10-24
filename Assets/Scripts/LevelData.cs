using System.Collections.Generic;
using UnityEngine;

public class LevelData: MonoBehaviour
{

    public List<Items> m_AllObjects;

    public List<Items> m_ItemsUsed;
    public List<DroneEnemy> m_Enemies;

    public void ResetLastCheckPoint()
    {
        for (int i = 0; i < m_AllObjects.Count; i++)
            if (m_AllObjects[i] == null)
                m_AllObjects[i].gameObject.SetActive(true);

        ResetTeleportObjects();
    }
    public void ResetTeleportObjects()
    {
        if (m_ItemsUsed.Count != 0)
            TeleportResetItems();
        if (m_Enemies.Count != 0)
            TeleportResetEnemies();

    }


    private void TeleportResetItems()
    {
        for (int i = 0; i < m_ItemsUsed.Count; i++)
            m_ItemsUsed[i].ResetItem(m_ItemsUsed[i].gameObject);
        
        m_ItemsUsed.Clear();
    }

    private void TeleportResetEnemies()
    {
        for (int i = 0; i < m_Enemies.Count; i++)
            m_Enemies[i].ResetStateEnemy();
        
        m_Enemies.Clear();
    }

    private void Awake()
    {
        GameController.GetGameController().SetLevelata(this);
    }
}

