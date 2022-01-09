using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class Word
{
    internal LetterCell[] Value;
    internal string Solution;
    internal bool Match;

    internal Word(string solution)
    {
        Solution = solution;
        Value = GetEmptyWord(solution);
    }

    internal LetterCell[] GetEmptyWord(string solution)
    {
        LetterCell[] word = new LetterCell[solution.Length];
        for (int i = 0; i < solution.Length; i++)
        {
            word[i] = new LetterCell(solution[i]);
        }

        return word;
    }

    internal bool GuessWord(string word)
    {
        bool match = true;
        for (int i = 0; i < word.Length; i++)
        {
            if (!Value[i].GuessLetter(word[i]))
            {
                match = false;
            }
        }

        Match = match;
        Debug.Log($"Word {word} is a match? {match}");
        return match;
    }
}