using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

internal class WordGridCell: LetterCell
{
    internal char Solution;
    internal bool Match;

    internal WordGridCell(char solution, GameObject letterCellParent) : base(letterCellParent)
    {
        Solution = solution;
    }

    internal bool GuessLetter(char letter)
    {
        SetValue(letter);
        m_LetterText.text = Value.ToString();
        Match = Value == Solution;
        return Match;
    }

    internal void DrawCurrentValue(char letter)
    {
        Value = letter;
        SetBoxColor(Color.white);
        m_LetterText.text = Value.ToString();
    }
}