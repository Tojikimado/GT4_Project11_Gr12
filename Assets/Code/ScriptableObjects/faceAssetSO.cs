using System.Collections.Generic;
using UnityEngine;

public enum AssetType
{
    Base,
    Eyes,
    Mouth,
    FrontHair,
    RearHair,
}

[CreateAssetMenu(fileName = "New face asset", menuName = "Face/New face asset")]
public class faceAssetSO : ScriptableObject
{
    [SerializeField] private Sprite m_AssetSprite;
    public Sprite AssetSprite => m_AssetSprite;

    [SerializeField] private AssetType m_AssetType;
    public AssetType Type => m_AssetType;


    [SerializeField] private List<TraitSO> m_Traits;
    public List<TraitSO> Traits => m_Traits;

}