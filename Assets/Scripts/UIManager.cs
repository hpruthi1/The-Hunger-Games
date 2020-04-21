using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Mirror;

public class UIManager : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnlineScene()
    {
        SceneManager.LoadScene(1);
    }

    public void OflineScene()
    {

        SceneManager.LoadScene(2);
    }

   // public void onPracticeButtoonClick()
    //{
      //  SceneManager.LoadScene(1);
      //  FindObjectOfType<NetworkManagerHUD>().enabled = false;
    //}
}
