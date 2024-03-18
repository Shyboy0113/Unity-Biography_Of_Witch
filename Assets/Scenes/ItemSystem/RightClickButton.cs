using UnityEngine;
using UnityEngine.EventSystems; // EventSystems ���ӽ����̽� �ʿ�

// MonoBehaviour�� ��ӹް� IPointerClickHandler �������̽��� ����
public class RightClickButton : MonoBehaviour, IPointerClickHandler
{
    // IPointerClickHandler �������̽����� OnPointerClick �޼��� ����
    public void OnPointerClick(PointerEventData eventData)
    {
        // ��Ŭ���� ����
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            Debug.Log("��Ŭ����!");
            // ���⿡ ��Ŭ�� �� ������ ������ ����
            OnRightClick();
        }
    }
    


    // ��Ŭ�� �� ȣ��� �޼���
    private void OnRightClick()
    {
        // ��Ŭ�� �� ������ ��� ����
        Debug.Log("��Ŭ�� ��ư ��� ����");
    }
}
