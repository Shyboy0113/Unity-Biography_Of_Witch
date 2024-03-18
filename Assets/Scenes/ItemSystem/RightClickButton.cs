using UnityEngine;
using UnityEngine.EventSystems; // EventSystems 네임스페이스 필요

// MonoBehaviour를 상속받고 IPointerClickHandler 인터페이스를 구현
public class RightClickButton : MonoBehaviour, IPointerClickHandler
{
    // IPointerClickHandler 인터페이스에서 OnPointerClick 메서드 구현
    public void OnPointerClick(PointerEventData eventData)
    {
        // 우클릭을 감지
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            Debug.Log("우클릭됨!");
            // 여기에 우클릭 시 실행할 로직을 구현
            OnRightClick();
        }
    }
    


    // 우클릭 시 호출될 메서드
    private void OnRightClick()
    {
        // 우클릭 시 실행할 기능 구현
        Debug.Log("우클릭 버튼 기능 실행");
    }
}
