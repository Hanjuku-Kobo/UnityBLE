namespace BlePlugin.Data {
    public enum BleStatus {
        // ステータスが決定していない
        unknown,

        // デバイスでBLEがサポートされていない
        unsupported,

        // このアプリでBLEの使用の許可がされていない
        unauthorized,

        // BLEがオフになっている
        poweredOff,

        // 位置情報サービスが無効
        locationServicesDisabled,

        // アプリでBLEがフル稼働している
        ready
    }
}