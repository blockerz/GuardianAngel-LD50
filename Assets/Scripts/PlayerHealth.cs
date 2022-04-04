using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField]
    int maxHealth = 100;

    public event Action OnPlayerHealthDecreased;
    public event Action OnPlayerDied;

    PlayerHealth()
    {
        Health = maxHealth;
    }

    public int Health { get; private set; }
    
    public int GetMaxHealth()
    {
        return maxHealth;
    }

    public void DecreaseHealth(int amount)
    {
        if (Health <= 0)
        {
            return;
        }
        
        Health -= amount;
        Math.Clamp(Health, 0, maxHealth);
        OnPlayerHealthDecreased?.Invoke();

        if (Health <= 0)
        {
            OnPlayerDied?.Invoke();
        }

        SoundManager.PlaySound(GameManager.instance.CurrentLevel.DamageSound);
    }
}
