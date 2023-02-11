using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGrid : MonoBehaviour
{
    [SerializeField] Enemy[] enemyPrefabs;
    [SerializeField] int rows = 5;
    [SerializeField] int columns = 11;

    [SerializeField] int spacing = 2;
    [SerializeField] float booster = 0.0f;
    [SerializeField] Vector3 startingPosition = Vector3.zero;
    [SerializeField] AnimationCurve speed;

    [SerializeField] float missileRate = 1.0f;
    [SerializeField] Projectile missilePrefab;
    [SerializeField] GameManager gameManager;


    int startingRows;
    int startingColumns;
    float startingMissileRate;

    public int enemiesKilled { get; private set; }
    public int enemiesAlive => totalEnemies - enemiesKilled;
    public int totalEnemies => rows * columns;
    public float percentKilled => (float)enemiesKilled / totalEnemies;

    bool paused = false;
    Vector3 direction = Vector2.right;

    private void Start()
    {
        InvokeRepeating(nameof(FireMissile), missileRate, missileRate);
    }

    private void Update()
    {
        if (!paused)
        {
            transform.position += direction * (speed.Evaluate(percentKilled) + booster) * Time.deltaTime;

            Vector3 leftEdge = Camera.main.ViewportToWorldPoint(Vector3.zero);
            Vector3 rightEdge = Camera.main.ViewportToWorldPoint(Vector3.right);

            foreach (Transform enemy in transform)
            {
                if (!enemy.gameObject.activeInHierarchy)
                    continue;

                if (direction == Vector3.right && enemy.position.x >= (rightEdge.x - 1))
                {
                    MoveDown();
                }
                else if (direction == Vector3.left && enemy.position.x <= (leftEdge.x + 1))
                {
                    MoveDown();
                }
            }
        }
    }

    void MoveDown()
    {
        direction.x *= -1;

        Vector3 position = transform.position;
        position.y -= 1;
        transform.position = position;
    }

    void EnemyKilled()
    {
        gameManager.AddPoints(200);
        enemiesKilled++;
        if (enemiesKilled >= totalEnemies)
        {
            enemiesKilled = 0;
            booster = 0.0f;
            transform.localPosition = startingPosition;
            gameManager.NewWave();
        }
    }

    public void SpawnEnemies()
    {
        for (int x = 0; x < rows; x++)
        {
            float width = spacing * (columns - 1);
            float height = spacing * (rows - 1);
            Vector2 center = new Vector2(-width / 2, -height / 2);
            Vector3 rowPosition = new Vector3(center.x, center.y + (x * spacing), 0);

            for (int y = 0; y < columns; y++)
            {
                if (x >= enemyPrefabs.Length)
                {
                    Enemy enemy = Instantiate(enemyPrefabs[enemyPrefabs.Length - 1], transform);
                    continue;
                }
                else
                {
                    Enemy enemy = Instantiate(enemyPrefabs[x], transform);
                    enemy.Killed += EnemyKilled;
                    Vector3 position = rowPosition;
                    position.x += y * spacing;
                    enemy.transform.localPosition = position;
                }
            }
        }
    }

    void FireMissile()
    {
        if (!paused)
        {
            foreach (Transform enemy in transform)
            {
                if (!enemy.gameObject.activeInHierarchy)
                    continue;

                if (Random.value < (1.0f / (float)enemiesAlive))
                {

                    Instantiate(missilePrefab, enemy.position, Quaternion.identity);
                    break;
                }
            }
        }
    }

    public void SetSize(int rows, int columns)
    {
        this.rows = rows;
        this.columns = columns;
    }

    public void AddRow(int amount)
    {
        rows += amount;
    }
    
    public void AddColumns(int amount)
    {
        columns += amount;
    }

    public void IncreaseBooster(float amount)
    {
        booster += amount;
    }

    public void SetPaused(bool value)
    {
        paused = value;
    }

    public void ResetValues()
    {
        rows = startingRows;
        columns = startingColumns;
        missileRate = startingMissileRate;
        booster = 0.0f;
    }

}
