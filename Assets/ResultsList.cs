using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ResultsList : MonoBehaviour
{
    [SerializeField]
    TMP_Text resultsText;
    
    [SerializeField]
    TMP_Text diffilcutyText;

    // Start is called before the first frame update
    void Start()
    {
        string results = "";

        for (int i = 1; i <= GameManager.instance.levels.Count; i++)
        {
            Level level = GameManager.instance.levels[i];
            results += "Level " + level.Order + ": ";

            if (level.DelayScore > 0)
            {
                results += level.DelayScore.ToString("F1") + " " + level.Units;
            }
            else
            {
                results += "Incomplete";
            }

            results += "\n";
        }

        resultsText.text = results;

        diffilcutyText.text = "Difficulty: " + GameManager.DifficultyMultiplier;
        ResetForNextPlay();
    }

    private static void ResetForNextPlay()
    {
        GameManager.DifficultyMultiplier++;
        for (int i = 1; i <= GameManager.instance.levels.Count; i++)
        {
            GameManager.instance.levels[i].DelayScore = 0f;
        }


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
