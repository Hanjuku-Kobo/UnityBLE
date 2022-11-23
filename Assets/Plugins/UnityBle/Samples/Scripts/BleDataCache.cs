using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BlePlugin.Data;

public class BleDataCache : MonoBehaviour
{
    public static string deviceName;

    public static string deviceAddress;

    public static BleDevice GetBleDevice() 
    {
        return new BleDevice(
            name: deviceName,
            address: deviceAddress
        );
    }
}
