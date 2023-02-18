using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] int lives = 3;
    [SerializeField] int wave = 0;
    [SerializeField] int points = 0;

    [SerializeField] UIManager uiManager;
    [SerializeField] EnemyGrid enemyGrid;
    [SerializeField] Player player;

    List<GameObject> lifeSprites;

    bool changingWave = false;
    bool paused = false;

    private void Start()
    {
        uiManager.SetLives(lives);
        NewWave();
    }

    private void Update()
    {
        if(Input.GetButtonDown("Pause"))
        {
            if(paused)
            {
                UnpauseGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void NewWave()
    {
        if(!changingWave)
            StartCoroutine(NewWaveCo());
    }

    IEnumerator NewWaveCo()
    {
        changingWave = true;
        wave++;
        uiManager.ShowWave(wave);
        EvaluateWave(wave);

        yield return new WaitForSeconds(2f);

        enemyGrid.SpawnEnemies();
        uiManager.HideWave();
        changingWave = false;
    }

    void EvaluateWave(int wave)
    {
        switch(wave)
        {
            case 2:
                enemyGrid.AddColumns(4);
                break;
            case 3:
                enemyGrid.AddRow(1);
                enemyGrid.IncreaseBooster(0.25f);
                break;
            case 4:
                enemyGrid.AddColumns(2);
                enemyGrid.IncreaseBooster(0.25f);
                break;
            case 5:
                enemyGrid.AddColumns(2);
                enemyGrid.IncreaseBooster(0.25f);
                break;
            case 6:
                enemyGrid.IncreaseBooster(0.25f);
                break;
            case 7:
                enemyGrid.AddRow(1);
                enemyGrid.AddColumns(-1);
                break;
            case 8:
                enemyGrid.AddRow(1);
                enemyGrid.SetRandomizeEnemies(true);
                break;
            case 9:
                enemyGrid.AddColumns(4);
                enemyGrid.IncreaseBooster(0.25f);
                break;
            case 10:
                // Final wave maybe boss?
                break;
        }
    }

    public void AddPoints(int points)
    {
        this.points += points;
        uiManager.SetPoints(this.points);
    }

    public void LooseLife()
    {
        StartCoroutine(LooseLifeCo());
    }

    IEnumerator LooseLifeCo()
    {
        lives--;
        player.gameObject.SetActive(false);
        enemyGrid.SetPaused(true);

        yield return new WaitForSeconds(2f);

        uiManager.RemoveLife();

        if (lives <= 0)
        {
            //Game over
            uiManager.ShowGameOver();
        }
        else
        {
            player.gameObject.SetActive(true);
            enemyGrid.SetPaused(false);
        }
    }

    public void StartNewGame()
    {
        uiManager.HideGameOver();
        lives = 3;
        wave = 0;
        points = 0;
        uiManager.SetLives(lives);
        player.gameObject.SetActive(true);
        enemyGrid.SpawnEnemies();
    }

    public void Continue()
    {
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    void PauseGame()
    {
        paused = true;
        enemyGrid.SetPaused(true);
        player.SetPaused(true);
        uiManager.ShowPauseMenu();
    }

    void UnpauseGame()
    {
        paused = false;
        enemyGrid.SetPaused(false);
        player.SetPaused(false);
        uiManager.HidePauseMenu();
    }

    public void GameOver()
    {
        player.gameObject.SetActive(false);
        enemyGrid.SetPaused(true);
        uiManager.ShowGameOver();
    }
}
