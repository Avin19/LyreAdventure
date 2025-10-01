using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class LoopingTypewriter : MonoBehaviour
{
    public TextMeshProUGUI textUI; // Assign your UI Text (or TextMeshProUGUI if using TMP)
    public string fullText = "Press SPACE to continue...";
    public float typingSpeed = 0.05f;
    public float pauseDuration = 1f; // Wait time after typing/erasing

    void Start()
    {
        StartCoroutine(TypewriterLoop());
    }

    IEnumerator TypewriterLoop()
    {
        while (true) // Infinite loop
        {
            // Type characters one by one
            textUI.text = "";
            foreach (char c in fullText)
            {
                textUI.text += c;
                yield return new WaitForSeconds(typingSpeed);
            }

            // Pause after fully typed
            yield return new WaitForSeconds(pauseDuration);

            // Erase characters one by one
            for (int i = fullText.Length - 1; i >= 0; i--)
            {
                textUI.text = fullText.Substring(0, i);
                yield return new WaitForSeconds(typingSpeed);
            }

            // Pause before repeating
            yield return new WaitForSeconds(pauseDuration);
        }
    }
}
