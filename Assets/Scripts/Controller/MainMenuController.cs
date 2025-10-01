using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private Button startButton;
    [SerializeField] private Button optionsButton;
    [SerializeField] private Button exitButton;

    [Header("Audio")]
    [SerializeField] private AudioSource musicSource; // assign background music

    void Start()
    {
        // Hook up buttons
        if (startButton != null) startButton.onClick.AddListener(OnStartGame);
        if (optionsButton != null) optionsButton.onClick.AddListener(OnOptions);
        if (exitButton != null) exitButton.onClick.AddListener(OnExit);

        // Play music if not already playing
        if (musicSource != null && !musicSource.isPlaying)
            musicSource.Play();
    }

    void OnStartGame()
    {
        Debug.Log("Start Game pressed");

        // Use GameFlowManager if it exists
        if (GameFlowManager.Instance != null)
            GameFlowManager.Instance.StartGame();
        else
            SceneManager.LoadScene("Cutscene_Intro"); // fallback
    }

    void OnOptions()
    {
        Debug.Log("Options pressed");
        // TODO: Show options menu (volume, graphics, controls)
        // For now: just log
    }

    void OnExit()
    {
        Debug.Log("Exit pressed");

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
