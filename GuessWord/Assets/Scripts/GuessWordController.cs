using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

using UnityEditor;
using UnityEngine.Accessibility;
using UnityEngine.UI;


public class GuessWordController : MonoBehaviour
{
    [Header("Game Objects")] // Assign in inspector
    [SerializeField]
    internal GameObject m_LetterBoxPrefab;

    [SerializeField]
    internal GameObject m_KeyPrefab;

    [SerializeField]
    internal GameObject m_GameGridQuad;

    [SerializeField]
    internal GameObject m_KeyboardQuad;

    [SerializeField]
    internal Canvas m_CanvasObject;

    [SerializeField]
    internal Button m_Button;

    internal TMP_InputField m_InputField;

    [Header("Game Dimensions")] [SerializeField] [Range(4, 8)]
    internal int m_WordSize = 5; // number of letters per word

    [SerializeField]
    [Range(3, 12)]
    internal int m_NumTries = 6; // Number of guesses allowed per round

    [Header("Grid Settings")]
    [SerializeField]
    [Range(0.0f, 1.5f)]
    internal float m_GridMargin = 0; // margin around grid, equal padding along top/bottom/sides

    [SerializeField]
    [Range(0.0f, 1.5f)]
    internal float m_CellPadding = 0; // padding between grid cells

    internal GuessWordGame GuessWordGameObject;

    internal bool m_Won;

    internal MessageBox m_MessageBox;
    internal ReferenceWordDictionary m_Dictionary;

    // Start is called before the first frame update
    void Start()
    {
        PrintGridInfo();
        SetUpDictionary();
        SetUpMessageBox();
        SetUpInputField();
        StartNewGame();
    }

    void Update()
    {

    }

    internal void SetUpDictionary()
    {
        m_Dictionary = new ReferenceWordDictionary();
    }
    internal void StartNewGame()
    {
        ActivateInputField();
        GuessWordGameObject = new GuessWordGame(m_LetterBoxPrefab, m_KeyPrefab,  m_GameGridQuad, m_KeyboardQuad, m_CanvasObject, m_WordSize, m_NumTries, m_GridMargin, m_CellPadding);
        GuessWordGameObject.Run();
    }

    internal void SetUpInputField()
    {
        m_InputField = GameObject.Find("InputGuessTextField").GetComponent<TMP_InputField>();
        // m_InputField.transform.position -= new Vector3(250.0f, 700.0f); // for higher res
        // m_InputField.transform.localScale *= 3.5f; // for higher res
    }

    internal void ActivateInputField()
    {
        m_InputField.onEndEdit.AddListener(MakeGuess);
        m_InputField.gameObject.SetActive(true);
    }

    internal void DeactivateInputField()
    {
        m_InputField.onEndEdit.RemoveAllListeners();
        m_InputField.gameObject.SetActive(false);
    }
    internal void MakeGuess(string word)
    {
        word = word.ToUpper();
        if (!IsValid(word))
        {
            Debug.Log($"Word {word} is not valid; please try again.");
            return;
        }

        // submit the guess
        m_Won = GuessWordGameObject.GuessWord(word);

        // clear the text field
        m_InputField.text = "";

        if (m_Won)
        {
            OnWin();
            return;
        }

        if (GuessWordGameObject.m_GuessCount >= m_NumTries)
        {
            OnLose();
            return;
        }
    }

    internal void SetUpMessageBox()
    {
        m_MessageBox = new MessageBox(m_GameGridQuad, m_CanvasObject, m_Button);
    }

    internal void ShowMessageBox(string message)
    {
        m_MessageBox.Show(message);
        m_MessageBox.m_YesButton.GetComponent<Button>().onClick.AddListener(Restart);
        m_MessageBox.m_NoButton.GetComponent<Button>().onClick.AddListener(Exit);
    }

    internal void Restart()
    {
        CleanUpOldGame();
        StartNewGame();
    }

    internal void CleanUpOldGame()
    {
        m_MessageBox.Destroy();
        GuessWordGameObject.Destroy();
    }

    internal void Exit()
    {
        CleanUpOldGame();
        Application.Quit();
    }

    internal void OnWin()
    {
        string message = $"Congratulations!\n\n The solution was {GuessWordGameObject.m_Solution}";
        DeactivateInputField();
        ShowMessageBox(message + "\n\nPlay again?\n");
    }

    internal void OnLose()
    {
        string message = $"Game over!  The solution was {GuessWordGameObject.m_Solution}";
        DeactivateInputField();
        ShowMessageBox(message + "\n\nPlay again?\n");
    }

    internal bool IsValid(string word)
    {
        if (word.Length != m_WordSize)
        {
            Debug.Log($"Word needs to be {m_WordSize} letters.  Please try again.");
            return false;
        }

        if (!m_Dictionary.Contains(word.ToLower()))
        {
            return false;
        }

        return true;
    }

    void DisableCanvas()
    {
        m_CanvasObject.enabled = false;
    }

    void PrintGridInfo()
    {
        Debug.Log($"Game: {m_WordSize} word length x {m_NumTries} guesses");
    }
}
