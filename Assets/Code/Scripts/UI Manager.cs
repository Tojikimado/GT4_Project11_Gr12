using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> m_NPCList;
    [SerializeField] private List<Transform> m_placeHolder;
    [SerializeField] private TMP_InputField seed;
    public void AddToNPCList(GameObject npc)
    {
        m_NPCList.Add(npc);
    }

    public void RefreshAllNPC()
    {
        foreach(var t in m_NPCList)
        {
            NPCView temp = t.GetComponent<NPCView>();
            temp.UpdateNpcView();
        }
    }
    public void UpdateSeed(string tmp)
    {
        seed.text = tmp;
    }
    public void PlaceNPC()
    {
        for(int i = 0; i< m_NPCList.Count; i++)
        {
            m_NPCList[i].transform.localPosition = m_placeHolder[i].localPosition;
        }
    }

}
