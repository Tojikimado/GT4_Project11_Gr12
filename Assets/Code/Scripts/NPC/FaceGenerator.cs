using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class FaceGenerator : MonoBehaviour
{
    [SerializeField] 
    private List<faceAssetSO> m_Assets;
    private List<int> m_SeparatedSeed = new List<int>();
    public NPC RefNpc;
    public List<faceAssetSO> BaseList = new List<faceAssetSO>();
    public TraitSO TraitVieux;
    
    private int m_SeedId;

    public void GenerateFace(int id,NPC newNpc, NpcFaceView newFace)
    {
        RefNpc = newNpc;
        m_SeedId = id;
        SpawnFace(newFace);
    }
    public void SetUp(int _seed)
    {
        for (int i = 0; i < Mathf.Abs(_seed).ToString().Length; i++)
        {
            m_SeparatedSeed.Add((int)Char.GetNumericValue(Mathf.Abs(_seed).ToString()[i]));
        }
    }

    private faceAssetSO GenerateBase()
    {
        if (m_Assets != null)
        {
            BaseList = GenerateTmpBaseList(AssetType.Base);

            if (RefNpc.PhysicalTraits.Contains(TraitVieux))
            {
                for (int i = BaseList.Count - 1; i >= 0; i--)
                {
                    if (!BaseList[i].Traits.Contains(TraitVieux))
                    {
                        BaseList.RemoveAt(i);
                    }
                }
            }
            else
            {
                for (int i = BaseList.Count - 1; i >= 0; i--)
                {
                    if (BaseList[i].Traits.Contains(TraitVieux))
                    {
                        BaseList.RemoveAt(i);
                    }
                }
            }
        }

        if (m_SeparatedSeed[m_SeedId] >= BaseList.Count)
        {
            return BaseList[0];
        }
        return BaseList[m_SeparatedSeed[m_SeedId]];
    }

    private faceAssetSO GenerateMouth()
    {
        List<faceAssetSO> MouthList = new List<faceAssetSO>();

        if (m_Assets != null)
        {
            MouthList = GenerateTmpList(AssetType.Mouth);
        }


        if (m_SeparatedSeed[m_SeedId] >= MouthList.Count)
        {
            return MouthList[0];
        }
        return MouthList[m_SeparatedSeed[m_SeedId]]; ;
    }
    private faceAssetSO GenerateEye()
    {
        List<faceAssetSO> EyesList = new List<faceAssetSO>();
        if (m_Assets != null)
        {
            EyesList = GenerateTmpList(AssetType.Eyes);
        }
        if (m_SeparatedSeed[m_SeedId] >= EyesList.Count)
        {
            return EyesList[0];
        }
        return EyesList[m_SeparatedSeed[m_SeedId]];
    }
    private faceAssetSO GenerateRearHair()
    {
        List<faceAssetSO> hairList = new List<faceAssetSO>();
        if (m_Assets != null)
        {
            hairList = GenerateTmpBaseListViaPhysical(AssetType.RearHair);
        }
        if(hairList.Count == 0)
        {
            return null;
        }
        if (m_SeparatedSeed[m_SeedId] >= hairList.Count)
        {
            return hairList[0];
        }
        return hairList[m_SeparatedSeed[m_SeedId]];
    }
    private faceAssetSO GenerateFrontHair()
    {
        List<faceAssetSO> hairList = new List<faceAssetSO>();
        if (m_Assets != null)
        {
            hairList = GenerateTmpBaseList(AssetType.FrontHair);
        }
        if (hairList.Count == 0)
        {
            return null;
        }
        if (m_SeparatedSeed[m_SeedId] >= hairList.Count)
        {
            return hairList[0];
        }
        return hairList[m_SeparatedSeed[m_SeedId]];
    }

    public void SpawnFace(NpcFaceView newFace)
    {
        newFace.Base.sprite = GenerateBase().AssetSprite;
        newFace.Mouth.sprite = GenerateMouth().AssetSprite;
        newFace.Eye.sprite = GenerateEye().AssetSprite;
        faceAssetSO tmp = GenerateRearHair();
        if (tmp != null)
        {
            newFace.RearHair.sprite = tmp.AssetSprite;
            tmp = GenerateFrontHair(); 
            newFace.FrontHair.sprite = tmp.AssetSprite;
            newFace.RearHair.color = new Color32(255, 255, 225, 225);
            newFace.FrontHair.color = new Color32(255, 255, 225, 225); 
        }
            
    }



    private List<faceAssetSO> GenerateTmpList(AssetType type)
    {
        List<faceAssetSO> TmpList = new List<faceAssetSO>();
        foreach (var assetSO in m_Assets)
        {
            if (assetSO.Type == type)
            {
                if(CheckIfPersonalityTraitsGood(assetSO))
                {
                    TmpList.Add(assetSO);  
                }
            }
        }
        if(TmpList.Count == 0)
        {
            foreach (var assetSO in m_Assets)
            {
                if (assetSO.Type == type)
                {
                    TmpList.Add(assetSO);
                }
            }
        }
        return TmpList;
    }
    private List<faceAssetSO> GenerateTmpBaseList(AssetType type)
    {
        List<faceAssetSO> TmpList = new List<faceAssetSO>();
        foreach (var assetSO in m_Assets)
        {
            if (assetSO.Type == type)
            {
                TmpList.Add(assetSO);
            }
        }
        return TmpList;
    }
    private List<faceAssetSO> GenerateTmpBaseListViaPhysical(AssetType type)
    {
        List<faceAssetSO> TmpList = new List<faceAssetSO>();
        foreach (var assetSO in m_Assets)
        {
            if (assetSO.Type == type)
            {
                if(CheckIfPhysicalTraitsGood(assetSO))
                {
                    TmpList.Add(assetSO);
                }
            }
        }
        return TmpList;
    }


    private bool CheckIfPhysicalTraitsGood(faceAssetSO _assetSO)
    {
        foreach (var trait in _assetSO.Traits)
        {
            if (RefNpc.PhysicalTraits.Contains(trait))
            {
                return true;
            }
        }
        return false;
    }
    private bool CheckIfPersonalityTraitsGood(faceAssetSO _assetSO)
    {
        foreach (var trait in _assetSO.Traits)
        {
            if(RefNpc.PersonalityTraits.Contains(trait))
            {
                return true;
            }
        }
        return false;
    }
}
