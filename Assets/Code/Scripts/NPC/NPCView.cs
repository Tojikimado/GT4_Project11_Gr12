using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class NPCView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI m_NameText;
    [SerializeField] private TextMeshProUGUI m_DescriptionText;
    [SerializeField] private List<TMP_Dropdown> m_PersonalityTraitsList;
    [SerializeField] private List<TextMeshProUGUI> m_PhysicalTraitsList;
    [SerializeField] private NPC m_MyNPC;
    [SerializeField] private GameObject m_DescObject;
    [SerializeField] private GameObject m_TraitBOX;

    [SerializeField] private GameObject m_NPCFacePrefab;
    [SerializeField] public GameObject NPCFaceSpawned;
    [SerializeField] private GameObject m_Canvas;


    public Vector3 SlotPos;


    private bool IsFocused = false;

    public NPCManager NPCManager { get; internal set; }

    public void UpdateNpcView()
    {
        m_NameText.text = m_MyNPC.Name;

        for(int i = 0; i< m_PersonalityTraitsList.Count;i++ )
        {
            int index = m_PersonalityTraitsList[i].options.FindIndex(o => o.text == m_MyNPC.PersonalityTraits[i].Name);
            m_PersonalityTraitsList[i].value = index;
        }

        for (int i = 0; i < m_PhysicalTraitsList.Count; i++)
        {
            m_PhysicalTraitsList[i].text = m_MyNPC.PhysicalTraits[i].Name;
        }
        m_DescriptionText.text = m_MyNPC.Description;
    }
    public void spawnface()
    {
        NPCFaceSpawned = Instantiate(m_NPCFacePrefab, m_Canvas.transform);
    }

    public void ShowthisNPC()
    {
        IsFocused = !IsFocused;
        UIManager.Instance.ShowSelected(gameObject, IsFocused);
        m_DescObject.SetActive(!m_DescObject.activeSelf);
        m_TraitBOX.SetActive(!m_TraitBOX.activeSelf);
    }

    public void RegenDescription()
    {
        NPC npc = gameObject.GetComponent<NPC>();
        if (npc != null)
        {
            CheckTrait(npc); 
        }

        npc.Description = NPCManager.GenerateRandomDescription(npc);
        m_DescriptionText.text = npc.Description;
    }

    private void CheckTrait(NPC npc)
    {
        for (int i = 0; i < m_PersonalityTraitsList.Count; i++)
        {
            for (int o = 0; o < m_PersonalityTraitsList.Count; o++)
            {
                if (i == o)
                    break;
                int dropdownIValue = m_PersonalityTraitsList[i].value;
                int dropdownOValue = m_PersonalityTraitsList[o].value;

                TraitSO oppositeI = NPCManager.AllPersonalityTraits[dropdownIValue].ConflictTraits[0];
                int oppositeIValue = NPCManager.AllPersonalityTraits.IndexOf(oppositeI);

                if (dropdownIValue == dropdownOValue || dropdownOValue == oppositeIValue)
                {
                    m_PersonalityTraitsList[o].value = UnityEngine.Random.Range(0, NPCManager.AllPersonalityTraits.Count);
                }
            }
        }
    }

    public void SetDropDown(List<TraitSO> traits)
    {
        foreach(TMP_Dropdown dropdown in m_PersonalityTraitsList)
        {
            foreach(TraitSO trait in traits)
            {
                dropdown.options.Add(new TMP_Dropdown.OptionData() { text = trait.Name });
            }
        }
    }
}
