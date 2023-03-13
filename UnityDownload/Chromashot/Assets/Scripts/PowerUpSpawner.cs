using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSpawner : MonoBehaviour
{
    [SerializeField] GameObject[] powerUps;
    [SerializeField] Player player;

    GameObject spawnedPowerUp;

    Vector3 leftEdge;
    Vector3 rightEdge;

    private void Start()
    {
        leftEdge = Camera.main.ViewportToWorldPoint(Vector3.zero);
        rightEdge = Camera.main.ViewportToWorldPoint(Vector3.right);
    }

    public void SpawnNewPowerUp()
    {
        if (spawnedPowerUp == null)
        {
            int index = Random.Range(0, powerUps.Length);
            Vector3 randPos = transform.position + new Vector3(Random.Range(leftEdge.x + 2, rightEdge.x - 2), 0, 0);
            spawnedPowerUp = Instantiate(powerUps[index], randPos, Quaternion.identity);
            if(player.GetCurrentPowerUps().Contains(spawnedPowerUp.GetComponent<PowerUp>().GetPowerUpType()))
                Destroy(spawnedPowerUp);
        }
    }

    public void SetPowerUpPause(bool value)
    {

    }
}
