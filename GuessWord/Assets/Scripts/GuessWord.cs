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

    internal Word[] WordGrid;

    // Start is called before the first frame update
    void Start()
    {
        PrintGridInfo();
        WordGrid = GetWordGrid();
        WordGrid[0].GuessWord("TURN");
        WordGrid[1].GuessWord("TEST");
    }

    void PrintGridInfo()
    {
        Debug.Log($"Game: {m_WordSize} word length x {m_NumTries} guesses");
    }

    internal Word[] GetWordGrid()
    {
        string solution = GetSolution();
        Word[] wordGrid = new Word[m_NumTries];
        float zOffset = 0.8f;

        float cellSize = GetCellSize(m_GameGrid.transform.localScale);
        Vector3 upperLeftCorner = GetUpperLeftCorner(m_GameGrid.transform.position, m_GameGrid.transform.localScale);

        for (int j = 0; j < m_NumTries; j++)
        {
            float cellCentreY = upperLeftCorner.y - (m_GridMargin + cellSize / 2.0f + j * (m_CellPadding + cellSize));
            wordGrid[j] = new Word(solution);
            for (int i = 0; i<m_WordSize; i++)
            {
                float cellCentreX = upperLeftCorner.x + m_GridMargin + cellSize / 2.0f + i * (m_CellPadding + cellSize);
                Vector3 cellCentre = new Vector3(cellCentreX, cellCentreY, upperLeftCorner.z - zOffset);
                wordGrid[j].Value[i].ConfigureCell(cellCentre, cellSize, m_LetterBox);
            }
        }

        return wordGrid;
    }

    internal string GetSolution()
    {
        return "TEST";
    }

    internal Vector3 GetUpperLeftCorner(Vector3 gridPosition, Vector3 gridScale)
    {
        return new Vector3(
            gridPosition.x - gridScale.x / 2.0f,
            gridPosition.y + gridScale.y / 2.0f,
            gridPosition.z);
    }

    internal float GetCellSize(Vector3 gridScale)
    {
        float cellWidth = (gridScale.x - 2.0f * m_GridMargin - (m_WordSize - 1.0f) * m_CellPadding) / m_WordSize;
        float cellHeight = (gridScale.y - 2.0f * m_GridMargin - (m_NumTries - 1.0f) * m_CellPadding) / m_NumTries;
        return Mathf.Min(cellWidth, cellHeight);
    }
}
