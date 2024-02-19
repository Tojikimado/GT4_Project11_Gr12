using UnityEngine;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using System.Collections.Generic;
using UnityEngine.AI;
using System;


public class JSONLoader : MonoBehaviour
{
    public static JSONLoader Instance { get; private set; }

    private void Awake()
    {
        if (Instance)
            Destroy(Instance);

        Instance = this;
    }

    public SentencesTemplates LoadSentencesTemplates(string fileName)
    {
        TextAsset jsonFile = Resources.Load<TextAsset>(fileName);
        SentencesTemplates sentencesTemplates = JsonConvert.DeserializeObject<SentencesTemplates>(jsonFile.text);
        return sentencesTemplates;
    }

    public NamesData LoadNamesData(string fileName)
    {
        TextAsset jsonFile = Resources.Load<TextAsset>(fileName);
        NamesData namesData = JsonConvert.DeserializeObject<NamesData>(jsonFile.text);

        return namesData;
    }

}
