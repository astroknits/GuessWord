using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

class KeyboardObject : Object
{
    // Quad defining region of keyboard on screen
    internal GameObject m_KeyboardQuad; // set in editor
    // Prefab for individual keys
    internal GameObject m_KeyPrefab; // set in editor

    // Parent empty GameObject for grouping the keys
    internal GameObject m_KeyBoxParent;

    // Order/arrangement of the keys
    internal KeyboardArrangement m_KeyboardArrangement;

    // Dictionary of KeyboardCell objects for the keyboard
    internal Dictionary<char, KeyboardCell> m_Keyboard;


    internal KeyboardObject(GameObject keyboardQuad,
                            GameObject keyBoxParent,
                            GameObject keyPrefab,
                            KeyboardArrangement arrangement)
    {
        m_KeyboardQuad = keyboardQuad;
        m_KeyBoxParent = keyBoxParent;
        m_KeyPrefab = keyPrefab;
        m_KeyboardArrangement = arrangement;
        SetUpKeyboard();
    }

    internal void SetUpKeyboard()
    {
        float cellSize = 0.250f;
        float xValue = -2.0f; // m_KeyboardQuad.transform.position.x - cellSize;
        float yValue = -2.5f; // m_KeyboardQuad.transform.position.y + cellSize;
        m_Keyboard = new Dictionary<char, KeyboardCell>();
        for (char letter = 'A'; letter <= 'Z'; letter++)
        {

            Vector3 centre = new Vector3(xValue, yValue, 4.0f);
            m_Keyboard.Add(letter, new KeyboardCell(letter, m_KeyBoxParent));
            m_Keyboard[letter].ConfigureCell(centre, cellSize, m_KeyPrefab);
            m_Keyboard[letter].RenderLetter(letter.ToString(), 3);
            if (letter == 'I' || letter == 'R')
            {
                yValue -= cellSize + 0.09f;
                xValue = -2.0f;
            }
            xValue += cellSize + 0.09f;
        }
    }

    /*
    internal void Stup()
    {
        m_Solution = GetSolution();
        m_LetterBoxParent = new GameObject("LetterBoxParent");

        m_WordGrid = new Word[m_NumTries];

        float cellSize = GetCellSize(m_GameGridQuad.transform.localScale);
        Vector3 upperLeftCorner = GetUpperLeftCorner(m_GameGridQuad.transform.position, m_GameGridQuad.transform.localScale);

        for (int guessNumber = 0; guessNumber < m_NumTries; guessNumber++)
        {
            float cellCentreY = upperLeftCorner.y - (m_GridMargin + cellSize / 2.0f + guessNumber * (m_CellPadding + cellSize));
            m_WordGrid[guessNumber] = new Word(m_Solution, m_LetterBoxParent, guessNumber);
            for (int i = 0; i<m_WordSize; i++)
            {
                float cellCentreX = upperLeftCorner.x + m_GridMargin + cellSize / 2.0f + i * (m_CellPadding + cellSize);
                Vector3 cellCentre = new Vector3(cellCentreX, cellCentreY, upperLeftCorner.z - m_ZOffset);
                m_WordGrid[guessNumber].Value[i].ConfigureCell(cellCentre, cellSize, m_LetterBox);
            }
        }

        return m_WordGrid;
    }
    */
    internal void Destroy()
    {
        foreach (var gameObject in m_KeyBoxParent.GetComponentsInChildren<Transform>())
        {
            GameObject.Destroy(gameObject.gameObject);
        }
    }
}
