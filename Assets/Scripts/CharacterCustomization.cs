using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class CharacterCustomization : MonoBehaviour
{
    public Audiomanager audiomanager = null;
    public GameObject Alex;
    public GameObject Bryce;
    public GameObject James;
    private void Start()
    {
        audiomanager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<Audiomanager>();
        James.SetActive(true);
        Bryce.SetActive(false);
        Alex.SetActive(false);
    }

    public void onCharacter1Button()
    {
        FindObjectOfType<Audiomanager>().Play("Click");
        FindObjectOfType<NetworkManager>().playerPrefab = FindObjectOfType<NetworkManager>().spawnPrefabs[0];
        James.SetActive(true);
        Bryce.SetActive(false);
        Alex.SetActive(false);
    }

    public void onCharacter2Button()
    {
        FindObjectOfType<Audiomanager>().Play("Click");
        FindObjectOfType<NetworkManager>().playerPrefab = FindObjectOfType<NetworkManager>().spawnPrefabs[1];
        James.SetActive(false);
        Bryce.SetActive(true);
        Alex.SetActive(false);
    }

    public void onCharacter3Button()
    {
        FindObjectOfType<Audiomanager>().Play("Click");
        James.SetActive(false);
        Bryce.SetActive(false);
        Alex.SetActive(true);
        FindObjectOfType<NetworkManager>().playerPrefab = FindObjectOfType<NetworkManager>().spawnPrefabs[2];
    }
}
