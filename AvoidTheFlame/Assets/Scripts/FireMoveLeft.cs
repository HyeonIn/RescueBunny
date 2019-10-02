using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireMoveLeft : MonoBehaviour
{
    // Start is called before the first frame update
    private const float moveSpeed = 1.7f;
    float ShootTimer = 1.1f; //처음엔 shootTimer를 TimeWait 보다 길게 해서 조건을 피함
    const float TimeWait = 1.0f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (ShootTimer > TimeWait){ //딜레이 확인
            if (transform.position.x < -2)
                transform.position = new Vector3(2.1f, -0.73f, 0);
            else
                transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
        }
        else
        {
            ShootTimer += Time.deltaTime; //딜레이 증가
        }
        
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag.Equals("Character")){ //부딪힌 객체가 캐릭터인지 검사합니다.
            ShootTimer = 0.0f; //타임 딜레이를 걸어줍니다.
            transform.position = new Vector3(2.1f, -0.73f, 0);
        }
    }
}
