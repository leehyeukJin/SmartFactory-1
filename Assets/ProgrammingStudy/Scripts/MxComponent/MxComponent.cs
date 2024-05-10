using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ActUtlType64Lib;
using TMPro; // MX Component v5 Library 사용


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
    public TMP_Text log;

    void Start()
    {
        mxComponent = new ActUtlType64();
        mxComponent.ActLogicalStationNumber = 1;
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

    private void OnDestroy()
    {
        OnDisconnectPLCBtnClkEvent();
    }
}
