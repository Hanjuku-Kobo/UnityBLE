using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using BlePlugin.Ble;
using BlePlugin.Util;
using BlePlugin.Data;

public class Main : MonoBehaviour, Callbacks
{
    // クラス呼び出し時に呼ばれる
    void Start()
    {
        // 位置情報の権限をリクエストする
        PermissionUtil.RequestLocation();

        // BLEの初期化
        BleController.Initialize(OnInitialize, OnError);
    }

    // アプリ終了時に呼ばれる
    void OnDestroy()
    {
        // BLEの終了処理をする
        BleController.Close(OnError);
    }

    // フレームの更新ごとに呼ばれる
    void Update() { }

    // callbacks
    private void OnInitialize()
    {
        // 初期化後にする処理を実装
        Debug.Log("initialize");
    }
    private void OnError(string errorMessage)
    {
        // BLEを初期化できない原因がエラーメッセージで返ってくる
        // 再び初期化をするかその他の処理を実装
        Debug.Log("Ble Initialized Error: "+errorMessage);
    }
}