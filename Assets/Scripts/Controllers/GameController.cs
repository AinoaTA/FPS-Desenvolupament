using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public Transform m_DestroyObjects;
    private DoorShoot Gate;
    static GameController m_GameController;

    private DoorKey m_DoorKey;
    LevelData m_LevelData;
    FPS m_player;

    private void Awake()
    {
        if (m_GameController == null)
        {
            m_GameController = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(this.gameObject);         
    }

    void Start()
    {
        Gate = FindObjectOfType<DoorShoot>();
        m_DoorKey = FindObjectOfType<DoorKey>();
    }
    public void ResetLevel()
    {
        for (int i = 0; i < m_DestroyObjects.childCount; i++)
        {
            Transform l_Transform =m_DestroyObjects.GetChild(i);
            l_Transform.gameObject.SetActive(false);
        }


        m_LevelData.ResetLastCheckPoint();
        m_LevelData.ResetDecansLevel();
        TeleportController.GetTeleportController().ResetTeleport();
        HudController.GetHudController().QuitPauseMenu();
        HudController.GetHudController().DesactiveGameOver();

        ResetStats();
        if (Gate != null)
            Gate.ResetGate();
        if (m_DoorKey != null)
            m_DoorKey.ResetKeyDoor();
    }

    public void ResetStats()
    {
        m_player.ResetPlayerPos();
        m_player.GetComponent<Shoot>().ResetsShootStates();
        m_player.GetComponent<HealthSystemPlayer>().ResetStates();
    }
    static public GameController GetGameController()
    {
        return m_GameController;
    }

    
    public void SetLevelata(LevelData _levelData)
    {
        m_LevelData = _levelData;
    }
    public LevelData GetLevelData()
    {
        return m_LevelData;
    }

    public void SetPlayer(FPS fpsPlayer)
    {
        m_player = fpsPlayer;
    }

    public FPS GetPlayer()
    {
        return m_player;
    }

    public void CheckPointPlayerStats(float cLife, float cShield, int cBullet, int cBulletHold)
    {
        print("CheckplayerStates");
        m_player.GetComponent<HealthSystemPlayer>().m_ShieldLifeTime = cShield;
        m_player.GetComponent<HealthSystemPlayer>().currentLife = cLife;
        m_player.GetComponent<Shoot>().UpdateTextUI(cBullet, cBulletHold, m_player.GetComponent<Shoot>().CurrentValues.BulletForCharger);
        m_player.GetComponent<Shoot>().CurrentValues.CurrentBulletHold = cBulletHold;
        m_player.GetComponent<Shoot>().CurrentValues.CurrentBullets = cBullet;
    }
}
