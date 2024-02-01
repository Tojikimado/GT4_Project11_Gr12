using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;
using System.Diagnostics;

public class NPCGenerator : MonoBehaviour
{
    [SerializeField] private List<TraitSO> m_AllTraits;
    [SerializeField] private int m_NumberOfNPCsToGenerate = 6;
    [SerializeField] private GameObject prefabNpc;
    [SerializeField] private GameObject _Canvas;
    [SerializeField] private UIManager Manager;

    private float pos = 0.0f;
    private int buffer= 0;
    private int buffer2= -1;

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
        GameObject newGO = Instantiate(prefabNpc, _Canvas.transform);
        Manager.NPCLIST.Add(newGO);

        if (npcNumber % 2 == 0)
        {
            pos = 280.0f * buffer * buffer2;
            buffer2 *= -1;
        }
           
            
        if (npcNumber % 2 == 1)
        {
            pos = 280.0f * buffer * buffer2;
            buffer++;
            buffer2 *= -1;
            
        }
            


        newGO.transform.localPosition = new Vector3(pos, newGO.transform.localPosition.y, newGO.transform.localPosition.z);
        

                NPC newNPC = newGO.GetComponent<NPC>();
        newNPC.Name = GenerateRandomName(npcNumber);
        newNPC.PersonalityTraits = GenerateRandomTraits();
        newNPC.Description = GenerateRandomDescription(newNPC.Name, newNPC.PersonalityTraits);

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
