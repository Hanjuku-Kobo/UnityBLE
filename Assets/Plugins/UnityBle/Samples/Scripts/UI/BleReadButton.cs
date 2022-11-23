using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using BlePlugin.Ble;
using BlePlugin.Data;

public class BleReadButton : MonoBehaviour
{
    public void OnClick() 
    {
        if (BleController.connectionStatus == ConnectionStatus.connected) {
            // ストリームを監視して変更があればtextを変更させる
            ReadValueStream readValueStream = ReadValueStream.GetInstance();
            readValueStream.OnValueChanged.Subscribe(value => 
            {
                SampleCanvas.SetText("BleDataText", value);
            });

            // 接続していれば
            BleInteractor.ReadCharacteristic();
        } else {
            Debug.Log("デバイスを接続してください");
        }
    }
}
