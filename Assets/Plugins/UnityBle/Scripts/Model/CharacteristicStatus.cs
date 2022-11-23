namespace BlePlugin.Data {
    public enum CharacteristicStatus {
        // ステータスが決定していない
        unknown,

        // Readしている
        read,

        // Writeしている
        write,

        // notifyを設定している状態
        notify
    }
}
