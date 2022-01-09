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


    // Start is called before the first frame update
    void Start()
    {
        PrintGridInfo();

        WordGridObject = new WordGrid(m_LetterBox, m_GameGrid, m_CanvasObject, m_WordSize, m_NumTries, m_GridMargin, m_CellPadding);
        WordGridObject.Run();

        TMP_InputField inputField = GameObject.Find("InputGuessTextField").GetComponent<TMP_InputField>();
        // GameObject inputField = GetInputField();
        // Debug.Log($"input field: {inputField}");
        // TMP_InputField ipf = inputField.GetComponent<TMP_InputField>();
        inputField.onEndEdit.AddListener(MakeGuess);

        /*
        GameObject inputFieldGO = new GameObject ();
        inputFieldGO.name = "inputfield";
        InputField inputField = inputFieldGO.AddComponent<InputField> ();
        inputField.text = "Hello World!";
        inputField.transform.position = new Vector3(-23.0f, -154.5f, 0.0f);

        GameObject placeholderGO = new GameObject();
        placeholderGO.transform.SetParent(inputFieldGO.transform);
        Text myPlaceholderText = placeholderGO.AddComponent<Text>();
        inputField.placeholder = myPlaceholderText;

        Debug.Log($"input field: {inputField}");
        */


        /*
        InputField nameInputField = null;
        // string placeHolderText = nameInputField.placeholder.GetComponent<Text> ().text = "Guess a word ...";
        InputField.SubmitEvent submitEvent = new InputField.SubmitEvent();
        submitEvent.AddListener(MakeGuess);
        nameInputField.onEndEdit = submitEvent;

        Debug.Log($"input field: {nameInputField}");
        */
        // InputField _inputField = GameObject.Find("InputGuessTextField").GetComponent<InputField>();
        // Debug.Log($"input field: {_inputField}");
        // _inputField.onEndEdit.AddListener(MakeGuess);
    }

    internal GameObject GetInputField()
    {
        int children = m_CanvasObject.transform.childCount;
        for (int i = 0; i < children; ++i)
        {
            Debug.Log("For loop: " + m_CanvasObject.transform.GetChild(i));
            GameObject go = m_CanvasObject.transform.GetChild(i).gameObject;
            if (go.name == "InputGuessTextField")
            {
                Debug.Log($"Found it.  {go.name}");
                return go;
            }
        }

        return null;
    }

    internal void MakeGuess(string word)
    {
        WordGridObject.GuessWord(word);
    }

    void ToggleCanvas()
    {
        m_CanvasObject.enabled = !m_CanvasObject.enabled;
    }

    void SetUpCanvas()
    {
        StartCoroutine(Waiter(m_CanvasObject));
    }

    IEnumerator Waiter(Canvas CanvasObject)
    {
        m_CanvasObject.enabled = true;
        //Wait for 10 seconds
        Debug.Log("Waiting for 10 seconds");
        yield return new WaitForSeconds(10);
        Debug.Log("Done waiting");
        m_CanvasObject.enabled = false;
        Debug.Log("Waiting for 2 seconds");
        yield return new WaitForSeconds(2);
        Debug.Log("Done.");
    }

    void PrintGridInfo()
    {
        Debug.Log($"Game: {m_WordSize} word length x {m_NumTries} guesses");
    }
}
