using System;
using UnityEngine;
using UnityEngine.Android;
using BlePlugin.Data;

namespace BlePlugin.Util {
    public class JavaClassUtil : MonoBehaviour
    {
        public static void CallStaticWithoutResponse(string methodName, params object[] args) 
        {
            using var javaClass = new AndroidJavaClass(PathNames.RECEIVER_PATH);
            javaClass.CallStatic(methodName, args);
        }
    }
}