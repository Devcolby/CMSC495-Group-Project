using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField] PowerUpType powerUpType;
    [SerializeField] Sprite[] animationSprites;
    [SerializeField] float animationTime = 1.0f;

    [SerializeField] float speed = 2.0f;
    [SerializeField] Vector3 direction;

    [SerializeField] SpriteRenderer spriteRenderer;
    int currentAnimFrame;
    bool paused = false;

    private void Start()
    {
        InvokeRepeating(nameof(AnimateSprite), animationTime, animationTime);
    }

    private void Update()
    {
        if(!paused)
            transform.position += direction * speed * Time.deltaTime;
    }

    private void AnimateSprite()
    {
        currentAnimFrame++;

        if (currentAnimFrame >= animationSprites.Length)
            currentAnimFrame = 0;

        spriteRenderer.sprite = animationSprites[currentAnimFrame];
    }

    public PowerUpType GetPowerUpType()
    {
        return powerUpType;
    }

}
