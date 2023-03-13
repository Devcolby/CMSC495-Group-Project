using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossManager : MonoBehaviour
{
    [SerializeField] Enemy[] enemies;
    [SerializeField] int enemiesAlive = 4;

    [SerializeField] GameObject projectile;

    GameManager gameManager;
    bool paused = false;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();

        foreach(Enemy enemy in enemies)
        {
            enemy.Killed += EnemyKilled;
        }

        InvokeRepeating(nameof(FireProjectile), 0, 2f);
    }

    private void Update()
    {
        if (!paused)
            transform.position += Vector3.down * 0.1f * Time.deltaTime;
    }

    void EnemyKilled()
    {
        enemiesAlive--;

        if(enemiesAlive <= 0)
        {
            //Game win
            gameManager.WinGame();
        }
    }

    void FireProjectile()
    {
        foreach(Enemy enemy in enemies)
        {
            if (enemy == null)
                continue;

            if(Random.value < 0.5f)
            {
                Instantiate(projectile, enemy.transform.position, Quaternion.identity);
            }    
        }
    }

    public void SetPaused(bool value)
    {
        paused = value;
    }
}
