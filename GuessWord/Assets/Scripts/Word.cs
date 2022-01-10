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
        bool match = true;
        for (int i = 0; i < word.Length; i++)
        {
            if (!Value[i].GuessLetter(word[i], i, GuessNumber))
            {
                match = false;
            }
        }

        Match = match;
        Debug.Log($"Word {word} is a match? {match}");
        return match;
    }
}