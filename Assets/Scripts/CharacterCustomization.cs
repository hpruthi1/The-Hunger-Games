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
    private int SelectedPlayer;

    private void Awake()
    {
        
            
    }

    public void SelectCharacter(CharacterSelection selection)
    {
        _playerInstance.PlayerSpawner.TrySpawn(selection.CharacterType);
    }

    private void Start()
    {
        {
            James.SetActive(true);
            Bryce.SetActive(false);
            Alex.SetActive(false);
        }
    }

    public void onCharacter1Button()
    {
        {
            SelectedPlayer = 1;
            FindObjectOfType<NetworkManager>().playerPrefab = FindObjectOfType<NetworkManager>().spawnPrefabs[0];
            James.SetActive(true);
            Bryce.SetActive(false);
            Alex.SetActive(false);
        }
    }

    public void onCharacter2Button()
    {
        {
            SelectedPlayer = 2;
            FindObjectOfType<NetworkManager>().playerPrefab = FindObjectOfType<NetworkManager>().spawnPrefabs[1];
            James.SetActive(false);
            Bryce.SetActive(true);
            Alex.SetActive(false);
        }
    }

    public void onCharacter3Button()
    {
        {
            SelectedPlayer = 3;
            James.SetActive(false);
            Bryce.SetActive(false);
            Alex.SetActive(true);
            FindObjectOfType<NetworkManager>().playerPrefab = FindObjectOfType<NetworkManager>().spawnPrefabs[2];
        }
    }
}
