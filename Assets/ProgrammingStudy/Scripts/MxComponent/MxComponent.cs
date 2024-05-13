﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ActUtlType64Lib; // MX Component v5 Library 사용
using TMPro;
using UnityEngine.UI;


/// <summary>
/// OpenPLC, ClosePLC
/// </summary>
public class MxComponent : MonoBehaviour
{
    enum Connection
    {
        Connected,
        Disconnected,
    }

    ActUtlType64 mxComponent;
    Connection connection = Connection.Disconnected;
    public Button readButton;
    public Button readBlockButton;
    public TMP_InputField deviceInput;
    public TMP_InputField valueInput;
    public TMP_InputField deviceBlockInput;
    public TMP_InputField valueBlockInput;
    public TMP_Text log;

    public MeshRenderer redLamp;
    public MeshRenderer yellowLamp;
    public MeshRenderer greenLamp;

    public Transform cylinderA;
    public Transform cylinderA_start;
    public Transform cylinderA_end;
    public Transform cylinderB;
    public Transform cylinderB_start;
    public Transform cylinderB_end;
    public int Y0 = 0;
    public int Y1 = 0;
    public int Y2 = 0;
    public int Y3 = 0;

    void Start()
    {
        mxComponent = new ActUtlType64();
        mxComponent.ActLogicalStationNumber = 1;

        //readButton.onClick.AddListener(OnReadDataBtnClkEvent);
        readButton.onClick.AddListener(()=> OnReadDataBtnClkEvent(deviceInput, valueInput));

        redLamp.material.color = Color.black;
        yellowLamp.material.color = Color.black;
        greenLamp.material.color = Color.black;

        StartCoroutine(CoListener());
        StartCoroutine(CoRunMPS());
    }

    private void Update()
    {   /* // 신호등
        int valueRed = GetDevice("M100");
        if (valueRed != 0)
        {
            redLamp.material.color = Color.red;
            yellowLamp.material.color = Color.black;
            greenLamp.material.color = Color.black;
        }

        int valueYellow = GetDevice("M101");
        if (valueYellow != 0)
        {
            redLamp.material.color = Color.black;
            yellowLamp.material.color = Color.yellow;
            greenLamp.material.color = Color.black;
        }

        int greenValue = GetDevice("M102");
        if (greenValue != 0)
        {
            redLamp.material.color = Color.black;
            yellowLamp.material.color = Color.black;
            greenLamp.material.color = Color.green;
        }*/

        /* // 실습 1
        int motor0 = GetDevice("M0");
        int timer0 = GetDevice("T0");
        int motor1 = GetDevice("M1");
        int timer1 = GetDevice("T1");

        log.text = $"motor0; {motor0}, Timer0; {timer0}\nmotor1; {motor1}, Timer1; {timer1}";
        */

        //실습2
        int redlampvalue = GetDevice("Y0");
        int yellowlampvalue = GetDevice("Y1");
        int greenlampvalue = GetDevice("Y2");

        if(redlampvalue == 1)
        {
            redLamp.material.color = Color.red;
        }

        if (yellowlampvalue == 1)
        {
            redLamp.material.color = Color.yellow;
        }

        if (greenlampvalue == 1)
        {
            redLamp.material.color = Color.green;
        }

    }

    int GetDevice(string device)
    {
        if (connection == Connection.Connected)
        {
            int Data = 0;
            int returnValue = mxComponent.GetDevice(device, out Data);

            if (returnValue != 0)
                print(returnValue.ToString("X"));

            return Data;
        }
        else
            return 0;
    }

    public void OnConnectPLCBtnClkEvent()
    {
        if(connection == Connection.Disconnected)
        {
            int returnValue = mxComponent.Open();
            if(returnValue == 0)
            {
                print("연결에 성공하였습니다.");

                connection = Connection.Connected;
            }
            else
            {
                print("연결에 실패했습니다. returnValue: 0x" + returnValue.ToString("X")); // 16진수로 변경
            }
        }
        else
        {
            print("연결 상태입니다.");
        }
    }

