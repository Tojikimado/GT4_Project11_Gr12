using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{

    [SerializeField] private Vector3 m_EndPointLeft;
    [SerializeField] private Vector3 m_EndPointRight;
    [SerializeField] private float  m_Step;
    [SerializeField] private List<GameObject> m_NPCList;

    public void AddToNPCList(GameObject npc)
    {
        m_NPCList.Add(npc);
    }

    public void GoLeft()
    {
        foreach(var item in m_NPCList)
        {
            item.transform.localPosition = new Vector3(item.transform.localPosition.x + m_Step, item.transform.localPosition.y, item.transform.localPosition.z);
        }
    }
    public void GoRight()
    {
        foreach (var item in m_NPCList)
        {
            item.transform.localPosition = new Vector3(item.transform.localPosition.x - m_Step, item.transform.localPosition.y, item.transform.localPosition.z);
        }
    }


    public void CheckBorderLeft(GameObject item)
    {
        if(item.transform.position.x == -840.0f)
        {
            item.transform.position = new Vector3(840.0f,0,0);
        }
    }
    public void CheckBorderRight(GameObject item)
    {
        if (item.transform.position.x == 1120.0f)
        {
            item.transform.position= new Vector3(-560.0f, 0, 0);
        }
    }
}
