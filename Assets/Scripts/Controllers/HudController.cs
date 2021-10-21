using UnityEngine;

public class HudController : MonoBehaviour
{
    public GameObject m_GameOver;
    static HudController m_HudController;

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

        m_GameOver.GetComponent<Animator>().SetBool("GameOver", true);
        m_GameOver.GetComponent<Animator>().SetBool("Idle", false);
    }

    public void DesactiveGameOver()
    {
        UnLockCursor();


        m_GameOver.GetComponent<Animator>().SetBool("GameOver", false);
        m_GameOver.GetComponent<Animator>().SetBool("Idle", true);
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
            UnLockCursor();
            
    }
}
