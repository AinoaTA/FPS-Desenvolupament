using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelData: MonoBehaviour
{

    public List<Items> m_AllObjects;
    public List<DroneEnemy> m_AllEnemies;

    public List<Items> m_ItemsUsed;
    public List<DroneEnemy> m_EnemiesDeath;

    public void ResetLastCheckPoint()
    {
        for (int i = 0; i < m_AllObjects.Count; i++)
            if (m_AllObjects[i] == null)
                m_AllObjects[i].gameObject.SetActive(true);

        for (int i = 0; i < m_AllEnemies.Count; i++)
        {
            m_AllEnemies[i].ResetStateEnemy();
            print(i);
        }
            

        ResetTeleportObjects();
        ResetDecansLevel();
    }
    public void ResetTeleportObjects()
    {
        if (m_ItemsUsed.Count != 0)
            TeleportResetItems();
        if (m_EnemiesDeath.Count != 0)
            TeleportResetEnemies();
    }

    public void ResetDecansLevel()
    {
        List<GameObject> decans = new List<GameObject>();
        decans = GameObject.FindGameObjectsWithTag("Deca").ToList();

        for (int i = 0; i < decans.Count; i++)
        {
            decans[i].gameObject.SetActive(false);
        }
    }

    private void TeleportResetItems()
    {
        for (int i = 0; i < m_ItemsUsed.Count; i++)
            m_ItemsUsed[i].ResetItem(m_ItemsUsed[i].gameObject);
        
        m_ItemsUsed.Clear();
    }

    private void TeleportResetEnemies()
    {
        for (int i = 0; i < m_EnemiesDeath.Count; i++)
            m_EnemiesDeath[i].ResetStateEnemy();
        
        m_EnemiesDeath.Clear();
    }

    private void Awake()
    {
        GameController.GetGameController().SetLevelata(this);
    }
}

