using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionPanel : MonoBehaviour
{
    public GameObject[] panel;
    public int idx = 0;

    public void BeforePanel()
    {
        if(idx is not 0)
        {
            panel[idx--].SetActive(false);
            panel[idx].SetActive(true);
        }
    }

    public void AfterPanel()
    {
        if (idx < panel.Length -1) {
            panel[idx++].SetActive(false);
            panel[idx].SetActive(true);
        }
    }

}
