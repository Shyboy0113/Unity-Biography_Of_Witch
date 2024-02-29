using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public GameObject[] panels;

    public void SetVisible(int idx)
    {
        panels[idx].SetActive(true);
    }

    public void SetInvisible(int idx)
    {
        panels[idx].SetActive(false);
    }



    public void QuitGame() //게임 종료
    {
        Application.Quit();
    }
    
}
