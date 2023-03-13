using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float boostedSpeed;
    [SerializeField] GameManager gameManager;

    [SerializeField] Projectile projectilePrefab;
    [SerializeField] GameObject explosionPrefab;

    [SerializeField] Projectile dualProjectile;
    [SerializeField] GameObject shieldPrefab;
    [SerializeField] bool dualShot = false;
    [SerializeField] bool rapidFire = false;

    [SerializeField] AudioSource powerUpAudio;
    [SerializeField] AudioClip shieldClip;
    [SerializeField] AudioClip rapidFireClip;

    [SerializeField] List<PowerUpType> activePowerUps = new List<PowerUpType>();

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
            if (dualShot)
            {
                Projectile spawnedProjectile = Instantiate(dualProjectile, transform.position, Quaternion.identity);
            }
            else
            {
                Projectile spawnedProjectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
                spawnedProjectile.destroyed += DestroyedProjectile;
                projectileActive = true;
            }
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
            gameManager.LoseLife();

            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        } 
        else if (layer == LayerMask.NameToLayer("PowerUp"))
        {
            PowerUp powerUp = collision.gameObject.GetComponent<PowerUp>();

            if(!activePowerUps.Contains(powerUp.GetPowerUpType()))
                StartCoroutine(ActivatePowerUpCo(powerUp.GetPowerUpType()));

            Destroy(powerUp.gameObject);
        }
    }

    public void SetPaused(bool value)
    {
        paused = value;
    }

    IEnumerator ActivatePowerUpCo(PowerUpType powerUpType)
    {
        GameObject shield = null;
        // Alter player values here
        switch (powerUpType)
        {
            case PowerUpType.Shield:
                // Shield power
                activePowerUps.Add(PowerUpType.Shield);
                shield = Instantiate(shieldPrefab, transform);
                powerUpAudio.clip = shieldClip;
                powerUpAudio.Play();
                break;
            case PowerUpType.Dual_Shots:
                // Dual shots power
                activePowerUps.Add(PowerUpType.Dual_Shots);
                dualShot = true;
                powerUpAudio.clip = rapidFireClip;
                powerUpAudio.Play();
                break;
        }

        // Wait for 10 seconds
        yield return new WaitForSeconds(10f);

        // Reset player values here
        switch (powerUpType)
        {
            case PowerUpType.Shield:
                if (activePowerUps.Contains(PowerUpType.Shield))
                    activePowerUps.Remove(PowerUpType.Shield);
                Destroy(shield);
                break;
            case PowerUpType.Dual_Shots:
                if (activePowerUps.Contains(PowerUpType.Dual_Shots))
                    activePowerUps.Remove(PowerUpType.Dual_Shots);
                dualShot = false;
                break;
        }
    }

    public List<PowerUpType> GetCurrentPowerUps()
    {
        return activePowerUps;
    }
}
