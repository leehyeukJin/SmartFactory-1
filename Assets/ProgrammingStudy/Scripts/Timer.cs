using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 시간의 경과를 특정 변수에 저장한다.

public class Timer : MonoBehaviour
{
    public float currentTime = 0;
    bool isFunction1Active = false;

    private void Start()
    {
        // StartCoroutine(CoExecuteSequnce());
        StartCoroutine("CoExecuteSequnce");
    }

    // Coroutine 함수: 여러가지 작업을 함께 진행하기위해 사용하는 C#의 기능
    IEnumerator CoExecuteSequnce()
    {
        yield return new WaitForSeconds(0.5f); // thread.sleep(2000)과 같은 2초 지연

        CheckTime();

        yield return new WaitForSeconds(0.5f);

        CheckTime();

        yield return null;
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;

        //if(currentTime > 2)
        //{
        //    CheckTime();
        //}
    }

    private void CheckTime()
    {
        print("경과 시간: " + currentTime); ;
    }
}
