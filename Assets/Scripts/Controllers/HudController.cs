using UnityEngine;

public class HudController : MonoBehaviour
{
    public GameObject m_GameOver;
    static HudController m_HudController;
    public GameObject Pointer;
    public GameObject Pause;

    private void OnEnable()
    {
        HealthSystemPlayer.delegateGameOver += GameOverAction;
    }

    private void OnDisable()
    {
        HealthSystemPlayer.delegateGameOver -= GameOverAction;
    }

    private void Start()
    {
        m_HudController = this;
        LockCursor();
    }

    static public HudController GetHudController() => m_HudController;
    private void LockCursor()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void UnLockCursor()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
    public void GameOverAction()
    {
        UnLockCursor();
        m_GameOver.SetActive(true);
        Time.timeScale = 0;
    }

    public void DesactiveGameOver()
    {
        LockCursor();
        m_GameOver.SetActive(false);
        Time.timeScale =1;

    }

    void Update()
    {
        if (PlayerState.PlayerStateMode == PlayerState.PlayerMode.Charging)
            Pointer.SetActive(false);
        else
            Pointer.SetActive(true);

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pause.SetActive(true);
            UnLockCursor();
            Time.timeScale = 0f;
        }
            
    }

    public void QuitPauseMenu()
    {
        LockCursor();
        print("a");
        Pause.SetActive(false);
        Time.timeScale = 1;
    }
}
