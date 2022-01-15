using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

enum KeyboardArrangement
{
    QWERTY,
    ALPHA,
    TYPE
}

class KeyboardCell : LetterCell
{
    internal KeyboardCell(GameObject letterCellParent) : base(letterCellParent) {}
}


class KeyboardObject : Object
{
    internal GameObject m_KeyPrefab;
    internal GameObject m_KeyboardQuad;
    internal KeyboardArrangement m_KeyboardArrangement;

    internal KeyboardCell[] m_Keyboard;

    internal GameObject m_KeyBoxParent;

    internal KeyboardObject(GameObject keyboardQuad, GameObject keyPrefab, KeyboardArrangement arrangement)
    {
        m_KeyboardQuad = keyboardQuad;
        m_KeyPrefab = keyPrefab;
        m_KeyboardArrangement = arrangement;
    }
}


internal class WordGrid : Object
{
    internal GameObject m_LetterBox;
    internal GameObject m_KeyPrefab;
    internal GameObject m_GameGrid;
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

    internal WordGrid(GameObject letterBox, GameObject keyPrefab, GameObject gameGrid, GameObject keyboardQuad, Canvas canvasObject,
        int wordSize, int numTries, float gridMargin, float cellPadding)
    {
        m_LetterBox = letterBox;
        m_KeyPrefab = keyPrefab;
        m_GameGrid = gameGrid;
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
        // Set up the grid object
        SetGrid();
        // Enable the canvas
        m_CanvasObject.enabled = true;
    }

    internal string GetSolution()
    {
        return new Dictionary().GetRandomWord(m_WordSize).ToUpper();
    }

    internal Word[] SetGrid()
    {
        m_Solution = GetSolution();
        m_LetterBoxParent = new GameObject("LetterBoxParent");

        m_Grid = new Word[m_NumTries];

        float cellSize = GetCellSize(m_GameGrid.transform.localScale);
        Vector3 upperLeftCorner = GetUpperLeftCorner(m_GameGrid.transform.position, m_GameGrid.transform.localScale);

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
        m_KeyboardObject = new KeyboardObject(m_KeyboardQuad, m_KeyPrefab, KeyboardArrangement.QWERTY);
    }

    internal void Destroy()
    {
        foreach (var gameObject in m_LetterBoxParent.GetComponentsInChildren<Transform>())
        {
            GameObject.Destroy(gameObject.gameObject);
        }
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