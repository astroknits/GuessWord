using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

using UnityEditor;
using UnityEngine.Accessibility;
using UnityEngine.UI;

public class GuessWord : MonoBehaviour
{
    [Header("Game Objects")]
    [SerializeField]
    internal GameObject m_LetterBox;
    [SerializeField]
    public GameObject m_GameGrid;

    [SerializeField]
    internal Canvas m_CanvasObject; // Assign in inspector

    internal TMP_InputField m_InputField;

    [Header("Game Dimensions")]
    [SerializeField]
    [Range(4, 8)]
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

    internal WordGrid WordGridObject;

    internal bool m_Won;

    // Start is called before the first frame update
    void Start()
    {
        PrintGridInfo();
        StartNewGame();
    }

    void Update()
    {

    }

    internal void StartNewGame()
    {
        SetUpInputField();
        WordGridObject = new WordGrid(m_LetterBox, m_GameGrid, m_CanvasObject, m_WordSize, m_NumTries, m_GridMargin, m_CellPadding);
        WordGridObject.Run();
    }

    internal void SetUpInputField()
    {
        m_InputField = GameObject.Find("InputGuessTextField").GetComponent<TMP_InputField>();
        m_InputField.transform.position -= new Vector3(250.0f, 700.0f); // for higher res
        m_InputField.transform.localScale *= 3.5f; // for higher res
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
        m_Won = WordGridObject.GuessWord(word);

        // clear the text field
        m_InputField.text = "";

        if (m_Won)
        {
            OnWin();
            return;
        }

        if (WordGridObject.m_GuessCount >= m_NumTries)
        {
            OnLose();
            return;
        }
    }

    internal bool Message(string message,
                          float width=3.0f, float height=2.5f,
                          float yOffset=2.5f, float zOffset=-4.0f,
                          int fontSize=3)
    {
        // Set up the modal/dialog box
        GameObject messageBox = GameObject.CreatePrimitive(PrimitiveType.Quad);
        messageBox.transform.position = m_GameGrid.transform.position + new Vector3(0, yOffset, zOffset);
        messageBox.transform.localScale = new Vector3(width, height, 1.0f);

        // Set the text message
        GameObject messageBoxText = new GameObject("Message");
        TextMeshPro text = messageBoxText.AddComponent<TextMeshPro>();
        text.text = message;
        text.color = Color.black;
        text.fontSize = fontSize;
        text.transform.position = messageBox.transform.position;
        text.rectTransform.sizeDelta = new Vector2(width*.9f, height*.9f);



        // GameObject.Destroy(messageBox);
        return true;
    }

    internal void OnWin()
    {
        string message = $"Congratulations!  You've guessed the solution, {WordGridObject.m_Solution}";
        DeactivateInputField();
        Message(message + "\n\nPlay again?\n");
    }

    internal void OnLose()
    {
        string message = $"Game over!  The solution was {WordGridObject.m_Solution}";
        DeactivateInputField();
        Message(message + "\n\nPlay again?\n");
    }

    internal bool IsValid(string word)
    {
        if (word.Length != m_WordSize)
        {
            Debug.Log($"Word needs to be {m_WordSize} letters.  Please try again.");
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
