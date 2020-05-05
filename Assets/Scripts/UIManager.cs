using UnityEngine;
using UnityEngine.SceneManagement;
using Mirror;

public class UIManager : MonoBehaviour
{
    public void OnlineScene()
    {
        SceneManager.LoadScene(1);
        GameObject.FindGameObjectWithTag("NetworkManager").GetComponent<NetworkManagerHUD>().enabled = true;
    }

    public void OfflineScene()
    {
        SceneManager.LoadScene(2);
    }

    public void onExitButtoon()
    {
        Application.Quit();
    }

    public void BackButton()
    {
        SceneManager.LoadScene("New Scene");
        GameObject.FindGameObjectWithTag("NetworkManager").GetComponent<NetworkManagerHUD>().enabled = false;
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene("Practice");
    }
}
