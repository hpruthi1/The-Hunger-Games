using UnityEngine;
using UnityEngine.SceneManagement;
using Mirror;

public class UIManager : MonoBehaviour
{
    public void OnlineScene()
    {
        SceneManager.LoadScene("Start");
        GameObject.FindGameObjectWithTag("NetworkManager").GetComponent<NetworkManagerHUD>().enabled = true;
    }

    public void OfflineScene()
    {
        SceneManager.LoadScene("Practice");
    }

    public void onExitButtoon()
    {
        Application.Quit();
    }

    public void BackButton()
    {
        SceneManager.LoadScene("MainMenu"); 
        GameObject.FindGameObjectWithTag("NetworkManager").GetComponent<NetworkManagerHUD>().enabled = false;
    }

    public void PracticePlayAgain()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void EndScenePlayAgain()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
