using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float mouseSensitivity = 100.0f; // 마우스 민감도
    public Transform playerBody; // 플레이어 몸체의 Transform 참조

    private float xRotation = 0f;

    void Start()
    {
        // 커서를 잠그고 숨깁니다 (게임 플레이 중 커서가 화면 밖으로 나가지 않도록)
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // 마우스 입력 받기
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // 상하 회전을 위한 xRotation 값 계산
        xRotation -= mouseY;
        // 회전 각도를 -90도에서 90도 사이로 제한
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        // 카메라의 상하 회전 적용
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        // 캐릭터(플레이어 몸체)의 좌우 회전 적용
        playerBody.Rotate(Vector3.up * mouseX);
    }
}
