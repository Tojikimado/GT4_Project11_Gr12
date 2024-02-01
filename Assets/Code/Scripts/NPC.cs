using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;
public class NPC : MonoBehaviour
{
    [SerializeField] private Sprite m_Photo;
    public Sprite Photo
    {
        get { return m_Photo; }
        set { m_Photo = value; }
    }

    [SerializeField] private string m_NPCName;
    public string Name
    {
        get { return m_NPCName; }
        set { m_NPCName = value; }
    }

    [SerializeField] private  List<TraitSO> m_PersonalityTraits;
    public List<TraitSO> PersonalityTraits
    {
        get { return m_PersonalityTraits; }
        set { m_PersonalityTraits = value; }
    }

    [SerializeField] private List<TraitSO> m_PhysicalTraits;
    public List<TraitSO> PhysicalTraits
    {
        get { return m_PhysicalTraits; }
        set { m_PhysicalTraits = value; }
    }

    [SerializeField] private string m_Description;
    public string Description
    {
        get { return m_Description; }
        set { m_Description = value; }
    }
}
