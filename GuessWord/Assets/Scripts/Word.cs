using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class Word
{
    internal LetterCell[] Value;
    internal string Solution;
    internal int GuessNumber;
    internal bool Match;
    private GameObject LetterCellParent;

    internal Word(string solution, GameObject letterCellParent, int guessNumber)
    {
        Solution = solution;
        LetterCellParent = letterCellParent;
        GuessNumber = guessNumber;
        Value = GetEmptyWord();
    }

    internal LetterCell[] GetEmptyWord()
    {
        LetterCell[] word = new LetterCell[Solution.Length];
        for (int i = 0; i < Solution.Length; i++)
        {
            word[i] = new LetterCell(Solution[i], LetterCellParent);
        }

        return word;
    }

    internal bool GuessWord(string word)
    {
        SetWord(word);
        SetLetterColors();
        Debug.Log($"Word {word} is a match? {Match}");
        return Match;
    }

    internal void SetWord(string word)
    {
        Match = true;
        for (int i = 0; i < word.Length; i++)
        {
            if (!Value[i].GuessLetter(word[i], i, GuessNumber))
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
}