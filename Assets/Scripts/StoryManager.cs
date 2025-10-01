using UnityEngine;
using System.IO;
using TMPro;
using UnityEngine.SceneManagement; // ✅ Needed for scene loading

public class StoryManager : MonoBehaviour
{
    public TextMeshProUGUI storyText; // UI Text for displaying dialogue
    private StoryData story;
    private int currentLine = 0;
    private string[] currentDialogues;

    [Header("Cutscene Settings")]
    public bool isIntroCutscene = false;
    public bool isLevelIntro = false;
    public bool isLevelEnding = false;
    public bool isEnding = false;
    public int levelId = 1; // which level this cutscene belongs to

    void Start()
    {
        LoadStory();

        if (isIntroCutscene) LoadIntro();
        else if (isLevelIntro) LoadLevelIntro(levelId);
        else if (isLevelEnding) LoadLevelEnding(levelId);
        else if (isEnding) LoadEnding();

        ShowNextLine();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ShowNextLine();
        }
    }

    void LoadStory()
    {
        string path = Path.Combine(Application.streamingAssetsPath, "story.json");
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            story = JsonUtility.FromJson<StoryData>(json);
        }
        else
        {
            Debug.LogError("Story JSON not found at: " + path);
        }
    }

    public void LoadIntro()
    {
        currentDialogues = story.intro.dialogues;
        currentLine = 0;
    }

    public void LoadLevelIntro(int levelId)
    {
        currentDialogues = story.levels[levelId - 1].introDialogue;
        currentLine = 0;
    }

    public void LoadLevelEnding(int levelId)
    {
        currentDialogues = story.levels[levelId - 1].endDialogue;
        currentLine = 0;
    }

    public void LoadEnding()
    {
        currentDialogues = story.ending.dialogues;
        currentLine = 0;
    }

    void ShowNextLine()
    {
        if (currentDialogues != null && currentLine < currentDialogues.Length)
        {
            storyText.text = currentDialogues[currentLine];
            currentLine++;
        }
        else
        {
            storyText.text = "";

            if (isIntroCutscene)
            {
                StartCoroutine(FadeTransition.Instance.FadeOutAndLoad("Level1_Forest"));
            }
            else if (isLevelIntro)
            {
                StartCoroutine(FadeTransition.Instance.FadeOutAndLoad("Level" + levelId + "_Forest"));
            }
            else if (isLevelEnding)
            {
                if (levelId < story.levels.Length)
                    StartCoroutine(FadeTransition.Instance.FadeOutAndLoad("Cutscene_Level" + (levelId + 1) + "Intro"));
                else
                    StartCoroutine(FadeTransition.Instance.FadeOutAndLoad("Cutscene_Ending"));
            }
            else if (isEnding)
            {
                StartCoroutine(FadeTransition.Instance.FadeOutAndLoad("MainMenu"));
            }
        }

    }
}

[System.Serializable]
public class StoryData
{
    public string gameTitle;
    public Intro intro;
    public Level[] levels;
    public Ending ending;
}

[System.Serializable]
public class Intro
{
    public string scene;
    public string[] dialogues;
}

[System.Serializable]
public class Level
{
    public int id;
    public string name;
    public string[] introDialogue;
    public string goal;
    public string[] endDialogue;
}

[System.Serializable]
public class Ending
{
    public string[] dialogues;
}
