using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportController : MonoBehaviour
{
    public List<Teleport> m_Activated;
    static TeleportController m_TeleportController;
    public List<Items> m_ItemsUsed;
    public List<DroneEnemy> m_Enemies;
    private void Start()
    {
        m_TeleportController = this;
    }

    static public TeleportController GetTeleportController()
    {
        return m_TeleportController;
    }
    public Vector3 SpawnToLastTeleport()
    {
        if(m_ItemsUsed.Count!=0)
           TeleportResetItems();
        if (m_Enemies.Count != 0)
            TeleportResetEnemies();

        GameController.GetGameController().ResetLevel();

        return m_Activated[m_Activated.Count-1].m_ToSpawn.position;
    }

    public void ButtonLastCheckPoint()
    {
        GameController.GetGameController().GetPlayer().transform.position = SpawnToLastTeleport();
        HudController.GetHudController().DesactiveGameOver();

    }

    private void TeleportResetItems()
    {
        
        for (int i = 0; i < m_ItemsUsed.Count; i++)
        {
            m_ItemsUsed[i].ResetItem(m_ItemsUsed[i].gameObject);
        }
        m_ItemsUsed.Clear();
    }

    private void TeleportResetEnemies()
    {
        for (int i = 0; i < m_Enemies.Count; i++)
        {
            m_Enemies[i].GetComponent<HealthSystemEnemy>().ResetEnemy();
            m_Enemies[i].ResetStateEnemy();
        }
        m_Enemies.Clear();
    }
}
