using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportController : MonoBehaviour
{
    public Teleport m_Activated;
    static TeleportController m_TeleportController;
    public List<Teleport> m_Teleports;
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

        //si no se ha pasado por encima de un checkpoint le llevará a la posición de inicio.
        if (m_Activated != null)
        {
            GameController.GetGameController().CheckPointPlayerStats(m_Activated.HealthSaved, m_Activated.ShieldSaved, m_Activated.CurrentBulletSaved, m_Activated.CurrentBulletHold);
            GameController.GetGameController().GetLevelData().ResetTeleportObjects();
            GameController.GetGameController().GetPlayer().GetComponent<Shoot>().AmmoPool.ResetElement();

            return m_Activated.m_ToSpawn.position;
        }
        return GameController.GetGameController().GetPlayer().ResetPosition.position;
    }

    public void ButtonLastCheckPoint()
    {
        GameController.GetGameController().GetPlayer().GetComponent<Shoot>().AmmoPool.ResetElement();
        GameController.GetGameController().GetPlayer().transform.position = SpawnToLastTeleport();
        HudController.GetHudController().QuitPauseMenu();
        HudController.GetHudController().DesactiveGameOver();
    }

    public void ResetTeleport()
    {
        m_Activated = null;
        for (int i = 0; i < m_Teleports.Count; i++)
        {
            m_Teleports[i].ResetTeleport();
        }
    }
}
