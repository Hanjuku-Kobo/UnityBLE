using System;
using UnityEngine;
using UnityEngine.Android;
using BlePlugin.Data;
using BlePlugin.Util;

namespace BlePlugin.Ble {
    public class BleController : MonoBehaviour
    {
        private static BleReceiver receiver;

        public static BleStatus bleStatus;

        public static ConnectionStatus connectionStatus;

        public static DiscoveredService discoveredService;

        public static CharacteristicStatus characteristicStatus;

        // staticのコンストラクタ
        static BleController() 
        {
            // Ble receiverを初期化
            receiver = BleReceiver.GetOrCreateReceiver();

            bleStatus = BleStatus.unknown;
            connectionStatus = ConnectionStatus.disconnected;
            discoveredService = new DiscoveredService();
            characteristicStatus = CharacteristicStatus.unknown;
        }

        // 権限の確認や必要な変数の初期化を行う
        public static void Initialize(Action onInitialize, Action<string> onError) 
        {
            if (bleStatus != BleStatus.ready) {
                receiver.OnInitialize = onInitialize;
                receiver.OnError = onError;

                JavaClassUtil.CallStaticWithoutResponse("InitBle", "Central");
            }
        }

        // PeripheralをScanする
        public static bool StartScan(Action<string, string> onScanCallback, Action<string> onError) 
        {
            if (bleStatus == BleStatus.ready) {
                receiver.OnScanCallback = onScanCallback;
                receiver.OnError = onError;

                JavaClassUtil.CallStaticWithoutResponse("CallCentralMethod", "startScan");

                // 状態を更新するため
                return true;
            } else {
                return false;
            }
        }

        // Scanを停止する
        public static void StopScan(Action<string> onError) 
        {
            if (bleStatus != BleStatus.ready) return;
            receiver.OnError = onError;

            JavaClassUtil.CallStaticWithoutResponse("CallCentralMethod", "stopScan");
        }

        // デバイスへ接続する
        public static void Connect(string address, Action onConnect, Action onDisconnect, Action<string> onError) 
        {
            // 状態を変更する
            BleController.connectionStatus = ConnectionStatus.connecting;

            receiver.OnConnect = onConnect;
            receiver.OnDisconnect = onDisconnect;
            receiver.OnError = onError;
            JavaClassUtil.CallStaticWithoutResponse("CallCentralMethod", "connect", address);
        }

        // 切断する
        public static void Disconnect(Action<string> onError) 
        {
            // 状態を変更する
            BleController.connectionStatus = ConnectionStatus.disconnecting;

            receiver.OnError = onError;
            JavaClassUtil.CallStaticWithoutResponse("CallCentralMethod", "disconnect");
        }

        // 接続したデバイスのサービスを検索する => 発見したときCallbackを呼び出される
        public static void DiscoverService(Action<string> onError) 
        {
            receiver.OnError = onError;
        
            JavaClassUtil.CallStaticWithoutResponse("CallCentralMethod", "discoverService");
        }

        // キャラクタリスティック読み取り
        public static void ReadCharacteristic(string serviceUUID, string characteristicUUID, Action<string> onRead, Action<string> onError) 
        {
            receiver.OnRead = onRead;
            receiver.OnError = onError;

            // characteristicの状態をセットする
            characteristicStatus = CharacteristicStatus.read;
            JavaClassUtil.CallStaticWithoutResponse("CallCentralMethod", "readCharacteristic", serviceUUID, characteristicUUID);
        }

        // キャラクタリスティック書き込み + レスポンス
        public static void WriteCharacteristic(string serviceUUID, string charcteristicUUID, byte[] message, Action onWrite, Action<string> onError)
        {
            receiver.OnWrite = onWrite;
            receiver.OnError = onError;

            characteristicStatus = CharacteristicStatus.write;
            JavaClassUtil.CallStaticWithoutResponse("CallCentralMethod", "writeCharacteristic", serviceUUID, charcteristicUUID, message);
        }

        // notifyを受け取れるように許可・設定する
        public static void StartNotification(string serviceUUID, string charcteristicUUID, Action<string> onNotify, Action<string> onError)
        {
            receiver.OnNotify = onNotify;
            receiver.OnError = onError;

            characteristicStatus = CharacteristicStatus.notify;
            JavaClassUtil.CallStaticWithoutResponse("CallCentralMethod", "startNotification", serviceUUID, charcteristicUUID);
        }

        // notifyを停止する
        public static void StopNotification(string serviceUUID, string characteristicUUID, Action<string> onError)
        {
            receiver.OnError = onError;

            characteristicStatus = CharacteristicStatus.unknown;
            JavaClassUtil.CallStaticWithoutResponse("CallCentralMethod", "stopNotification", serviceUUID, characteristicUUID);
        }

        // bleを終了する
        public static void Close(Action<string> onError)
        {
            receiver.OnError = onError;

            JavaClassUtil.CallStaticWithoutResponse("CallCentralMethod", "close");
            bleStatus = BleStatus.poweredOff;
        }
    }
}