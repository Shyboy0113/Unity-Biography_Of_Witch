using UnityEngine;
using UnityEngine.EventSystems;

public class DragNDrop : MonoBehaviour, IPointerDownHandler, IDragHandler, IEndDragHandler
{
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("Item Drag Started");
        canvasGroup.alpha = 0.6f; // 아이템을 드래그하는 동안 투명도 변경
        canvasGroup.blocksRaycasts = false; // 드래그 중 Raycast 블록 해제
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta; // 아이템 위치 업데이트
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("Item Dropped");
        canvasGroup.alpha = 1.0f; // 드롭 시 투명도 원상복귀
        canvasGroup.blocksRaycasts = true; // 드롭 후 Raycast 다시 블록
    }
}
