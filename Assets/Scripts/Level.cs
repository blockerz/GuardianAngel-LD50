using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level 
{
    public string SceneName { get; set; }
    public string Units { get; set; }
    public string HealthText { get; set; }
    public string GoalMessage { get; set; }
    public float DelayScore { get; set; }
    public int Order { get; set; }
    public int Failures { get; set; }
    public SoundManager.Sound DamageSound { get; set; }
    
    public Level(int order, string name, string units, string healthText, string goal, SoundManager.Sound damage = SoundManager.Sound.DAMAGE)
    {
        Order = order;
        SceneName = name;
        DelayScore = 0f;
        Failures = 0;
        Units = units;
        GoalMessage = goal;
        HealthText = healthText;
        DamageSound = damage;
    }

}
