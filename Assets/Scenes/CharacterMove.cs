using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMove : MonoBehaviour
{
    public float speed = 5.0f; // 캐릭터의 이동 속도
    public float rotationSpeed = 100.0f; // 캐릭터의 회전 속도

    void Update()
    {
        // 앞뒤로 이동
        float translation = Input.GetAxis("Vertical") * speed;
        // 좌우로 회전
        float rotation = Input.GetAxis("Horizontal") * rotationSpeed;

        translation *= Time.deltaTime;
        rotation *= Time.deltaTime;

        // 캐릭터를 이동시킨다
        transform.Translate(0, 0, translation);

        // 캐릭터를 회전시킨다
        transform.Rotate(0, rotation, 0);
    }


}
