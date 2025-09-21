using UnityEngine;
using System.IO;
using TMPro;
public class StoryManager : MonoBehaviour
{
    public TextMeshProUGUI storyText; // UI Text for displaying dialogue
    private StoryData story;
    private int currentLine = 0;
    private string[] currentDialogues;

    void Start()
    {
        LoadStory();
        LoadIntro(); // start with the intro by default
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
            storyText.text = ""; // clear after dialogues end
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