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
    [SerializeField] private NPC MyNPC;
    [SerializeField] private GameObject DescObject;
    [SerializeField] private GameObject TraitBOX;


    public Vector3 SlotPos;


    private bool IsFocused = false;

    public NPCManager NPCManager { get; internal set; }

    public void UpdateNpcView()
    {
        m_NameText.text = MyNPC.Name;

        for(int i = 0; i< m_PersonalityTraitsList.Count;i++ )
        {
            int index = m_PersonalityTraitsList[i].options.FindIndex(o => o.text == MyNPC.PersonalityTraits[i].Name);
            m_PersonalityTraitsList[i].value = index;
        }

        for (int i = 0; i < m_PhysicalTraitsList.Count; i++)
        {
            m_PhysicalTraitsList[i].text = MyNPC.PhysicalTraits[i].Name;
        }
        m_DescriptionText.text = MyNPC.Description;
    }


    public void ShowthisNPC()
    {
        IsFocused = !IsFocused;
        UIManager.Instance.ShowSelected(gameObject, IsFocused);
        DescObject.SetActive(!DescObject.activeSelf);
        TraitBOX.SetActive(!TraitBOX.activeSelf);
    }

    public void OnDropdownValueChanged()
    {
        NPCManager.GenerateRandomDescription(m_NameText.text, gameObject.GetComponent<NPC>().PersonalityTraits);
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
