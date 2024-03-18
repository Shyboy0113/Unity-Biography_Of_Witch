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
        canvasGroup.alpha = 0.6f; // �������� �巡���ϴ� ���� ���� ����
        canvasGroup.blocksRaycasts = false; // �巡�� �� Raycast ��� ����
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta; // ������ ��ġ ������Ʈ
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("Item Dropped");
        canvasGroup.alpha = 1.0f; // ��� �� ���� ���󺹱�
        canvasGroup.blocksRaycasts = true; // ��� �� Raycast �ٽ� ���
    }
}
