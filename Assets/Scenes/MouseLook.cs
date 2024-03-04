using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float mouseSensitivity = 100.0f; // ���콺 �ΰ���
    public Transform playerBody; // �÷��̾� ��ü�� Transform ����

    private float xRotation = 0f;

    void Start()
    {
        // Ŀ���� ��װ� ����ϴ� (���� �÷��� �� Ŀ���� ȭ�� ������ ������ �ʵ���)
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // ���콺 �Է� �ޱ�
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // ���� ȸ���� ���� xRotation �� ���
        xRotation -= mouseY;
        // ȸ�� ������ -90������ 90�� ���̷� ����
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        // ī�޶��� ���� ȸ�� ����
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        // ĳ����(�÷��̾� ��ü)�� �¿� ȸ�� ����
        playerBody.Rotate(Vector3.up * mouseX);
    }
}
