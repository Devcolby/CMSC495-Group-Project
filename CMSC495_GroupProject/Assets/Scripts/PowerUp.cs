using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField] PowerUpType powerUpType;
    [SerializeField] Sprite[] animationSprites;
    [SerializeField] float animationTime = 1.0f;

    SpriteRenderer spriteRenderer;
    int currentAnimFrame;

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
