using UnityEngine;
using System.IO;
using System.Text;
using Newtonsoft.Json;

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
        string jsonFilePath = Path.Combine(Application.dataPath, "Resources", fileName + ".json");
        string jsonString = File.ReadAllText(jsonFilePath, Encoding.UTF8);
        SentencesTemplates sentencesTemplates = JsonConvert.DeserializeObject<SentencesTemplates>(jsonString);

        return sentencesTemplates;
    }
}
