using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance {  get; private set; }
    
    [SerializeField] private List<GameObject> m_NPCList;
    [SerializeField] private List<Transform> m_PlaceHolder;
    [SerializeField] private TMP_InputField m_Seed;

    void Awake()
    {
        Instance = this;
    }
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
        m_Seed.text = tmp;
    }
    public void PlaceNPC()
    {
        for(int i = 0; i< m_NPCList.Count; i++)
        {
            m_NPCList[i].transform.localPosition = m_PlaceHolder[i].localPosition;
            m_NPCList[i].GetComponent<NPCView>().SlotPos = m_PlaceHolder[i].localPosition;
        }
    }

    public void ShowSelected(GameObject NPCSelected, bool value)
    {
        if (!value)
        {
            NPCSelected.transform.localPosition = NPCSelected.GetComponent<NPCView>().SlotPos;
            NPCSelected.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            foreach (GameObject g in m_NPCList)
            {
                if (g != NPCSelected)
                {
                    g.SetActive(true);
                }
            }
        }
        else
        {
            foreach(GameObject g in m_NPCList)
            {
                if(g != NPCSelected)
                {
                    g.SetActive(false);
                }
            }
            NPCSelected.transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
            NPCSelected.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        }
       
    }

    public void DestroyAllNpc()
    {
        foreach(GameObject g in m_NPCList)
        {
            //m_NPCList.Remove(g);
            Destroy(g);
        }
        m_NPCList.Clear();
    }
}
