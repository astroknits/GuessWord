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
        Vector3 upperLeftCorner = GetUpperLeftCorner();

        char[] alpha = m_KeyboardArrangement.m_CharArray;
        float cellCentreXStart = upperLeftCorner.x + m_GridMargin + cellSize / 2.0f;
        float cellCentreX = cellCentreXStart;
        float cellCentreY = upperLeftCorner.y - (m_GridMargin + cellSize / 2.0f);
        for (int l = 0; l < alpha.Length; l++)
        {
            char letter = alpha[l];
            Vector3 centre = new Vector3(cellCentreX, cellCentreY, 9.5f);
            m_Keyboard.Add(letter, new KeyboardCell(letter, m_LetterBoxParent));
            m_Keyboard[letter].ConfigureCell(centre, cellSize, m_LetterBox);
            m_Keyboard[letter].RenderLetter(letter.ToString(), 3);
            cellCentreX += (m_CellPadding + cellSize);
            if (letter == 'P' || letter == 'L')
            {
                cellCentreY -= (m_CellPadding + cellSize);
                cellCentreX = cellCentreXStart;
            }
        }
    }
}
