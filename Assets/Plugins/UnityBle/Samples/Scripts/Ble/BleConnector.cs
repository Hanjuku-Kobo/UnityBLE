using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BlePlugin.Ble;
using BlePlugin.Data;

public class BleConnector : Callbacks {
    // 状態管理
    private FlagStream flagStream = FlagStream.GetInstance();

    public void Connect(string deviceAddress) 
    {
        if (BleController.connectionStatus != ConnectionStatus.connected) 
        {
            BleController.Connect(
                deviceAddress,
                OnConnect, 
                OnDisconnect, 
                OnError
            );
        }
    }

    public void Disconnect() 
    {
        if (BleController.connectionStatus != ConnectionStatus.disconnected) 
        {
            BleController.Disconnect(OnError);
        }
    }

    // callbacks
    private void OnConnect()
    {
        flagStream.SetConnectFlag(true);
        Debug.Log("connected");
    }
    private void OnDisconnect()
    {
        Debug.Log("disconnected");
    }
    private void OnError(string message)
    {
        Debug.Log(message);
    }
}