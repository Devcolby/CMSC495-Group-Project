using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] Transform livesParent;
    [SerializeField] GameObject lifeSpritePrefab;

    [SerializeField] Text waveText;
    [SerializeField] Text pointsText;
    [SerializeField] GameObject gameOverObj;
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject winMenu;

    List<GameObject> lifeSprites = new List<GameObject>();

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void SetPoints(int points)
    {
        pointsText.text = points.ToString();
        pointsText.enabled = true;
    }

    public void ShowWave(int wave)
    {
        if (wave == 10)
        {
            waveText.text = "Final Wave";
        }
        else
        {
            waveText.text = "Wave " + wave.ToString();
        }

        waveText.enabled = true;
    }

    public void HideWave()
    {
        waveText.enabled = false;
    }

    public void SetLives(int lives)
    {
        for(int i = 0; i < lives - 1; i++)
        {
            GameObject spawnedSprite = Instantiate(lifeSpritePrefab, livesParent);
            lifeSprites.Add(spawnedSprite);
        }
    }

    public void RemoveLife()
    {
        if (lifeSprites.Count > 0)
        {
            Destroy(lifeSprites[lifeSprites.Count - 1].gameObject);
            lifeSprites.RemoveAt(lifeSprites.Count - 1);
        }
    }

    public void ShowGameOver()
    {
        gameOverObj.SetActive(true);
        Cursor.lockState = CursorLockMode.Confined;
    }

    public void HideGameOver()
    {
        gameOverObj.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void ShowPauseMenu()
    {
        pauseMenu.SetActive(true);
        Cursor.lockState = CursorLockMode.Confined;
    }

    public void HidePauseMenu()
    {
        pauseMenu.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void ShowWinGameMenu()
    {
        winMenu.SetActive(true);
        Cursor.lockState = CursorLockMode.Confined;
    }
}
