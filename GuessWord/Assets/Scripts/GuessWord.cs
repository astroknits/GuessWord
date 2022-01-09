using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordGrid : MonoBehaviour
{
    internal class LetterCell
    {
        internal Vector3 Centre;
        internal char Value;
        internal GameObject Cube;

        internal LetterCell(Vector3 centre, float cellSize, GameObject prefab)
        {
            Centre = centre;
            Cube = Instantiate(prefab, centre, Quaternion.identity);
            Cube.transform.localScale *= cellSize;
        }

        internal void SetValue(char letter)
        {
            Value = letter;
            var target = Cube.transform;
            target.transform.position += new Vector3(0, 0, 0.5f);
        }
    }

    internal class Word()
    {
        internal LetterCell[] Value;
        internal LetterCell[] Solution;

        internal Word(LetterCell[] value, LetterCell[] solution)
        {
            Value = value;
            Solution = solution;
        }
    }

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

    internal LetterCell[,] LetterGrid;

    // Start is called before the first frame update
    void Start()
    {
        LetterGrid = new LetterCell[m_WordSize, m_NumTries];

        Vector3 gridPosition = m_GameGrid.transform.position;
        Vector3 gridScale = m_GameGrid.transform.localScale;

        Vector3 upperLeftCorner = new Vector3(
            gridPosition.x - gridScale.x / 2.0f,
            gridPosition.y + gridScale.y / 2.0f,
            gridPosition.z);

        float cellWidth = (gridScale.x - 2.0f * m_GridMargin - (m_WordSize - 1.0f) * m_CellPadding) / m_WordSize;
        float cellHeight = (gridScale.y - 2.0f * m_GridMargin - (m_NumTries - 1.0f) * m_CellPadding) / m_NumTries;
        float cellSize = Mathf.Min(cellWidth, cellHeight);

        PrintGridInfo(gridPosition, gridScale, cellWidth, cellHeight, cellSize);

        for (int i = 0; i<m_WordSize; i++)
        {
            float cellCentreX = upperLeftCorner.x + m_GridMargin + cellSize / 2.0f + i * (m_CellPadding + cellSize);
            for (int j = 0; j < m_NumTries; j++)
            {
                float cellCentreY = upperLeftCorner.y - (m_GridMargin + cellSize / 2.0f + j * (m_CellPadding + cellSize));
                Vector3 cellCentre = new Vector3(cellCentreX, cellCentreY, upperLeftCorner.z - 1.0f);
                LetterGrid[i, j] = new LetterCell(cellCentre, cellSize, m_LetterBox);
            }
        }
    }

    void PrintGridInfo(Vector3 gridPosition, Vector3 gridScale, float cellWidth, float cellHeight, float cellSize)
    {
        Debug.Log($"Game: Word length {m_WordSize}, {m_NumTries} guesses");
        Debug.Log($"Grid physical dimensions: {gridScale.x}x{gridScale.y}");
        Debug.Log($"Grid z-distance: {gridPosition.z}");
        Debug.Log($"Grid Centre: {gridPosition.x}, {gridPosition.y}");
        Debug.Log($"Cell width {cellWidth} height {cellHeight} size {cellSize}");
    }

    internal float GetCellWidth()
    {
        return 0.0f;
    }

    internal float GetCellPosition()
    {
        return 0.0f;
    }
}
