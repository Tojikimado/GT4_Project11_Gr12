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
    [SerializeField] private List<faceAssetSO> Assets;
    void Start()
    {
        
    }
  
    public void GenerateFace(int _seed, NPC newNpc)
    {
        seed = _seed;
        refNpc = newNpc;
        TruncateSeed();
    }
    private void TruncateSeed()
    {
        for(int i = 0; i < Mathf.Abs(seed).ToString().Length; i++)
        {
            SepSeed.Add(Mathf.Abs(seed).ToString()[i]);
        }
    }
    private void GenerateBase()
    {
        List<faceAssetSO> BaseList = new List<faceAssetSO>();
        if (Assets == null)
        {
            BaseList = GenerateTmpList(AssetType.Base);
        }
    }
    private void GenerateMouth()
    {
        List<faceAssetSO> MouthList = new List<faceAssetSO>();
        if (Assets == null)
        {
            MouthList = GenerateTmpList(AssetType.Mouth);
        }
    }
    private List<faceAssetSO> GenerateTmpList(AssetType type)
    {
        List<faceAssetSO> TmpList = new List<faceAssetSO>();
        foreach (var assetSO in Assets)
        {
            if (assetSO.Type == type)
            {
                if (CheckIfTraitGood(assetSO))
                {
                    TmpList.Add(assetSO);
                }
            }
        }
        return TmpList;
    }
    private bool CheckIfTraitGood(faceAssetSO _assetSO)
    {
        foreach(var trait in _assetSO.Traits)
        {
            if(refNpc.PersonalityTraits.Contains(trait))
            {
                return true;
            }
        }
        return false;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
