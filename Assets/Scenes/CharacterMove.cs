using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMove : MonoBehaviour
{
    public float speed = 5.0f; // ĳ������ �̵� �ӵ�
    public float rotationSpeed = 100.0f; // ĳ������ ȸ�� �ӵ�

    void Update()
    {
        // �յڷ� �̵�
        float translation = Input.GetAxis("Vertical") * speed;
        // �¿�� ȸ��
        float rotation = Input.GetAxis("Horizontal") * rotationSpeed;

        translation *= Time.deltaTime;
        rotation *= Time.deltaTime;

        // ĳ���͸� �̵���Ų��
        transform.Translate(0, 0, translation);

        // ĳ���͸� ȸ����Ų��
        transform.Rotate(0, rotation, 0);
    }


}
