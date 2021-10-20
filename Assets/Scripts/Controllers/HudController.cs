using UnityEngine;

public class HudController : MonoBehaviour
{
    public Animator m_GameOver;
    static HudController m_HudController;

    private void OnEnable()
    {
        HealthSystemPlayer.delegateGameOver += GameOverAction;
    }

    private void OnDisable()
    {
        HealthSystemPlayer.delegateGameOver -= GameOverAction;
    }

    private void Awake()
    {
        m_GameOver = GetComponent<Animator>();
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
    private void GameOverAction()
    {
        m_GameOver.SetBool("GameOver", true);
        UnLockCursor();
    }

    public void DesactiveGameOver()
    {
        m_GameOver.SetBool("GameOver", false);
        UnLockCursor();
    }
}
