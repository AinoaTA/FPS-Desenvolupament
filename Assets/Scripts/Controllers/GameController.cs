using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public Transform m_DestroyObjects;
    static GameController m_GameController;
    LevelData m_LevelData;
    FPS m_player;

    private void Awake()
    {
        if (m_GameController == null)
        {
            m_GameController = this;
            GameObject.DontDestroyOnLoad(gameObject);
        }
        else
            GameObject.Destroy(this.gameObject);         
    }

    public void ResetLevel()
    {
        for (int i = 0; i < m_DestroyObjects.childCount; i++)
        {
            Transform l_Transform =m_DestroyObjects.GetChild(i);

            GameObject.Destroy(l_Transform.gameObject);
        }
    }
    static public GameController GetGameController()
    {
        return m_GameController;
    }

    public void SetLevelata(LevelData _levelData)
    {
        m_LevelData = _levelData;
    }

    public void SetPlayer(FPS fpsPlayer)
    {
        m_player = fpsPlayer;
    }

    public FPS GetPlayer()
    {
        return m_player;
    }
}
