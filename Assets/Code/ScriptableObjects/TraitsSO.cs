using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Trait", menuName = "Traits/New Trait")]
public class TraitSO : ScriptableObject
{
    [SerializeField] private string m_TraitName;
    public string Name => m_TraitName;

    [SerializeField] private string m_Description;
    public string Description => m_Description;

    [SerializeField] private List<TraitSO> m_ConflictTraits;
    public List<TraitSO> ConfilctTraits => m_ConflictTraits;
}
