using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using BlePlugin.Ble;
using BlePlugin.Data;

public class BleNotifyButton : MonoBehaviour
{
    public void OnClick() 
    {
        if (BleController.connectionStatus == ConnectionStatus.connected) {
            // 接続済みであれば
            if (BleController.characteristicStatus == CharacteristicStatus.notify) {
                SampleCanvas.SetInteractive("BleReadButton", true);

                // Notifyであれば設定を解除する
                BleInteractor.StopNotification();
            } else {
                // 現在Notifyの設定がされていなければ
                // ストリームを監視して通知に応じてtextを変更する
                NotifyValueStream notifyValueStream = NotifyValueStream.GetInstance();
                IDisposable iDisposable = notifyValueStream.OnValueChanged.Subscribe(value => 
                {
                    SampleCanvas.SetText("BleDataText", value);
                });
                NotifyValueStream.iDisposable = iDisposable;

                SampleCanvas.SetInteractive("BleReadButton", false);

                // Notifyを設定する
                BleInteractor.StartNotification();
            }
        } else {
            Debug.Log("デバイスを接続してください");
        }
    }
}
