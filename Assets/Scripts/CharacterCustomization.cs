﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class CharacterCustomization : MonoBehaviour
{
    public GameObject Alex;
    public GameObject Bryce;
    public GameObject James;
    public GameObject Player1;
    public GameObject Player2;
    public GameObject Player3;

    private void Start()
    {
        James.SetActive(true);
        Bryce.SetActive(false);
        Alex.SetActive(false);
    }

    public void onCharacter1Button()
    {
        FindObjectOfType<NetworkManager>().playerPrefab = Player1;
        James.SetActive(true);
        Bryce.SetActive(false);
        Alex.SetActive(false);
    }

    public void onCharacter2Button()
    {
        FindObjectOfType<NetworkManager>().playerPrefab = Player2 ;
        James.SetActive(false);
        Bryce.SetActive(true);
        Alex.SetActive(false);
    }

    public void onCharacter3Button()
    {
        James.SetActive(false);
        Bryce.SetActive(false);
        Alex.SetActive(true);
        FindObjectOfType<NetworkManager>().playerPrefab = Player3;
    }

}
