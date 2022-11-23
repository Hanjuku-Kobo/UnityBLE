using System;
using System.Collections.Generic;
using UnityEngine;
using BlePlugin.Util;
using BlePlugin.Data;

namespace BlePlugin.Ble {
    public class BleReceiver : MonoBehaviour
    {
        public static BleReceiver instance;

        public Action OnInitialize;
        public Action<string, string> OnScanCallback;
        public Action OnConnect;
        public Action OnDisconnect;
        public Action<string> OnRead;
        public Action OnWrite;
        public Action<string> OnNotify;
        public Action<string> OnError;

        public static BleReceiver GetOrCreateReceiver() 
        {
            if (instance == null) 
            {
                instance = new GameObject("BleReceiver").AddComponent<BleReceiver>();
                DontDestroyOnLoad(instance.gameObject);
            }
            return instance;
        }

        public void PluginMessage(string message)
        {
            var param = message.Split(',');
            if (param.Length == 0)
            {
                return;
            }

            switch (param[0])
            {
                case "InitCentral":
                    BleController.bleStatus = BleStatus.ready;
                    OnInitialize?.Invoke();
                    break;

                case "ScanCallback":
                    OnScanCallback?.Invoke(param[1], param[2]);
                    break;

                case "ConnectCallback":
                    BleController.connectionStatus = ConnectionStatus.connected;
                    OnConnect?.Invoke();
                    break;

                case "DisconnectCallback":
                    BleController.connectionStatus = ConnectionStatus.disconnected;
                    OnDisconnect?.Invoke();
                    break;

                case "DiscoverServiceCallback":
                    // データクラスを使用し、発見したcharacteristicやserviceUuidを管理する
                    Dictionary<AttProperty, bool> properties = BleUtil.GetAttProperties(param[3]);
                    Debug.Log("Property: "+param[3]);

                    DiscoveredCharacterisitc characterisitc = new DiscoveredCharacterisitc(
                        characteristicUuid: param[2], 
                        serviceId: param[1], 
                        isReadable: properties[AttProperty.Read], 
                        isWritableWithoutResponse: properties[AttProperty.WriteWithoutResponse],
                        isWritableWithResponse: properties[AttProperty.WriteWithResponse],
                        isNotifiable: properties[AttProperty.Notify],
                        isIndicatable: properties[AttProperty.Indicate]
                    );

                    BleController.discoveredService.setValue(param[1], characterisitc);
                    break;

                case "CharacteristicReadCallback":
                    OnRead?.Invoke(param[1]);
                    break;

                case "CharacteristicWriteCallback":
                    Debug.Log(param[1]);
                    OnWrite?.Invoke();
                    break;

                case "CharacteristicChangedCallback":
                    OnNotify?.Invoke(param[1]);
                    break;

                case "ErrorCallback":
                    // Initialize時のCallback
                    // Statusメッセージを更新する
                    if (param[1] == "unsupported") {
                        BleController.bleStatus = BleStatus.unsupported;
                    } else if (param[1] == "unauthorized") {
                        BleController.bleStatus = BleStatus.unauthorized;
                    } else if (param[1] == "locationServicesDisabled") {
                        BleController.bleStatus = BleStatus.locationServicesDisabled;
                    }

                    OnError?.Invoke(param[1]);
                    break;
                
                default:
                    Debug.Log("Defautlt: "+param[1]);
                    break;
            }
        }
    }
}