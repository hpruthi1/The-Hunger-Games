﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCustomization : MonoBehaviour
{
    public GameObject Alex;
    public GameObject Bryce;
    public GameObject James;

    private void Start()
    {
        James.SetActive(true);
        Bryce.SetActive(false);
        Alex.SetActive(false);
    }
    public void onCharacter2Button()
    {
        James.SetActive(false);
        Bryce.SetActive(true);
    }

    public void onCharacter3Button()
    {
        James.SetActive(false);
        Bryce.SetActive(false);
        Alex.SetActive(true);
    }

}