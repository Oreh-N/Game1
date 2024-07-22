using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenu : MonoBehaviour
{
    public bool pauseGame;
    public GameObject pauseGameMenu;


    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pauseGame)
            { Resume(); }
            else { Pause(); }
        }
    }

    public void Pause()
    { Pause(pauseGameMenu); }

    public void Resume()
    { Resume(pauseGameMenu); }

    public void Pause(GameObject gameObject)
    {
        gameObject.SetActive(true);
        Time.timeScale = 0f;
        pauseGame = true;
    }

    public void Resume(GameObject gameObject)
    {
        gameObject.SetActive(false);
        Time.timeScale = 1f;
        pauseGame = false;
    }

    public void LoadMenuScene()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }
}
