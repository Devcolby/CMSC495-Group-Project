using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] int health = 1;

    [SerializeField] Sprite[] animationSprites;
    [SerializeField] float animationTime = 1.0f;

    [SerializeField] Projectile projectilePrefab;
    [SerializeField] GameObject explosionPrefab;

    public System.Action Killed;

    SpriteRenderer spriteRenderer;
    int currentAnimFrame;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        InvokeRepeating(nameof(AnimateSprite), animationTime, animationTime);
    }

    private void AnimateSprite()
    {
        currentAnimFrame++;

        if (currentAnimFrame >= animationSprites.Length)
            currentAnimFrame = 0;

        spriteRenderer.sprite = animationSprites[currentAnimFrame];
    }

    public void FireProjectile()
    {
        Instantiate(projectilePrefab, transform.position, Quaternion.identity);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Projectile"))
        {
            health--;

            if (health <= 0)
            {
                Instantiate(explosionPrefab, transform.position, Quaternion.identity);
                Killed.Invoke();
                Destroy(gameObject);
            }
        }
    }
}
