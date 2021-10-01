using UnityEngine.SceneManagement;
using UnityEngine;

public class SceneController : MonoBehaviour
{
    public void RestartScene()
    {
        SceneManager.LoadScene("GamePlay");
    }
}
