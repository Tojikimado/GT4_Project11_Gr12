using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;
using System.Diagnostics;
using System.Text;
using System.Globalization;

public class NPCManager : MonoBehaviour
{
    [SerializeField] private List<TraitSO> m_AllPersonalityTraits;
    [SerializeField] private List<TraitSO> m_AllPhysicalTraits;
    [SerializeField] private int m_NPCToGenerate = 6;
    [SerializeField] private GameObject m_NPCPrefab;
    [SerializeField] private GameObject m_Canvas;
    [SerializeField] private UIManager m_UIManager;
    [SerializeField] private int m_Seed;
    [SerializeField] private FaceGenerator _FaceGenerator;


    private SentencesTemplates m_SentencesTemplates;

    private void Start()
    {
        m_Seed = (int)System.DateTime.Now.Ticks;
        _FaceGenerator.SetUp(m_Seed);
         m_SentencesTemplates = JSONLoader.Instance.LoadSentencesTemplates("TraitsSentences");
        GenerateNPCs();
        m_UIManager.UpdateSeed(m_Seed.ToString());
    }

    void GenerateNPCs()
    {
        Random.InitState(m_Seed);
        
        for (int i = 0; i < m_NPCToGenerate; i++)
        {
            NPC newNPC = CreateNPC(i + 1);
        }
        m_UIManager.RefreshAllNPC();
        m_UIManager.PlaceNPC();
    }
    public void RefreshSeed()
    {
        m_Seed = (int)System.DateTime.Now.Ticks;

        m_UIManager.DestroyAllNpc();
        GenerateNPCs();
        m_UIManager.UpdateSeed(m_Seed.ToString());
    }
    NPC CreateNPC(int npcNumber)
    {
        GameObject NPCGO = Instantiate(m_NPCPrefab, m_Canvas.transform);
        m_UIManager.AddToNPCList(NPCGO);

       
        
        NPC newNPC = NPCGO.GetComponent<NPC>();
        newNPC.Name = GenerateRandomName(npcNumber);
        newNPC.PersonalityTraits = GenerateRandomTraits(m_AllPersonalityTraits);
        newNPC.PhysicalTraits = GenerateRandomTraits(m_AllPhysicalTraits);
        newNPC.Description = GenerateRandomDescription(newNPC.Name, newNPC.PersonalityTraits);

        NPCView newNPCView = NPCGO.GetComponent<NPCView>();
        newNPCView.spawnface();
        _FaceGenerator.GenerateFace(npcNumber,newNPC, newNPCView.m_NPCFaceSpawned.GetComponent<NpcFaceView>());
        return newNPC;
    }

    string GenerateRandomName(int npcNumber)
    {
        return "NPC " + npcNumber;
    }

    List<TraitSO> GenerateRandomTraits(List<TraitSO> allTraits)
    {
        List<TraitSO> randomTraits = new List<TraitSO>();

        while (randomTraits.Count < 3)
        {
            TraitSO randomTrait = allTraits[Random.Range(0, allTraits.Count)];

            bool hasConflict = false;
            foreach (TraitSO selectedTrait in randomTraits)
            {
                if (randomTrait.ConflictTraits.Contains(selectedTrait) || selectedTrait.ConflictTraits.Contains(randomTrait))
                {
                    hasConflict = true;
                    break;
                }
            }

            if (!hasConflict)
                randomTraits.Add(randomTrait);
        }

        return randomTraits;
    }

    string GenerateRandomDescription(string name, List<TraitSO> personalityTraits)
    {
        string description = "";
        
        foreach (TraitSO trait in personalityTraits)
        {
            string traitName = RemoveDiacritics(trait.Name.Trim());
            if (m_SentencesTemplates.TraitsSentences.ContainsKey(traitName))
            {
                TraitSentences traitSentences = m_SentencesTemplates.TraitsSentences[traitName];
                string randomSentence = GetRandomSentence(traitSentences);
                description += randomSentence + "\n";
            }
            else
                UnityEngine.Debug.LogWarning("La clé '" + traitName + "' n'est pas présente dans le dictionnaire.");
        }

        return description;
    }

    string GetRandomSentence(TraitSentences traitSentences)
    {
        List<string> allSentences = new List<string>();
        allSentences.AddRange(traitSentences.Past);
        allSentences.AddRange(traitSentences.Present);
        allSentences.AddRange(traitSentences.Futur);

        int randomIndex = Random.Range(0, allSentences.Count);
        return allSentences[randomIndex];
    }

    private string RemoveDiacritics(string text)
    {
        string normalized = text.Normalize(NormalizationForm.FormD);
        StringBuilder builder = new StringBuilder();

        foreach (char c in normalized)
        {
            if (CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
            {
                builder.Append(c);
            }
        }

        return builder.ToString();
    }
}
