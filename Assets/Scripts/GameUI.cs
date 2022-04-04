using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameUI : MonoBehaviour
{
    [SerializeField]
    Slider healthSlider;
    
    [SerializeField]
    Image sliderFill;

    [SerializeField]
    Gradient healthGradient;

    [SerializeField]
    TMP_Text messageText;

    [SerializeField]
    TMP_Text healthText;

    [SerializeField]
    TMP_Text delayAmountText;
    
    [SerializeField]
    TextMeshProUGUI nextButton;
    
    [SerializeField]
    TextMeshProUGUI tryAgainButton;
        
    GameObject player;
    PlayerHealth playerHealth;
    CourseManager courseManager;
    bool breach = false;
    string units;
    string unitsShort;

    private void Awake()
    {
        if (GameManager.instance.CurrentLevel == null)
        {
            GameManager.instance.CurrentLevel = GameManager.instance.levels[1];
        }
    }

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<PlayerHealth>();

        courseManager = GameObject.FindObjectOfType<CourseManager>();

        playerHealth.OnPlayerHealthDecreased += UpdateHealth;
        playerHealth.OnPlayerDied += CourseCompleted;
        healthSlider.maxValue = playerHealth.GetMaxHealth();
        healthSlider.value = playerHealth.GetMaxHealth();

        //sliderFill = healthSlider.GetComponentInChildren<Image>();
        //sliderFill.color = healthGradient.Evaluate(1f);

        messageText.text = GameManager.instance.CurrentLevel.GoalMessage;
        healthText.text = GameManager.instance.CurrentLevel.HealthText;

        nextButton.rectTransform.parent.gameObject.SetActive(false);
        tryAgainButton.rectTransform.parent.gameObject.SetActive(false);

        units = GameManager.instance.CurrentLevel.Units;
        unitsShort = units.ToLower().Substring(0, 1);

    }

    private void CourseCompleted()
    {
        

        if (breach)
        {
            messageText.text = "You delayed the inevitable for " + courseManager.delayedInevitability.ToString("F1") + " " + units + ".";
            nextButton.rectTransform.parent.gameObject.SetActive(true);
            tryAgainButton.rectTransform.parent.gameObject.SetActive(true);
            GameManager.instance.CurrentLevel.DelayScore = courseManager.delayedInevitability;
        }
        else
        {
            if (GameManager.instance.CurrentLevel.Failures >= 5 ||
                GameManager.instance.CurrentLevel.DelayScore > 0)
            {
                nextButton.rectTransform.parent.gameObject.SetActive(true);
            }

            messageText.text = "You could not delay the inevitable.";
            tryAgainButton.rectTransform.parent.gameObject.SetActive(true);
            GameManager.instance.CurrentLevel.Failures++;
        }
    }

    private void UpdateHealth()
    {
        healthSlider.value = playerHealth.Health;
        //sliderFill.color = healthGradient.Evaluate(healthSlider.value/healthSlider.maxValue);
    }

    void Update()
    {
        if (courseManager.delayedInevitability > 0)
        {
            if (!breach)
            {
                //StartCoroutine(DisplayMessageForTime("Inevitabilty Breach Detected", 2f));
                messageText.text = "Inevitabilty Breach Detected";
                breach = true; 
            }

            delayAmountText.text = courseManager.delayedInevitability.ToString("F1") + unitsShort;
        }
    }

    public void NextButtonClick()
    {
            GameManager.instance.NextScene();         
        
    }
    
    public void TryAgainButtonClick()
    {
        GameManager.instance.ReloadCurrentScene();
    }

    private IEnumerator DisplayMessageForTime(string message, float time)
    {
        messageText.text = message;
        yield return new WaitForSeconds(time);
        messageText.text = "";
    }
    
    //private void DisplayPermanentMessage()
    //{
    //    if (breach)
    //    {
    //        messageText.text = "You delayed the inevitable for " + courseManager.delayedInevitability.ToString("F1") + " seconds.";        
    //    }
    //    else
    //    { 
    //        messageText.text = "You failed to delay the inevitable.";        
    //    }
    //}
}
