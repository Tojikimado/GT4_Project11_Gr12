using UnityEngine;
using System.Collections.Generic;

public class NPC : MonoBehaviour
{
    [SerializeField] private Sprite m_Photo;
    public Sprite Photo
    {
        get { return m_Photo; }
        set { m_Photo = value; }
    }

    [SerializeField] private string m_Name;
    public string Name
    {
        get { return m_Name; }
        set { m_Name = value; }
    }

    [SerializeField] private  List<TraitSO> m_PersonalityTraits;
    public List<TraitSO> PersonalityTraits
    {
        get { return m_PersonalityTraits; }
        set { m_PersonalityTraits = value; }
    }

    [SerializeField] private string m_Description;
    public string Description
    {
        get { return m_Description; }
        set { m_Description = value; }
    }

    public void AfficherInfos()
    {
        Debug.Log("Nom: " + m_Name);
        Debug.Log("Description: " + m_Description);
        Debug.Log("Traits de personnalité:");
        foreach (TraitSO trait in m_PersonalityTraits)
        {
            Debug.Log("- " + trait.Name);
        }
    }
}
