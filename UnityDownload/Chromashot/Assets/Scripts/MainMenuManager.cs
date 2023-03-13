using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject gameRules;
    [SerializeField] GameObject about;
    [SerializeField] GameObject controls;

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
        controls.SetActive(false);
        about.SetActive(false);

        mainMenu.SetActive(true);
    }

    public void Controls()
    {
        mainMenu.SetActive(false);
        controls.SetActive(true);
    }

    public void About()
    {
        mainMenu.SetActive(false);
        about.SetActive(true);
    }
}
