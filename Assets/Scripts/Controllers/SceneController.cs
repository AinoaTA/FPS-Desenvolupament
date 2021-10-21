using UnityEngine.SceneManagement;
using UnityEngine;

public class SceneController : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Player"))
            LoadLevel2();

    }

    void LoadLevel2()
    {
        SceneManager.LoadSceneAsync("Level2");
    }

}
