using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


internal class GuessWordGame : Object
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

    // Word grid for keeping track of guessed words
    internal WordGridObject m_WordGridObject;

    // Keyboard
    internal KeyboardObject m_KeyboardObject;

    internal int m_GuessCount;

    internal float m_ZOffset = 0.8f;

    internal GameObject m_LetterBoxParent;
    internal GameObject m_KeyBoxParent;

    internal GuessWordGame(GameObject letterBox,
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
        SetUpWordGrid();
        // Enable the canvas
        m_CanvasObject.enabled = true;
    }

    internal void SetUpWordGrid()
    {
        m_LetterBoxParent = new GameObject("LetterBoxParent");
        m_WordGridObject = new WordGridObject(m_LetterBox, m_LetterBoxParent, m_GameGridQuad, m_WordSize, m_NumTries, m_GridMargin, m_CellPadding);
    }

    internal void SetUpKeyboard()
    {
        m_KeyBoxParent = new GameObject("KeyBoxParent");
        m_KeyboardObject = new KeyboardObject(m_KeyboardQuad, m_KeyBoxParent, m_KeyPrefab, KeyboardArrangement.QWERTY);
    }

    internal void Destroy()
    {
        m_WordGridObject.Destroy();
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
        bool match = m_WordGridObject.m_WordGrid[m_GuessCount].GuessWord(word);
        m_GuessCount += 1;
        return match;
    }
}