using System.Collections.Generic;
using UnityEngine;

public enum TraitType
{
    Personality,
    Physical
}

[CreateAssetMenu(fileName = "New Trait", menuName = "Traits/New Trait")]
public class TraitSO : ScriptableObject
{
    [SerializeField] private string m_TraitName;
    public string Name => m_TraitName;

    [SerializeField] private string m_TraitDescription;
    public string Description => m_TraitDescription;

    [SerializeField] private TraitType m_TraitType;
    public TraitType Type => m_TraitType;

    [SerializeField] private List<TraitSO> m_ConflictTraits;
    public List<TraitSO> ConflictTraits => m_ConflictTraits;
}
