using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;

public class NPCGenerator : MonoBehaviour
{
    [SerializeField] private List<TraitSO> m_AllTraits;
    [SerializeField] private int m_NumberOfNPCsToGenerate = 6;

    private void Start()
    {
        GenerateNPCs();
    }

    void GenerateNPCs()
    {
        for (int i = 0; i < m_NumberOfNPCsToGenerate; i++)
        {
            NPC newNPC = CreateNPC(i + 1);
            newNPC.AfficherInfos();
        }
    }

    NPC CreateNPC(int npcNumber)
    {
        NPC newNPC = new GameObject("NPC").AddComponent<NPC>();

        newNPC.Name = GenerateRandomName(npcNumber);
        newNPC.PersonalityTraits = GenerateRandomTraits();
        newNPC.Description = GenerateRandomDescription();

        return newNPC;
    }

    string GenerateRandomName(int npcNumber)
    {
        return "NPC " + npcNumber;
    }

    List<TraitSO> GenerateRandomTraits()
    {
        List<TraitSO> randomTraits = new List<TraitSO>();

        while (randomTraits.Count < 3)
        {
            TraitSO randomTrait = m_AllTraits[Random.Range(0, m_AllTraits.Count)];

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
            {
                randomTraits.Add(randomTrait);
            }
        }

        return randomTraits;
    }


    string GenerateRandomDescription(string name, List<TraitSO> traits)
    {
        string description = "Je suis " + name + ".\nMes traits principaux sont :\n";

        foreach (TraitSO trait in traits)
            description += "- " + trait.Name + "\n";

        return description;
    }
}
