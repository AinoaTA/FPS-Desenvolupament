using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HudController : MonoBehaviour
{
    public Animator GameOver;

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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
            GameOverAction();
        
    }
    private void GameOverAction()
    {
        GameOver.SetBool("GameOver", true);
        UnLockCursor();
    }
}
