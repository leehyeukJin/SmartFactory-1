using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 키입력을 받으면 Move 함수를 호출한다.
// 특정 변수의 값을 통해 Death 애니메이션을 실행시킨다.
public class Player : MonoBehaviour
{
    public float speed = 3;
    public float runSpeed = 5;
    public float blendingDuration = 2;
    Animator anim;
    float currenTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>(); // 캐싱: 정보를 미리 저장
    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        if(h != 0 || v != 0)
        {
            float nowSpeed;
            if(Input.GetKey(KeyCode.LeftShift))
            {
                currenTime += Time.deltaTime;
                if (currenTime >= blendingDuration)
                    currenTime = blendingDuration;

                nowSpeed = runSpeed;

                anim.SetInteger("Status", 2);
                anim.SetFloat("Blend", currenTime / blendingDuration);
            }
            else
            {
                currenTime = 0;

                nowSpeed = speed;

                anim.SetInteger("Status", 1);
            }

            Vector3 direction = new Vector3(h, 0, v);
            transform.position += direction * Time.deltaTime * nowSpeed;
        }
        else
        {
            anim.SetInteger("Status", 0);
        }
    }
}
