using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class CharacterCustomization : MonoBehaviour
{
    #region Private .
    private PlayerInstance _playerInstance;
    #endregion

    public GameObject Alex;
    public GameObject Bryce;
    public GameObject James;
    public GameObject Player1;
    public GameObject Player2;
    public GameObject Player3;


    private void Awake()
    {
        
            
    }

    public void SelectCharacter(CharacterSelection selection)
    {
        _playerInstance.PlayerSpawner.TrySpawn(selection.CharacterType);
    }

    private void Start()
    {
        //if (isLocalPlayer)
        {
            James.SetActive(true);
            Bryce.SetActive(false);
            Alex.SetActive(false);
        }
    }

    public void onCharacter1Button()
    {
        //if (isLocalPlayer)
        {
            FindObjectOfType<NetworkManager>().playerPrefab = Player1;
            //CmdPlayer1Spawn();
            James.SetActive(true);
            Bryce.SetActive(false);
            Alex.SetActive(false);
        }
    }

    public void onCharacter2Button()
    {
        //if (isLocalPlayer)
        {
            FindObjectOfType<NetworkManager>().playerPrefab = Player2;
            //CmdPlayer2Spawn();
            James.SetActive(false);
            Bryce.SetActive(true);
            Alex.SetActive(false);
        }
    }

    public void onCharacter3Button()
    {
       // if (isLocalPlayer)
        {
            James.SetActive(false);
            Bryce.SetActive(false);
            Alex.SetActive(true);
            FindObjectOfType<NetworkManager>().playerPrefab = Player3;
            //CmdPlayer3Spawn();
        }
    }

    /*
    [Command]

    void CmdPlayer1Spawn()
    {
        RpcPlayer1Spawn();
    }

    [ClientRpc]

    void RpcPlayer1Spawn()
    {
        FindObjectOfType<NetworkManager>().playerPrefab = Player1;
    }

    [Command]

    void CmdPlayer2Spawn()
    {
        RpcPlayer2Spawn();
    }

    [ClientRpc]

    void RpcPlayer2Spawn()
    {
        FindObjectOfType<NetworkManager>().playerPrefab = Player2;
    }

    [Command]

    void CmdPlayer3Spawn()
    {
        RpcPlayer3Spawn();
    }

    [ClientRpc]

    void RpcPlayer3Spawn()
    {
        FindObjectOfType<NetworkManager>().playerPrefab = Player3;
    }
    */
}
