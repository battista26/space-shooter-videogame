﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadGame()
    {
        // load the game scene
        SceneManager.LoadScene(1); // main game scene
    }

    public void Load2PlayerGame()
    {
        SceneManager.LoadScene(2);
    }
}
