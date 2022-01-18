using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

internal class WordGridObject : BaseLetterGrid
{
    // Word array for keeping track of guessed words
    internal Word[] m_WordGrid;
    internal int m_GuessCount;
    internal string m_Solution;

    internal WordGridObject(GameObject letterBox,
                      GameObject letterBoxParent,
                      GameObject gameGridQuad,
                      int wordSize,
                      int numTries,
                      float gridMargin,
                      float cellPadding) :
        base(letterBox, letterBoxParent, gameGridQuad, gridMargin, cellPadding)
    {
        m_GuessCount = 0;
        m_MaxCols = wordSize;
        m_MaxRows = numTries;
        Create();
    }

    internal string GetSolution()
    {
        return new ReferenceWordDictionary().GetRandomWord(m_MaxCols).ToUpper();
    }

    internal void Create()
    {
        m_Solution = GetSolution();
        m_WordGrid = new Word[m_MaxRows];

        float cellSize = GetCellSize();
        Vector3 upperLeftCorner = GetUpperLeftCorner();

        for (int guessNumber = 0; guessNumber < m_MaxRows; guessNumber++)
        {
            float cellCentreY = upperLeftCorner.y - (m_GridMargin + cellSize / 2.0f + guessNumber * (m_CellPadding + cellSize));
            m_WordGrid[guessNumber] = new Word(m_Solution, m_LetterBoxParent, guessNumber);
            for (int i = 0; i<m_MaxCols; i++)
            {
                float cellCentreX = upperLeftCorner.x + m_GridMargin + cellSize / 2.0f + i * (m_CellPadding + cellSize);
                Vector3 cellCentre = new Vector3(cellCentreX, cellCentreY, upperLeftCorner.z - m_ZOffset);
                m_WordGrid[guessNumber].Value[i].ConfigureCell(cellCentre, cellSize, m_LetterBox);
                m_WordGrid[guessNumber].Value[i].SetUpText($"LetterBox {i} {guessNumber}");
            }
        }
    }

    internal bool GuessWord(string word)
    {
        bool match = m_WordGrid[m_GuessCount].GuessWord(word);
        m_GuessCount += 1;
        return match;
    }
}