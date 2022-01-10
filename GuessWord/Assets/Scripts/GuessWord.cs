using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

using System.Collections;
using System.Collections.Generic;
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
        SetUpInputField();

        WordGridObject = new WordGrid(m_LetterBox, m_GameGrid, m_CanvasObject, m_WordSize, m_NumTries, m_GridMargin, m_CellPadding);
        WordGridObject.Run();
    }

    void Update()
    {

    }

    internal void SetUpInputField()
    {
        m_InputField = GameObject.Find("InputGuessTextField").GetComponent<TMP_InputField>();
        m_InputField.enabld = true;
        m_InputField.onEndEdit.AddListener(MakeGuess);
    }

    internal void DeactivateInputField()
    {
        m_InputField.onEndEdit.RemoveAllListeners();
        m_InputField.enabled = false;
    }
    internal void MakeGuess(string word)
    {
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
        }
    }

    internal void OnWin()
    {
        Debug.Log($"Congratulations!  You've guessed the solution, {WordGridObject.m_Solution}");
        DeactivateInputField();
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
