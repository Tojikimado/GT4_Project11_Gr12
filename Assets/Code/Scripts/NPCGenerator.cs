using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;
using System.Diagnostics;

public class NPCGenerator : MonoBehaviour
{
    [SerializeField] private List<TraitSO> m_AllPersonalityTraits;
    [SerializeField] private List<TraitSO> m_AllPhysicalTraits;
    [SerializeField] private int m_NPCToGenerate = 6;
    [SerializeField] private GameObject m_NPCPrefab;
    [SerializeField] private GameObject m_Canvas;
    [SerializeField] private UIManager m_UIManager;
    [SerializeField] private int m_Seed;

    private float pos = 0.0f;
    private int buffer= 0;
    private int buffer2= -1;

    private void Start()
    {
        m_Seed = (int)System.DateTime.Now.Ticks;

        GenerateNPCs();
    }

    void GenerateNPCs()
    {
        Random.InitState(m_Seed);

        for (int i = 0; i < m_NPCToGenerate; i++)
        {
            NPC newNPC = CreateNPC(i + 1);
        }
    }

    NPC CreateNPC(int npcNumber)
    {
        GameObject NPCGO = Instantiate(m_NPCPrefab, m_Canvas.transform);
        m_UIManager.AddToNPCList(NPCGO);

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
            


        NPCGO.transform.localPosition = new Vector3(pos, NPCGO.transform.localPosition.y, NPCGO.transform.localPosition.z);

        NPC newNPC = NPCGO.GetComponent<NPC>();
        newNPC.Name = GenerateRandomName(npcNumber);
        newNPC.PersonalityTraits = GenerateRandomTraits(m_AllPersonalityTraits);
        newNPC.PhysicalTraits = GenerateRandomTraits(m_AllPhysicalTraits);
        newNPC.Description = GenerateRandomDescription(newNPC.Name, newNPC.PersonalityTraits, newNPC.PhysicalTraits);

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

    string GenerateRandomDescription(string name, List<TraitSO> personalityTraits, List<TraitSO> physicalTraits)
    {
        string description = "Je suis " + name + ".\nMes traits principaux de personnalité sont :\n";

        foreach (TraitSO trait in personalityTraits)
            description += "- " + trait.Name + "\n";

        description += "\nMes traits physiques sont :\n";

        foreach (TraitSO trait in physicalTraits)
            description += "- " + trait.Name + "\n";

        return description;
    }
}
