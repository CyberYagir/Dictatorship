using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lang : MonoBehaviour
{
    public static Lang l;
    public int lang;
    public List<Word> words;
    public static int last = 0;
    public List<WordsClaster> clasters;

    [System.Serializable]
    public class WordsClaster
    {
        public string clasterName;
        public List<Word> words = new List<Word>();
    }

    public static string Find(string word, int claster)
    {
        for (int i = 0; i < l.clasters[claster].words.Count; i++)
        {
            if (l.clasters[claster].words[i].word.ToLower().Trim() == word.ToLower().Trim())
            {
                return l.clasters[claster].words[i].translations[l.lang];
            }
        }
        return word;
    }

    [System.Serializable]
    public class Word
    {
        public string word;
        public List<string> translations = new List<string>(new string[2]);
    }

    void Start()
    {
        l = this;
        lang = PlayerPrefs.GetInt("Lang", 1);
    }
}
