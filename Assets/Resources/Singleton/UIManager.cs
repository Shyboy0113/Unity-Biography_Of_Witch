using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{

    public GameObject menuUI;

    public void ToggleMenuUI(bool toggle)
    {
        menuUI.SetActive(toggle);    
    }

}
