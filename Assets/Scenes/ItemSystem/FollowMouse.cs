using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowMouse : MonoBehaviour
{
    private RectTransform rectTransform;

    [SerializeField]
    private Canvas canvas;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    private void Update()
    {
        rectTransform.position = Input.mousePosition / canvas.scaleFactor;
    }
    private void OnDisable()
    {
        rectTransform.position = new Vector3(-200f, 0, 0);
    }

}
