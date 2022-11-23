using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BlePlugin.Ble;
using BlePlugin.Data;

public class BleScanner : Callbacks {
    // 状態管理
    public static bool isScanning = false;
    private FlagStream flagStream = FlagStream.GetInstance();

    public static List<BleDevice> discoveredDevices = new List<BleDevice>();

    // 目的のデバイスを検索するときに使用 - 設定用
    private string deviceName = "圧力計";
    private string deviceAddress = "device address";

    // デバイスをスキャンする
    public void StartScan()
    {
        discoveredDevices.Clear();
        flagStream.SetScanFlag(false);

        isScanning = BleController.StartScan(OnScan, OnError);

        Debug.Log("start scan: scanning -> "+isScanning);
    }
    // スキャンを停止する
    public void StopScan()
    {
        if (isScanning)
        {
            BleController.StopScan(OnError);
            isScanning = false;
        }
    }

    // callbacks
    private void OnScan(string name, string address)
    {
        // 発見したデバイスをストックし、ユーザーが選択する場合
        // 重複しているかしらべる
        bool duplication = false;
        for (int i=0; i<discoveredDevices.Count; i++) 
        {
            if (discoveredDevices[i].address == address) 
            {
                duplication = true;
            }
        }

        // 新しく発見したデバイスであれば配列に追加
        if (!duplication) 
        {
            Debug.Log("scan result: "+name+" , "+address);

            discoveredDevices.Add(
                new BleDevice(
                    name: name,
                    address: address
                )
            );

            // 目的のデバイスがある場合
            if (deviceName == name || deviceAddress == address) {
                BleDataCache.deviceName = name;
                BleDataCache.deviceAddress = address;
                flagStream.SetScanFlag(true);
            }
        }
    }
    private void OnError(string message)
    {
        Debug.Log(message);
    }
}