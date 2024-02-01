using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;
public class NPC : MonoBehaviour
{


    [SerializeField] private TextMeshProUGUI Name_Text;
    //[SerializeField] private TextMeshProUGUI DEscr;
    [SerializeField] private TextMeshProUGUI Trait_1;
    [SerializeField] private TextMeshProUGUI Trait_2;
    [SerializeField] private TextMeshProUGUI Trait_3;


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
        //Debug.Log("Nom: " + m_Name);
        Name_Text.text = m_Name;

        //Debug.Log("Description: " + m_Description);
       // DEscr.text = m_Description;
        //Debug.Log("Traits de personnalité:");
        
        foreach (TraitSO trait in m_PersonalityTraits)
        {
            //Debug.Log("- " + trait.Name);
        }
        Trait_1.text = m_PersonalityTraits[0].Name;
        Trait_2.text = m_PersonalityTraits[1].Name;
        Trait_3.text = m_PersonalityTraits[2].Name;
    }
}
