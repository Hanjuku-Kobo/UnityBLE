# UnityBLE - Android Bluetooth LE for Unity
UnityのAndroidアプリでBLEを扱うためのAssetsPackageです

## Installing
1. リリースページである[こちら](https://github.com/Hanjuku-Kobo/UnityBLE_for_Android/releases)から、最新のバージョンの UnityBLE.unitypackage をクリックしてダウンロードしてください。  

2. Assets > Import Package > Custom Package の順に選択をします。ファイルブラウザーが表示され、.unitypackage ファイルを見つけるように表示されます。

3. ファイルブラウザーで、 先ほどダウンロードした UnityBLE.unitypackage を選択して Open をクリックします。Import Unity Package ウィンドウに、すでに選択され、インストールの準備ができているパッケージ内のすべてのアイテムが表示されます。

4. すべてのファイルを選択した状態で、 Import をクリックします。Unity はインポートされたアセットパッケージのコンテンツを Assets フォルダーに保存するため、Project ウィンドウからそれらにアクセスできます。

## Usage
### Initialization
使用する権限をリクエストする:  
```csharp
// 位置情報の権限をリクエストする
PermissionUtil.RequestLocation();
```

BLEを初期化する:  
```csharp
BleController.Initialize(OnInitialize, OnError);

// callbacks
private void OnInitialize() 
{ 
    // 初期化後にする処理を実装
}
private void OnError(string errorMessage) 
{ 
    // BLEを初期化できない原因がエラーメッセージで返ってくる
    // 再び初期化をするかその他の処理を実装
}   
```

### Device discovery
デバイスをScanする:
```csharp
isScanning = BleController.StartScan(OnScan, OnError);

// callbacks
private void OnScan(string name, string address)
{   
    // 発見したデバイスを管理する
    discoveredDevices.Add(
        new BleDevice(
            name: name,
            address: address
        )
    );
}
```

Scanを停止する:
```csharp
BleController.StopScan(OnError);
```

### Establishing connection
デバイスとの接続を確立する:
```csharp
BleController.Connect(
    deviceAddress,
    OnConnect, 
    OnDisconnect, 
    OnError
);

// callbacks
private void OnConnect()
{
    // 接続後の処理を実装する
}
```

### Disconnect the connection
デバイスとの接続を解除する:
```csharp
BleController.Disconnect(OnError);
```

### Interact with the device
デバイスのGattServiceやcharacteristicを取得する:
```csharp
BleController.DiscoverService(OnDiscoverService, OnError);

// callbacks
private void OnDiscoverService(string serviceUUID, string characteristicUUID, string property)
{
    // データクラスを使用し、発見したcharacteristicやserviceUuidを管理する
    DiscoveredCharacterisitc characterisitc = new DiscoveredCharacterisitc(
        characteristicUuid: characteristicUUID, 
        serviceId: serviceUUID, 
        isReadable: properties[AttProperty.Read], 
        isWritableWithoutResponse: properties[AttProperty.WriteWithoutResponse],
        isWritableWithResponse: properties[AttProperty.WriteWithResponse],
        isNotifiable: properties[AttProperty.Notify],
        isIndicatable: properties[AttProperty.Indicate]
    );

    discoveredService.setValue(serviceUUID, characterisitc);
}
```

#### Read characteristic  
デバイスからデータを読み取る:
```csharp
BleController.ReadCharacteristic(serviceUUID, readCharacteristic, OnRead, OnError);

// callbacks
private void OnRead(string value) 
{
    // value <= デバイスから送信されたデータ
}
```

#### Write without response
書き込み処理を行った後、レスポンスが返ってこない。一般的に想像されるのはこのパターンだろう。しかし、プログラム上である場合は書き込み処理の処理状況を把握したいケースが多く存在する。そのため、このメソッドはあまり使用されることがない。  
現時点では未実装だが、そのうち実装する予定です。

#### Write with response
書き込み処理が完了した時点で、デバイスからレスポンスが返ってくる。  

デバイスにデータを送信する:
```csharp
BleController.WriteCharacteristic(serviceUUID, writeCharacteristic, writeValue, OnWrite, OnError);

// callbacks
private void OnWrite()
{
    // 書き込みが完了した際に呼ばれる
    // 書き込みの後に行う処理を実装する
}
```

#### Subscribe to characteristic
デバイスは管理するデータに変化があった場合に変更通知を送信するので、その通知を受けられるように設定をする。  

データの変更通知を購読する:
```csharp
BleController.SetNotification(serviceUUID, notifyCharacteristic, OnNotify, OnError);

// callbacks
private void OnNotify(string value)
{
    // 変更通知があった場合の処理を実装する
}
```

### End processing
アプリ終了時に、システムが適切にリソースを解放できるようにする:
```csharp
BleController.Close(OnError);
```

## License
This project is licensed under the MIT License - see the [LICENSE.md](https://github.com/Hanjuku-Kobo/UnityBLE_for_Android/blob/main/LICENSE) file for details

## Acknowledgments
### Inspiration
このAssetPackageは [Flutter reactive BLE library](https://github.com/PhilipsHue/flutter_reactive_ble) を参考に開発しています。