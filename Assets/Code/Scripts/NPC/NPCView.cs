using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NPCView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI m_NameText;
    [SerializeField] private TextMeshProUGUI m_DescriptionText;
    [SerializeField] private List<TextMeshProUGUI> m_PersonalityTraitsList;
    [SerializeField] private List<TextMeshProUGUI> m_PhysicalTraitsList;
    [SerializeField] private NPC MyNPC;
    [SerializeField] private GameObject DescObject;
    [SerializeField] private GameObject TraitBOX;


    public Vector3 SlotPos;


    private bool IsFocused = false;

    public void UpdateNpcView()
    {
        m_NameText.text = MyNPC.Name;

        for(int i = 0; i< m_PersonalityTraitsList.Count;i++ )
        {
            m_PersonalityTraitsList[i].text = MyNPC.PersonalityTraits[i].Name;
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
}
