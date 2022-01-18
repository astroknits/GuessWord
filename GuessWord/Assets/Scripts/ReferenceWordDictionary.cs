using NetSpell.SpellChecker.Dictionary;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

internal class ReferenceWordDictionary : Object
{
    internal WordDictionary m_Dictionary;
    internal List<string> m_CommonWords;
    internal ReferenceWordDictionary()
    {
        SetUpDictionary();
        SetUpCommonWords();
    }

    internal void SetUpDictionary()
    {
        m_Dictionary = new WordDictionary();
        m_Dictionary.DictionaryFile = "Assets/Packages/NetSpell.2.1.7/dic/en-US.dic";
        m_Dictionary.Initialize();
    }

    internal void SetUpCommonWords()
    {
        string commonWordList = "../data/common.txt";
        m_CommonWords = new List<string>();
        var stream = new StreamReader(commonWordList);
        while (!stream.EndOfStream)
        {
            m_CommonWords.Add(stream.ReadLine().ToLower());
        }
    }

    internal bool Contains(string word)
    {
        bool match = (m_Dictionary.Contains(word) || m_CommonWords.Contains(word));
        return match;
    }

    internal string GetRandomWord(int wordLength)
    {
        string[] commonWordsOfLength = m_CommonWords.Where(word => word.Length == wordLength).ToArray();
        int randomIndex = Random.Range(0, commonWordsOfLength.Length-1);
        return commonWordsOfLength[randomIndex];
    }
}
