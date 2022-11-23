using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using BlePlugin.Ble;
using BlePlugin.Data;

public class ConnectButton : MonoBehaviour
{
    private const string ACTION_TEXT_NAME = "ConnectText";
    private const string STATE_TEXT_NAME = "ConnectStateText";

    // UIを更新するためのflag
    private FlagStream flagStream = FlagStream.GetInstance();

    private BleConnector connector = new BleConnector();

    void OnDestroy()
    {
        // アプリ終了時に切断する
        if (BleController.connectionStatus == ConnectionStatus.connected) {
            connector.Disconnect();
        }
    }

    public void OnClick() 
    {
        if (BleController.connectionStatus == ConnectionStatus.connected) {
            // 切断処理
            if (BleController.characteristicStatus == CharacteristicStatus.notify) {
                // Notifyであれば
                // ストリームの購読を停止する
                SampleCanvas.SetInteractive("BleReadButton", true);
                BleInteractor.StopNotification();
                NotifyValueStream.GetInstance().OnDispose();
            }
            
            connector.Disconnect();

            SampleCanvas.SetText(ACTION_TEXT_NAME, "Connect");
            SampleCanvas.SetText(STATE_TEXT_NAME, "disconnected");
            SampleCanvas.SetText("BleDataText", "No Data");
        } else {
            // 目的のデバイスを見つけていたら
            BleDevice device = BleDataCache.GetBleDevice();
            Debug.Log(device.address);
            connector.Connect(device.address);
            SampleCanvas.SetText(STATE_TEXT_NAME, "connecting");

            // デバイスと接続が確立した場合
            flagStream.OnConnectFlagChanged
                .Where(value => value)
                .Subscribe(value => 
                {
                    // UI変更
                    SampleCanvas.SetText(ACTION_TEXT_NAME, "Disconnect");
                    SampleCanvas.SetText(STATE_TEXT_NAME, "connected");
                });
        }
    }
}
