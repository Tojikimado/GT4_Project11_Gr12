using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class FaceGenerator : MonoBehaviour
{
    public int seed;
    public List<int> SepSeed = new List<int>();
    public NPC refNpc;
    public List<faceAssetSO> BaseList = new List<faceAssetSO>();
    public TraitSO TraitVieux;
    [SerializeField] private List<faceAssetSO> Assets;
    private int seedId;

    public void GenerateFace(int id,NPC newNpc, NpcFaceView newFace)
    {
        refNpc = newNpc;
        seedId = id;
        SpawnFace(newFace);
    }
    public void SetUp(int _seed)
    {
        seed = _seed;
        TruncateSeed();
    }
    private void TruncateSeed()
    {
        for(int i = 0; i < Mathf.Abs(seed).ToString().Length; i++)
        {
            SepSeed.Add((int)Char.GetNumericValue(Mathf.Abs(seed).ToString()[i]));
        }
    }
    private faceAssetSO GenerateBase()
    {
        if (Assets != null)
        {
            BaseList = GenerateTmpBaseList(AssetType.Base);

            if (refNpc.PhysicalTraits.Contains(TraitVieux))
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

        if (SepSeed[seedId] >= BaseList.Count)
        {
            return BaseList[0];
        }
        return BaseList[SepSeed[seedId]];
    }
    private faceAssetSO GenerateMouth()
    {
        List<faceAssetSO> MouthList = new List<faceAssetSO>();

        if (Assets != null)
        {
            MouthList = GenerateTmpList(AssetType.Mouth);
        }


        if (SepSeed[seedId] >= MouthList.Count)
        {
            return MouthList[0];
        }
        return MouthList[SepSeed[seedId]]; ;
    }
    private faceAssetSO GenerateEye()
    {
        List<faceAssetSO> EyesList = new List<faceAssetSO>();
        if (Assets != null)
        {
            EyesList = GenerateTmpList(AssetType.Eyes);
        }
        if (SepSeed[seedId] >= EyesList.Count)
        {
            return EyesList[0];
        }
        return EyesList[SepSeed[seedId]];
    }


    public void SpawnFace(NpcFaceView newFace)
    {
        newFace.Base.sprite = GenerateBase().AssetSprite;
        newFace.Mouth.sprite = GenerateMouth().AssetSprite;
        newFace.Eye.sprite = GenerateEye().AssetSprite;
    }



    private List<faceAssetSO> GenerateTmpList(AssetType type)
    {
        List<faceAssetSO> TmpList = new List<faceAssetSO>();
        foreach (var assetSO in Assets)
        {
            if (assetSO.Type == type)
            {
                if(CheckIfTraitGood(assetSO))
                {
                    TmpList.Add(assetSO);  
                }
                
            }
        }
        if(TmpList.Count == 0)
        {
            foreach (var assetSO in Assets)
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
        foreach (var assetSO in Assets)
        {
            if (assetSO.Type == type)
            {

                TmpList.Add(assetSO);


            }
        }
        return TmpList;
    }





    private bool CheckIfTraitGood(faceAssetSO _assetSO)
    {
        foreach (var trait in _assetSO.Traits)
        {
            if(refNpc.PersonalityTraits.Contains(trait))
            {
                return true;
            }
        }
        return false;
    }
}
