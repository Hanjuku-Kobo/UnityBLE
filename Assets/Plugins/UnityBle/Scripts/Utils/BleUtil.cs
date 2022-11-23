using System.Collections;
using System.Collections.Generic;
using BlePlugin.Data;

namespace BlePlugin.Util {
    public static class BleUtil {
        // characteristicの属性を返す
    public static Dictionary<AttProperty, bool> GetAttProperties(string strParam) 
    {
        int property = int.Parse(strParam);
        var properties = new Dictionary<AttProperty, bool>() {
            {AttProperty.Read, false},
            {AttProperty.WriteWithoutResponse, false},
            {AttProperty.WriteWithResponse, false},
            {AttProperty.Notify, false},
            {AttProperty.Indicate, false}
        };

        if (property-2 == 0 || (property-2)%4 == 0) {
            properties[AttProperty.Read] = true;
        } 
        if (property%4 == 0 || (property-2)%4 == 0) {
            properties[AttProperty.WriteWithoutResponse] = true;
        }
        if ((8 <= property && property <= 14) || (24 <= property && property <= 30) 
                || (40 <= property && property <= 46) || (56 <= property && property <= 62)) {
            properties[AttProperty.WriteWithResponse] = true;
        }
        if ((16 <= property && property <= 30) || (48 <= property && property <= 62)) {
            properties[AttProperty.Notify] = true;
        }
        if (32 <= property && property <= 62) {
            properties[AttProperty.Indicate] = true;
        }
        
        return properties;
        /*
        2:  "Read" 
        4:  "WriteWithoutResponse"
        6:  "Read,WriteWithoutResponse";
        8:  "WriteWithResponse";
        10: "Read,WriteWithResponse";
        12: "WriteWithoutResponse,WriteWithResponse";
        14: "Read,WriteWithoutResponse,WriteWithResponse";
        16: "Notify";
        18: "Read,Notify";
        20: "WriteWithoutResponse,Notify";
        22: "Read,WriteWithoutResponse,Notify";
        24: "WriteWithResponse,Notify";
        26: "Read,WriteWithResponse,Notify";
        28: "WriteWithoutResponse,WriteWithResponse,Notify";
        30: "Read,WriteWithoutResponse,WriteWithResponse,Notify";
        32: "Indicate";
        34: "Read,Indicate";
        36: "WriteWithoutResponse,Indicate";
        38: "Read,WriteWithoutResponse,Indicate";
        40: "WriteWithResponse,Indicate";
        42: "Read,WriteWithResponse,Indicate";
        44: "WriteWithoutResponse,WriteWithResponse,Indicate";
        46: "Read,WriteWithoutResponse,WriteWithResponse,Indicate";
        48: "Notify,Indicate";
        50: "Read,Notify,Indicate";
        52: "WriteWithoutResponse,Notify,Indicate";
        54: "Read,WriteWithoutResponse,Notify,Indicate";
        56: "WriteWithResponse,Notify,Indicate";
        58: "Read,WriteWithResponse,Notify,Indicate";
        60: "WriteWithoutResponse,WriteWithResponse,Notify,Indicate";
        62: "Read,WriteWithoutResponse,WriteWithResponse,Notify,Indicate";
        */
    }
    }
}