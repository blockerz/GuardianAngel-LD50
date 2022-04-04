using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    public static float DifficultyMultiplier = 3f; 

    public Dictionary<int, Level> levels;
    public Level CurrentLevel { get; set; }

    int sceneIndex = 0;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
        
        DontDestroyOnLoad(gameObject);

        Application.targetFrameRate = 60;
        
        levels = new Dictionary<int, Level>();

        //CurrentLevel = new Level(1, "HorizontalLineScene", "seconds", "Moisture", "Help me win a Staring Contest."); 
        //CurrentLevel = new Level(1, "VerticalLineScene", "Minutes", "Obligation", "Keep hitting snooze to delay getting up.");
        //levels.Add(CurrentLevel.Order, CurrentLevel);
        levels.Add(1, new Level(1, "HorizontalLineScene", "seconds", "Moisture", "Help me win a staring contest."));
        levels.Add(2, new Level(2, "VerticalLineScene", "minutes", "Obligation", "Please don't make me wake up.", SoundManager.Sound.ALARM));
        levels.Add(3, new Level(3, "SquareScene", "days", "Tax time", "I don't want to do my taxes.", SoundManager.Sound.COIN));
        levels.Add(4, new Level(4, "CrossScene", "hours", "Cleanliness", "I don't want to do the dishes!"));
        levels.Add(5, new Level(5, "CircleScene", "years", "Youth", "I want to stay young.", SoundManager.Sound.HEART));
        levels.Add(6, new Level(6, "StarScene", "centuries", "Forces", "Delay the collapse of a star.", SoundManager.Sound.STATIC));
        levels.Add(7, new Level(7, "CloudScene", "hours", "Core Temp", "Help me prevent a reactor meltdown.", SoundManager.Sound.SIREN));


    }

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("GameOverScene");
            CurrentLevel = null;
            sceneIndex = 0;
        }
    }

    public void NextScene()
    {
        int index = sceneIndex + 1;

        if (index <= levels.Count)
        {
            CurrentLevel = levels[index];
            sceneIndex = index;
        }
        else
        {
            SceneManager.LoadScene("GameOverScene");
            CurrentLevel = null;
            sceneIndex = 0;
        }

        if (CurrentLevel != null)
        {
            SceneManager.LoadScene(CurrentLevel.SceneName);
        }
    }
    
    public void ReloadCurrentScene()
    {
        if (CurrentLevel != null)
        {
            SceneManager.LoadScene(CurrentLevel.SceneName);
        }
    }
}
