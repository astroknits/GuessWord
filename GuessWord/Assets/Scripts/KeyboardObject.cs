using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

class KeyboardObject : BaseLetterGrid
{
    // Order/arrangement of the keys
    internal KeyboardArrangement m_KeyboardArrangement;

    // Dictionary of KeyboardCell objects for the keyboard
    internal Dictionary<char, KeyboardCell> m_Keyboard;

    internal KeyboardObject(KeyboardArrangement arrangement,
                            GameObject letterBox,
                            GameObject letterBoxParent,
                            GameObject gridQuad,
                            float gridMargin,
                            float cellPadding) :
        base(letterBox, letterBoxParent, gridQuad, gridMargin, cellPadding)
    {
        m_KeyboardArrangement = arrangement;
        m_MaxCols = 9; // maximum 9 chars per row in keyboard
        m_MaxRows = 3; // maximum 3 rows per keyboard
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
            m_Keyboard.Add(letter, new KeyboardCell(letter, m_LetterBoxParent));
            m_Keyboard[letter].ConfigureCell(centre, cellSize, m_LetterBox);
            m_Keyboard[letter].RenderLetter(letter.ToString(), 3);
            if (letter == 'I' || letter == 'R')
            {
                yValue -= cellSize + 0.09f;
                xValue = -2.0f;
            }
            xValue += cellSize + 0.09f;
        }
    }
}
