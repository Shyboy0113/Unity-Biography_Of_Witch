using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Book_Panel : MonoBehaviour
{

    public GameObject[] LeftButton;
    public GameObject[] RightButton;

    [SerializeField]
    private int _idx;

    private int _length;

    private void Start()
    {
        _idx = 0;
        _length = LeftButton.Length;
    }

    public void Setidx(int idx)
    {
        _idx = idx;

        if (idx <= 0)
        {
            _idx = 0;
        }

        if(idx >= _length-1)
        {
            _idx = _length-1;
        }

    }

    public void ChangeTagPosition(int direction)
    {
        if(direction < 0)
        {
            if (_idx > 0)
            {
                Debug.Log("Left");
                RightButton[_idx - 1].SetActive(true);
                LeftButton[_idx - 1].SetActive(false);
            }
        }
        else if (_idx < _length-1)
        {
            Debug.Log("Right");
            RightButton[_idx].SetActive(false);
            LeftButton[_idx].SetActive(true);
        }

        Setidx(_idx + direction);

    }


    public void LeftButtonClick(int idx)
    {
        Setidx(idx);

        for(int i = _length-1; i>=idx; i--)
        {
            LeftButton[i].SetActive(false);
            RightButton[i].SetActive(true);
        }
    }

    public void RightButtonClick(int idx)
    {
        Setidx(idx);

        for (int i = 0; i < idx; i++)
        {
            LeftButton[i].SetActive(true);
            RightButton[i].SetActive(false);
        }
    }


}
