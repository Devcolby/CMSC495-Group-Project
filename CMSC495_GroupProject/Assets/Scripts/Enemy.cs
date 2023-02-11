using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] Sprite[] animationSprites;
    [SerializeField] float animationTime = 1.0f;

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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Projectile"))
        {
            Killed.Invoke();
            Destroy(gameObject);
        }
    }
}
