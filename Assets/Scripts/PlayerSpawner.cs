using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    public GameObject Player;
    public GameObject Panel;
    // Start is called before the first frame update
    void Start()
    {
        Panel.SetActive(false);
        Player.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
