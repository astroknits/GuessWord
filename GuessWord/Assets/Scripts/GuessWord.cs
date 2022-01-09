using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuessWord : MonoBehaviour
{
    [Header("Game Objects")]
    [SerializeField]
    internal GameObject m_LetterBox;
    [SerializeField]
    public GameObject m_GameGrid;

    /*
    [SerializeField]
    internal Canvas CanvasObject; // Assign in inspector
    */

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
        WordGridObject = new WordGrid(m_LetterBox, m_GameGrid, m_WordSize, m_NumTries, m_GridMargin, m_CellPadding);
        WordGridObject.GuessWord(0, "TURN");
        WordGridObject.GuessWord(1, "TEST");
        // StartCoroutine(Waiter(CanvasObject));
        // CanvasObject.enabled = false;
    }

    IEnumerator Waiter(Canvas CanvasObject)
    {
        CanvasObject.enabled = true;
        //Wait for 10 seconds
        Debug.Log("Waiting for 10 seconds");
        yield return new WaitForSeconds(10);
        Debug.Log("Done waiting");
        CanvasObject.enabled = false;
        Debug.Log("Waiting for 2 seconds");
        yield return new WaitForSeconds(2);
        Debug.Log("Done.");
    }

    void PrintGridInfo()
    {
        Debug.Log($"Game: {m_WordSize} word length x {m_NumTries} guesses");
    }
}
