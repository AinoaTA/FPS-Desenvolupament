using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportController : MonoBehaviour
{
    public List<Teleport> m_Activated;
    static TeleportController m_TeleportController;

    static public TeleportController GetTeleportController()
    {
        return m_TeleportController;
    }
    public Vector3 SpawnToLastTeleport()
    {
        return m_Activated[m_Activated.Count-1].m_ToSpawn;
    }
}
