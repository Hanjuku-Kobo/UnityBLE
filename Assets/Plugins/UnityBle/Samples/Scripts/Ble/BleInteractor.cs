using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using BlePlugin.Ble;
using BlePlugin.Data;

// bleとの通信の役割を担う
public class BleInteractor : Callbacks {
    private static string serviceUUID = "3a77058c-dfa3-415a-ad71-1848ea0d5513";
    private static string readCharacteristic = "13a3e1a3-41f9-4a49-bdc3-c3de47deffa3";
    private static string writeCharacteristic = "write characteristic";
    private static string notifyCharacteristic = "13a3e1a3-41f9-4a49-bdc3-c3de47deffa3";

    // characteristicからデータを読みとる
    public static void ReadCharacteristic()  
    {
        if (BleController.connectionStatus != ConnectionStatus.connected) return;
        BleController.ReadCharacteristic(serviceUUID, readCharacteristic, OnRead, OnError);
    }

    // characteristicにデータを書き込む
    public static void WriteWithCharacteristic(byte[] writeValue) 
    {
        if (BleController.connectionStatus != ConnectionStatus.connected) return;
        BleController.WriteCharacteristic(serviceUUID, writeCharacteristic, writeValue, OnWrite, OnError);
    }

    // notifyを受け取れるように許可・設定をする
    public static void StartNotification() 
    {
        if (BleController.connectionStatus != ConnectionStatus.connected) return;
        BleController.StartNotification(serviceUUID, notifyCharacteristic, OnNotify, OnError);
    }

    // notifyを停止する
    public static void StopNotification()
    {
        if (BleController.connectionStatus != ConnectionStatus.connected) return;
        BleController.StopNotification(serviceUUID, notifyCharacteristic, OnError);
    }

    // callbacks
    private static void OnRead(string value)
    {
        Debug.Log("Value: "+value);
        // ストリームのデータを変更しイベントを発行する
        ReadValueStream readValueStream = ReadValueStream.GetInstance();
        readValueStream.SetValue(value);
    }
    private static void OnWrite()
    {
        // 書き込みが完了した際に呼ばれる
        Debug.Log("Write result: True");
    }
    private static void OnNotify(string value)
    {
        Debug.Log("Value: "+value);
        // ストリームのデータを変更しイベントを発行する
        NotifyValueStream notifyValueStream = NotifyValueStream.GetInstance();
        notifyValueStream.SetValue(value);
    }
    private static void OnError(string message)
    {
        Debug.Log(message);
    }
}