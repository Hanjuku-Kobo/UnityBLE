using System.Collections.Generic;

namespace BlePlugin.Data {
    public class BleDevice {
        public readonly string name;
        public readonly string address;

        public BleDevice(
            string name,
            string address
        ) {
            this.name = name;
            this.address = address;
        }
    } 

    // Connection status of the BLE device.
    public enum ConnectionStatus {
        // Device is disconnected.
        disconnected,

        // A connection is being established.
        connecting,

        // Connected with Device.
        connected,

        // Device is being disconnected.
        disconnecting,
    }
}