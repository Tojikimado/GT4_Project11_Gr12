using UnityEngine;
using System.Collections.Generic;
using System.Collections;

[System.Serializable]
public class TraitSentences
{
    public List<string> Past;
    public List<string> Present;
    public List<string> Futur;

    public IEnumerator<string> GetEnumerator()
    {
        foreach (string sentence in Past)
        {
            yield return sentence;
        }
        foreach (string sentence in Present)
        {
            yield return sentence;
        }
        foreach (string sentence in Futur)
        {
            yield return sentence;
        }
    }
}

[System.Serializable]
public class SentencesTemplates
{
    public Dictionary<string, TraitSentences> TraitsSentences;
}
