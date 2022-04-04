using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenarioAnimator : MonoBehaviour
{
    [SerializeField]
    List<Texture2D> normalAnimation;

    [SerializeField]
    float normalSpeed = 0.5f;
    
    [SerializeField]
    List<Texture2D> damageAnimation;

    [SerializeField]
    float damageSpeed = 0.1f;

    [SerializeField]
    List<Texture2D> deathAnimation;

    [SerializeField]
    float deathSpeed = 0.5f;

    PlayerHealth player;
    bool animationChanged = false;
    List<Texture2D> activeAnimation;
    float activeAnimationSpeed;
    SpriteRenderer render;
    int animationIndex = 0;
    float frameTime = 0;
    float damageTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        render = GetComponent<SpriteRenderer>();

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
        player.OnPlayerHealthDecreased += PlayDamageAnimation;
        player.OnPlayerDied += PlayDeathAnimation;

        PlayNormalAnimation();

    }

    private void PlayNormalAnimation()
    {
        PlayAnimation(normalAnimation, normalSpeed);
    }
    
    private void PlayDamageAnimation()
    {
        PlayAnimation(damageAnimation, damageSpeed);
        damageTime = damageAnimation.Count * damageSpeed;
    }

    private void PlayDeathAnimation()
    {
        damageTime = 0f;
        PlayAnimation(deathAnimation, deathSpeed);
    }


    private void PlayAnimation(List<Texture2D> animation, float speed)
    {
        activeAnimation = animation;
        activeAnimationSpeed = speed;
        animationChanged = true;
    }

    // Update is called once per frame
    void Update()
    {
        frameTime += Time.deltaTime;

        if (damageTime > 0)
        {
            damageTime -= Time.deltaTime;

            if (damageTime <= 0)
            {
                PlayNormalAnimation();
            }
        }

        if (animationChanged)
        {
            animationIndex = 0;
            animationChanged = false;
            frameTime = 0f;
            UpdateActiveSprite();
        }

        if (frameTime > activeAnimationSpeed)
        {
            frameTime = 0f;

            animationIndex++;

            if (animationIndex >= activeAnimation.Count)
            {
                animationIndex = 0;
            }
            UpdateActiveSprite();
        }
    }

    private void UpdateActiveSprite()
    {
        Sprite sprite = Sprite.Create(activeAnimation[animationIndex], 
                                        new Rect(0f, 0f, activeAnimation[animationIndex].width, activeAnimation[animationIndex].height), 
                                        new Vector2(0.5f, 0.5f));
        if (sprite != null)
        {
            render.sprite = sprite;
        }
    }
}
