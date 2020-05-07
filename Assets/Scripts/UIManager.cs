using UnityEngine;
using UnityEngine.SceneManagement;
using Mirror;

public class UIManager : MonoBehaviour
{
    public Audiomanager audiomanager = null;

    private void Start()
    {
        audiomanager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<Audiomanager>();
    }
    public void OnlineScene()
    {
        FindObjectOfType<Audiomanager>().Play("Click");
        SceneManager.LoadScene("Start");
        GameObject.FindGameObjectWithTag("NetworkManager").GetComponent<NetworkManagerHUD>().enabled = true;
    }

    public void OfflineScene()
    {
        FindObjectOfType<Audiomanager>().Play("Click");
        SceneManager.LoadScene("Practice");
    }

    public void onExitButtoon()
    {
        FindObjectOfType<Audiomanager>().Play("Click");
        Application.Quit();
    }

    public void BackButton()
    {
        FindObjectOfType<Audiomanager>().Play("Click");
        SceneManager.LoadScene("MainMenu"); 
        GameObject.FindGameObjectWithTag("NetworkManager").GetComponent<NetworkManagerHUD>().enabled = false;
    }

    public void PracticePlayAgain()
    {
        FindObjectOfType<Audiomanager>().Play("Click");
        SceneManager.LoadScene("MainMenu");
    }

    public void EndScenePlayAgain()
    {
        FindObjectOfType<Audiomanager>().Play("Click");
        SceneManager.LoadScene("MainMenu");
    }
}
