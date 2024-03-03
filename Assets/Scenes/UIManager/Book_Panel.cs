using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Book_Panel : MonoBehaviour
{

    public GameObject[] panels;

    public Image[] buttonColor;

    public void SetPanelsVisibility(int idx)
    {
        for (int i=0; i<5; i++)
        {
            if (i == idx)
            {
                panels[i].SetActive(true);

                //눌린 버튼의 투명도 낮춤
                Color pressedColor = buttonColor[i].color;
                pressedColor.a = 10f / 255f;
                buttonColor[i].color = pressedColor;

                continue;
            }

            //안 눌린 버튼의 투명도 원래대로
            Color relasedColor = buttonColor[i].color;
            relasedColor.a = 50f / 255f;
            buttonColor[i].color = relasedColor;

            panels[i].SetActive(false);
        }
    }


}
