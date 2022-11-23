using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BlePlugin.Ble {
    interface Callbacks 
    {
        void OnInitialize() {}

        void OnScan(string deviceName, string address) {}

        void OnConnect() {}

        void OnDisconnect() {}

        void OnRead(string value) {}

        void OnWrite() {}

        void OnNotify(string value) {}

        void OnError(string message) {}
    }
}