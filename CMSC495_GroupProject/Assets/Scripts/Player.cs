using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] GameManager gameManager;

    [SerializeField] Projectile projectilePrefab;
    bool projectileActive = false;
    float horizontal;
    bool paused = false;

    private void Update()
    {
        if (!paused)
        {
            horizontal = Input.GetAxisRaw("Horizontal");

            if (horizontal >= 0.01f && transform.position.x <= 15f)
            {
                transform.position += Vector3.right * speed * Time.deltaTime;
            }
            else if (horizontal <= -0.01f && transform.position.x >= -15f)
            {
                transform.position -= Vector3.right * speed * Time.deltaTime;
            }

            if (Input.GetButtonDown("Fire1"))
            {
                FireProjectile();
            }
        }
    }

    void FireProjectile()
    {
        if (!projectileActive)
        {
            Projectile spawnedProjectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            spawnedProjectile.destroyed += DestroyedProjectile;
            projectileActive = true;
        }
    }

    void DestroyedProjectile()
    {
        projectileActive = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("EnemyProjectile"))
        {
            gameManager.LooseLife();
        }
    }

    public void SetPaused(bool value)
    {
        paused = value;
    }
}
