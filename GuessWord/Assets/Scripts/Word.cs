using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class Word
{
    internal WordGridCell[] Value;
    internal string Solution;
    internal int GuessNumber;
    internal bool Match;
    private GameObject WordGridCellParent;

    internal Word(string solution, GameObject wordGridCellParent, int guessNumber)
    {
        Solution = solution;
        WordGridCellParent = wordGridCellParent;
        GuessNumber = guessNumber;
        Value = GetEmptyWord();
    }

    internal WordGridCell[] GetEmptyWord()
    {
        WordGridCell[] word = new WordGridCell[Solution.Length];
        for (int i = 0; i < Solution.Length; i++)
        {
            word[i] = new WordGridCell(Solution[i], WordGridCellParent);
        }

        return word;
    }

    internal bool GuessWord(string word)
    {
        SetWord(word);
        SetLetterColors();
        // DrawText(word);
        Debug.Log($"Word {word} is a match? {Match}");
        return Match;
    }

    internal void DrawCurrentText(string word)
    {
        for (var i = 0; i < Solution.Length; i++)
        {
            if (i >= word.Length)
            {
                Value[i].DrawCurrentValue(' ');
            }
            else
            {
                Value[i].DrawCurrentValue(word[i]);
            }
        }
    }

    internal void ClearCurrentText()
    {
        for (var i = 0; i < Solution.Length; i++)
        {
            Value[i].DrawCurrentValue(' ');
        }
    }

    internal void SetWord(string word)
    {
        Match = true;
        for (int i = 0; i < word.Length; i++)
        {
            if (!Value[i].GuessLetter(word[i]))
            {
                Match = false;
            }
        }
    }

    internal void SetLetterColors()
    {
        for (int i = 0; i < Value.Length; i++)
        {
            if (Value[i].Match)
            {
                Value[i].SetBoxColor(Color.green);
            }
            else if (Solution.Contains(Value[i].Value.ToString()))
            {
                Value[i].SetBoxColor(Color.blue);
            }
            else
            {
                Value[i].SetBoxColor(Color.grey);
            }
        }
    }

    internal void UpdateKeyboard(KeyboardObject keyboardObject)
    {
        for (int i = 0; i < Value.Length; i++)
        {
            var letter = Value[i].Value;
            var color = Value[i].CellColor;
            keyboardObject.m_Keyboard[letter].CellColor = color;
            keyboardObject.m_Keyboard[letter].Cube.GetComponent<Renderer>().material.color = color;

        }
    }
}