using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;

using UnityEditor;
using UnityEngine.Accessibility;
using UnityEngine.UI;


internal class GuessWordController : MonoBehaviour
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

    internal string m_KeyboardInput;
    internal bool m_Won;

    internal MessageBox m_MessageBox;
    internal ReferenceWordDictionary m_Dictionary;

    internal KeyCode[] m_LetterKeyCodes;
    internal KeyCode[] m_EnterKeyCodes;
    internal KeyCode[] m_DeleteKeyCodes;

    // Start is called before the first frame update
    void Start()
    {
        PrintGridInfo();
        SetUpDictionary();
        SetUpMessageBox();
        SetUpKeyCodeLists();
        StartNewGame();
    }

    void Update()
    {
        ParseKeyboardInput();
    }

    internal void SetUpKeyCodeLists()
    {
        m_LetterKeyCodes = new KeyCode[]
        {
            KeyCode.A, KeyCode.B, KeyCode.C, KeyCode.D, KeyCode.E,
            KeyCode.F, KeyCode.G, KeyCode.H, KeyCode.I, KeyCode.J,
            KeyCode.K, KeyCode.L, KeyCode.M, KeyCode.N, KeyCode.O,
            KeyCode.P, KeyCode.Q, KeyCode.R, KeyCode.S, KeyCode.T,
            KeyCode.U, KeyCode.V, KeyCode.W, KeyCode.X, KeyCode.Y, KeyCode.Z
        };
        m_EnterKeyCodes = new KeyCode[] {KeyCode.Return, KeyCode.KeypadEnter};
        m_DeleteKeyCodes = new KeyCode[] {KeyCode.Delete, KeyCode.Backspace};
    }

    internal void ParseKeyboardInput()
    {
        foreach (KeyCode keyCode in m_LetterKeyCodes)
        {
            if (Input.GetKeyDown(keyCode))
            {
                m_KeyboardInput += keyCode.ToString();
            }
        }

        foreach (KeyCode keyCode in m_DeleteKeyCodes)
        {
            if (Input.GetKeyDown(keyCode))
            {
                m_KeyboardInput = m_KeyboardInput.Substring(0, m_KeyboardInput.Length - 1);
            }
        }

        foreach (KeyCode keyCode in m_EnterKeyCodes)
        {
            if (Input.GetKeyDown(keyCode))
            {
                MakeGuess();
            }
        }
    }

    internal void SetUpDictionary()
    {
        m_Dictionary = new ReferenceWordDictionary();
    }
    internal void StartNewGame()
    {
        GuessWordGameObject = new GuessWordGame(m_LetterBoxPrefab, m_KeyPrefab,  m_GameGridQuad, m_KeyboardQuad, m_CanvasObject, m_WordSize, m_NumTries, m_GridMargin, m_CellPadding);
        GuessWordGameObject.Run();
        ClearKeyboardInput();
    }

    internal void ClearKeyboardInput()
    {
        m_KeyboardInput = "";
        // clear the text field
        // m_InputField.text = "";
        // Focus
        // m_InputField.ActivateInputField();
    }

    internal void MakeGuess()
    {
        m_KeyboardInput = m_KeyboardInput.ToUpper();
        if (!IsValid(m_KeyboardInput))
        {
            Debug.Log($"Word {m_KeyboardInput} is not valid; please try again.");
            ClearKeyboardInput();
            return;
        }

        // submit the guess
        m_Won = GuessWordGameObject.GuessWord(m_KeyboardInput);
        ClearKeyboardInput();


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
        string message = $"Congratulations!\n\n The solution was {GuessWordGameObject.m_WordGridObject.m_Solution}";
        ShowMessageBox(message + "\n\nPlay again?\n");
    }

    internal void OnLose()
    {
        string message = $"Game over!  The solution was {GuessWordGameObject.m_WordGridObject.m_Solution}";
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
