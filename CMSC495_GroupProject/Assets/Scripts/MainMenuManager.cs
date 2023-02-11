using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject gameRules;

    public void StartGame()
    {
        mainMenu.SetActive(false);
        SceneManager.LoadScene(1);
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void GameRules()
    {
        mainMenu.SetActive(false);
        gameRules.SetActive(true);
    }

    public void MainMenu()
    {
        gameRules.SetActive(false);
        mainMenu.SetActive(true);
    }
}
