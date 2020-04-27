using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class CharacterCustomization : MonoBehaviour
{
    public GameObject Alex;
    public GameObject Bryce;
    public GameObject James;
    private int SelectedPlayer;

    private void Awake()
    {
        
            
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