    public void OnDisconnectPLCBtnClkEvent()
    {
        if(connection == Connection.Connected)
        {
            int returnValue = mxComponent.Close();
            if (returnValue == 0)
            {
                print("연결 해지되었습니다.");
                connection = Connection.Disconnected;
            }
            else
            {
                print("연결 해지에 실패했습니다. returnValue: 0x" + returnValue.ToString("X")); // 16진수로 변경
            }
        }
        else
        {
            print("연결 해지 상태입니다.");
        }
    }

    public void OnReadDataBtnClkEvent()
    {
        if(connection == Connection.Connected) 
        {
            int data = 0;
            int returnValue = mxComponent.GetDevice("M0", out data);
            if (returnValue != 0)
                print("returnValue: 0x" + returnValue.ToString("X"));
            else
                log.text = $"M0: {data}";
        }
    }

    public void OnReadDataBtnClkEvent(TMP_InputField deviceInput, TMP_InputField deviceValue)
    {
        if (connection == Connection.Connected)
        {
            int data = 0;
            int returnValue = mxComponent.GetDevice(deviceInput.text, out data);
            if (returnValue != 0)
                print("returnValue: 0x" + returnValue.ToString("X"));
            else
            {
                log.text = $"{deviceInput.text}: {data.ToString("X")}";
                deviceValue.text = data.ToString("X");
            }
        }
    }

    public void OnWriteDataBtnClkEvent()
    {
        if (connection == Connection.Connected)
        {
            int returnValue = mxComponent.SetDevice("M0", 1);
            if (returnValue != 0)
                print("returnValue: 0x" + returnValue.ToString("X"));
            else
                log.text = $"M0: 1";
        }
    }

    public void OnWriteDataBtnClkEvent(TMP_InputField deviceInput, TMP_InputField deviceValue)
    {
        print("ABC");

        if (connection == Connection.Connected)
        {
            int value = int.Parse(deviceValue.text);
            int returnValue = mxComponent.SetDevice(deviceInput.text, value);
            if (returnValue != 0)
                print("returnValue: 0x" + returnValue.ToString("X"));
            else
                log.text = $"{deviceInput.text}: {value}";
        }
    }

    public void OnReadDataBlockBtnClkEvent(TMP_InputField deviceInput, TMP_InputField deviceValue)
    {
        print("Hello");

        if(connection == Connection.Connected)
        {
            short data;

            int returnValue = mxComponent.ReadDeviceRandom2(deviceInput.text, 1, out data);
            if (returnValue != 0)
                print("returnValue: 0x" + returnValue.ToString("X"));
            else
                deviceValue.text = $"{deviceInput.text}: {data}";
        }
    }

    // 실습2. 공급 실린더(A) 전진, 후진 후, 송출 실린더(B) 전진, 후진
    // 조건: 모든 시퀀스 작동시간은 1초
    // Vector3.Lerp 사용
    IEnumerator MoveCylinder(Transform cylinder, Vector3 positionA, Vector3 positionB, float duration)
    {
        float currentTime = 0;

        while (true)
        {
            currentTime += Time.deltaTime;

            if (currentTime >= duration)
            {
                currentTime = 0;
                break;
            }

            cylinder.position = Vector3.Lerp(positionA, positionB, currentTime / duration);

            yield return new WaitForSeconds(Time.deltaTime);
        }
    }

    IEnumerator CoListener()
    {
        while(true)
        {
            Y0 = GetDevice("Y0");
            Y1 = GetDevice("Y1");
            Y2 = GetDevice("Y2");
            Y3 = GetDevice("Y3");

            yield return new WaitForSeconds(Time.deltaTime);
        }
    }

    IEnumerator CoRunMPS()
    {
        while(true)
        {
            if(Y0 == 1)
            {
                yield return MoveCylinder(cylinderA, cylinderA_start.position, cylinderA_end.position, 1);
            }

            if(Y1 == 1)
            {
                yield return MoveCylinder(cylinderA, cylinderA_end.position, cylinderA_start.position, 1);
            }

            if(Y2 == 1)
            {
                yield return MoveCylinder(cylinderB, cylinderB_start.position, cylinderB_end.position, 1);
            }

            if(Y3 == 1)
            {
                yield return MoveCylinder(cylinderB, cylinderB_end.position, cylinderB_start.position, 1);
            }

            yield return new WaitForSeconds(Time.deltaTime);
        } 
    }

    private void OnDestroy()
    {
        OnDisconnectPLCBtnClkEvent();
    }
}
