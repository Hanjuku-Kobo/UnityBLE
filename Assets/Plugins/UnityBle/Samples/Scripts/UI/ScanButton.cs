using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using BlePlugin.Ble;

public class ScanButton : MonoBehaviour
{
    private const string ACTION_TEXT_NAME = "ScanText";
    private const string STATE_TEXT_NAME = "ScanStateText";

    // UIを更新するためのflag
    private FlagStream flagStream = FlagStream.GetInstance();

    // BLE関連
    private BleScanner scanner = new BleScanner();

    public void OnClick()
    {
        if (BleScanner.isScanning) {
            scanner.StopScan();

            // scan停止
            SampleCanvas.SetText(ACTION_TEXT_NAME, "Start Scan");
            SampleCanvas.SetText(STATE_TEXT_NAME, "undiscovered");
        } else {
            scanner.StartScan();
            
            // scan開始
            SampleCanvas.SetText(ACTION_TEXT_NAME, "Stop Scan");
            SampleCanvas.SetText(STATE_TEXT_NAME, "scanning");
            
            // 目的のデバイスを発見した場合の処理
            flagStream.OnScanFlagChanged
                .Where(value => value)
                .Subscribe(value => 
                {
                    // scan停止
                    scanner.StopScan();
                    SampleCanvas.SetText(ACTION_TEXT_NAME, "Start Scan");
                    SampleCanvas.SetText(STATE_TEXT_NAME, "discovered");
                    SampleCanvas.SetInteractive("ConnectButton", true);

                    SampleCanvas.SetText("DeviceName", BleDataCache.deviceName);
                    SampleCanvas.SetText("DeviceAddress", BleDataCache.deviceAddress);
                });
        }
    }
}
