using UnityEngine;
using UnityEngine.SceneManagement;

public class GameFlowManager : MonoBehaviour
{
    public static GameFlowManager Instance;

    private int currentLevel = 0; // track which level player is on

    void Awake()
    {
        // Singleton pattern so it survives across scenes
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start Game from Main Menu → goes to intro cutscene
    public void StartGame()
    {
        currentLevel = 0;
        SceneManager.LoadScene("Cutscene_Intro");
    }

    // After intro → start level 1
    public void StartLevel(int level)
    {
        currentLevel = level;
        SceneManager.LoadScene("Level" + level + "_Forest");
    }

    // When a level ends → go to its end cutscene
    public void EndLevel()
    {
        SceneManager.LoadScene("Cutscene_Level" + currentLevel + "End");
    }

    // After end cutscene → go to next level intro OR ending
    public void NextLevel()
    {
        currentLevel++;
        if (currentLevel <= 3)
        {
            SceneManager.LoadScene("Cutscene_Level" + currentLevel + "Intro");
        }
        else
        {
            SceneManager.LoadScene("Cutscene_Ending");
        }
    }
}
