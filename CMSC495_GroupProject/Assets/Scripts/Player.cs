using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] GameManager gameManager;

    [SerializeField] Projectile projectilePrefab;
    [SerializeField] Projectile dualProjectile;
    [SerializeField] GameObject explosionPrefab;
    [SerializeField] GameObject shieldPrefab;


    bool projectileActive = false;
    float horizontal;
    bool paused = false;
    bool poweredUp = false;

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
        LayerMask layer = collision.gameObject.layer;

        if (layer == LayerMask.NameToLayer("EnemyProjectile"))
        {
            gameManager.LooseLife();
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        } 
        else if (layer == LayerMask.NameToLayer("PowerUp") && !poweredUp)
        {
            PowerUp powerUp = collision.gameObject.GetComponent<PowerUp>();
            StartCoroutine(ActivatePowerUpCo(powerUp.GetPowerUpType()));
        }
    }

    public void SetPaused(bool value)
    {
        paused = value;
    }

    IEnumerator ActivatePowerUpCo(PowerUpType powerUpType)
    {
        poweredUp = true;

        GameObject shield = null;
        // Alter player values here
        switch (powerUpType)
        {
            case PowerUpType.Shield:
                // Shield power
                shield = Instantiate(shieldPrefab, transform);
                break;
            case PowerUpType.Dual_Shots:
                // Dual shots power
                break;
        }

        // Wait for 10 seconds
        yield return new WaitForSeconds(10f);

        // Reset player values here
        switch (powerUpType)
        {
            case PowerUpType.Shield:
                Destroy(shield);
                break;
            case PowerUpType.Dual_Shots:

                break;
        }

        poweredUp = false;
    }
}
