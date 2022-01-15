using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


internal class WordGrid : Object
{
    internal GameObject m_LetterBox;
    internal GameObject m_GameGridQuad;

    internal GameObject m_KeyPrefab;
    internal GameObject m_KeyboardQuad;

    internal Canvas m_CanvasObject;
    internal int m_WordSize; // number of letters per word
    internal int m_NumTries; // Number of guesses allowed per round

    // Grid settings
    internal float m_GridMargin = 0; // margin around grid, equal padding along top/bottom/sides
    internal float m_CellPadding = 0; // padding between grid cells

    // Word array for keeping track of guessed words
    internal Word[] m_Grid;

    // Keyboard
    internal KeyboardObject m_KeyboardObject;

    internal int m_GuessCount;

    internal string m_Solution;
    internal float m_ZOffset = 0.8f;

    internal GameObject m_LetterBoxParent;
    internal GameObject m_KeyBoxParent;

    internal WordGrid(GameObject letterBox,
                      GameObject keyPrefab,
                      GameObject gameGridQuad,
                      GameObject keyboardQuad,
                      Canvas canvasObject,
                      int wordSize,
                      int numTries,
                      float gridMargin,
                      float cellPadding)
    {
        m_LetterBox = letterBox;
        m_KeyPrefab = keyPrefab;
        m_GameGridQuad = gameGridQuad;
        m_KeyboardQuad = keyboardQuad;
        m_CanvasObject = canvasObject;
        m_WordSize = wordSize;
        m_NumTries = numTries;
        m_GridMargin = gridMargin;
        m_CellPadding = cellPadding;
        m_GuessCount = 0;
    }

    internal void Run()
    {
        // Set up the keyboard
        SetUpKeyboard();
        // Set up the grid object
        SetUpGrid();
        // Enable the canvas
        m_CanvasObject.enabled = true;
    }

    internal string GetSolution()
    {
        return new ReferenceWordDictionary().GetRandomWord(m_WordSize).ToUpper();
    }

    internal Word[] SetUpGrid()
    {
        m_Solution = GetSolution();
        m_LetterBoxParent = new GameObject("LetterBoxParent");

        m_Grid = new Word[m_NumTries];

        float cellSize = GetCellSize(m_GameGridQuad.transform.localScale);
        Vector3 upperLeftCorner = GetUpperLeftCorner(m_GameGridQuad.transform.position, m_GameGridQuad.transform.localScale);

        for (int guessNumber = 0; guessNumber < m_NumTries; guessNumber++)
        {
            float cellCentreY = upperLeftCorner.y - (m_GridMargin + cellSize / 2.0f + guessNumber * (m_CellPadding + cellSize));
            m_Grid[guessNumber] = new Word(m_Solution, m_LetterBoxParent, guessNumber);
            for (int i = 0; i<m_WordSize; i++)
            {
                float cellCentreX = upperLeftCorner.x + m_GridMargin + cellSize / 2.0f + i * (m_CellPadding + cellSize);
                Vector3 cellCentre = new Vector3(cellCentreX, cellCentreY, upperLeftCorner.z - m_ZOffset);
                m_Grid[guessNumber].Value[i].ConfigureCell(cellCentre, cellSize, m_LetterBox);
            }
        }

        return m_Grid;
    }

    internal void SetUpKeyboard()
    {
        m_KeyBoxParent = new GameObject("KeyBoxParent");
        m_KeyboardObject = new KeyboardObject(m_KeyboardQuad, m_KeyBoxParent, m_KeyPrefab, KeyboardArrangement.QWERTY);
    }

    internal void DestroyGrid()
    {
        foreach (var gameObject in m_LetterBoxParent.GetComponentsInChildren<Transform>())
        {
            GameObject.Destroy(gameObject.gameObject);
        }
    }

    internal void Destroy()
    {
        DestroyGrid();
        m_KeyboardObject.Destroy();
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

    internal bool GuessWord(string word)
    {
        bool match = m_Grid[m_GuessCount].GuessWord(word);
        m_GuessCount += 1;
        return match;
    }
}