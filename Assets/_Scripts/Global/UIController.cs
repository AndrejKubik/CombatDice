using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    #region Singleton
    public static UIController instance;

    private void Awake()
    {
        instance = this;
    }
    #endregion

    public static bool FirstLoad = true;

    public GameObject StartMenu;
    public GameObject WinMenu;
    public GameObject LoseMenu;

    private void Start()
    {
        if(FirstLoad)
        {
            ShowStartMenu();
            FirstLoad = false;
        }
    }

    public void ShowStartMenu()
    {
        StartMenu.SetActive(true);
        Time.timeScale = 0f;
    }

    public void StartGame()
    {
        Time.timeScale = 1f;
        StartMenu.SetActive(false);
    }

    public void ShowWinMenu()
    {
        WinMenu.SetActive(true);
        Time.timeScale = 0f;
    }

    public void LoadNextLevel()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0) SceneManager.LoadScene(1);
        else SceneManager.LoadScene(0);
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ShowLoseMenu()
    {
        LoseMenu.SetActive(false);
        Time.timeScale = 0f;
    }
}
