using UnityEngine;

public class HudController : MonoBehaviour
{
    private Animator m_GameOver;

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
        LockCursor();
    }

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
}
