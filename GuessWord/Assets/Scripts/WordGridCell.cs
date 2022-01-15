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

    internal bool GuessLetter(char letter, int i, int j)
    {
        SetValue(letter, i, j);
        Match = Value == Solution;
        return Match;
    }
}