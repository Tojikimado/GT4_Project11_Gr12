using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{


    public Vector3 EndPointLeft;
    public Vector3 EndPointRight;
    public float  step ;
    public List<GameObject> NPCLIST;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GoLeft()
    {
        foreach(var item in NPCLIST)
        {
            item.transform.localPosition = new Vector3(item.transform.localPosition.x + step, item.transform.localPosition.y, item.transform.localPosition.z);
        }
    }
    public void GoRight()
    {
        foreach (var item in NPCLIST)
        {
            item.transform.localPosition = new Vector3(item.transform.localPosition.x - step, item.transform.localPosition.y, item.transform.localPosition.z);
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
