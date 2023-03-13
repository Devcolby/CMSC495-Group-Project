using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverVolume : MonoBehaviour
{
    [SerializeField] GameManager gameManager;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            gameManager.GameOver();
        }
    }
}
