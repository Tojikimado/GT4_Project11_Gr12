using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NPCView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI m_NameText;
    [SerializeField] private TextMeshProUGUI m_DescriptionText;
    [SerializeField] private List<TextMeshProUGUI> m_TraitsList;
    [SerializeField] private NPC MyNPC;

    public void UpdateNpcView()
    {
        m_NameText.text = MyNPC.Name;

        for(int i = 0; i< m_TraitsList.Count;i++ )
        {
            m_TraitsList[i].text = MyNPC.PersonalityTraits[i].Name;
        }
    }
}
