using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public void OnlineScene()
    {
        SceneManager.LoadScene(1);
    }

    public void OfflineScene()
    {
        SceneManager.LoadScene(2);
    }

    public void onExitButtoon()
    {
        Application.Quit();
    }
}
