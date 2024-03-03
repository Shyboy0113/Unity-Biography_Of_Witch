using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Book_Panel : MonoBehaviour
{

    public GameObject[] panels;

    public void SetPanelsVisibility(int idx)
    {
        for (int i=0; i<5; i++)
        {
            if (i == idx)
            {
                panels[i].SetActive(true);
                continue;
            }

            panels[i].SetActive(false);
        }
    }


}
