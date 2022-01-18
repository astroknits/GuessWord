using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

internal class KeyboardObject : BaseLetterGrid
{
    // Order/arrangement of the keys
    internal KeyboardArrangement m_KeyboardArrangement;

    // Dictionary of KeyboardCell objects for the keyboard
    internal Dictionary<char, KeyboardCell> m_Keyboard;

    internal KeyboardObject(KeyboardType keyboardType,
                            GameObject letterBox,
                            GameObject letterBoxParent,
                            GameObject gridQuad,
                            float gridMargin,
                            float cellPadding) :
        base(letterBox, letterBoxParent, gridQuad, gridMargin, cellPadding)
    {
        m_KeyboardArrangement = KeyboardArrangement.GetKeyboardArrangement(keyboardType);
        m_MaxCols = m_KeyboardArrangement.m_MaxCols;
        m_MaxRows = m_KeyboardArrangement.m_MaxRows;
        Create();
    }

    internal void Create()
    {
        m_Keyboard = new Dictionary<char, KeyboardCell>();

        float cellSize = GetCellSize();
        char[][] alpha = m_KeyboardArrangement.m_KeyboardLetterGrid;

        PopulateKeyboard(cellSize, alpha);
    }

    internal void PopulateKeyboard(float cellSize, char[][] alpha){

        Vector3 upperLeftCorner = GetUpperLeftCorner();

        for (int j = 0; j < alpha.Length; j++)
        {
            float cellCentreY = upperLeftCorner.y - (m_GridMargin + cellSize / 2.0f + j * (m_CellPadding + cellSize));
            char[] row = alpha[j];
            for (int i = 0; i < row.Length; i++)
            {
                char letter = row[i];
                float cellCentreX = upperLeftCorner.x + m_GridMargin + cellSize / 2.0f + i * (m_CellPadding + cellSize);
                Vector3 centre = new Vector3(cellCentreX, cellCentreY, 9.5f);

                // Now apply the settings for the letter and render the key
                m_Keyboard.Add(letter, new KeyboardCell(letter, m_LetterBoxParent));
                m_Keyboard[letter].ConfigureCell(centre, cellSize, m_LetterBox);
                m_Keyboard[letter].SetUpText(letter.ToString(), 3);
            }
        }
    }
}
