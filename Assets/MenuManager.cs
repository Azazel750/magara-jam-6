using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject optionsPanel, quitPanel;

    public bool gameOpened = false;

    private void Awake()
    {
        gameOpened = false;
    }

    public void PlayGame()
    {
        gameOpened = true;
    }
    
    public void OpenOptionsPanel()
    {
        optionsPanel.SetActive(true);   
    }
    
    public void OpenQuitPanel()
    {
        quitPanel.SetActive(true);   
    }
    
    public void QuitGame()
    {
        Application.Quit();
    }
}
