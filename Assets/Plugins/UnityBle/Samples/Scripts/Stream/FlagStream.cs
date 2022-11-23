using System.Collections;
using UnityEngine;
using UniRx;
using System; // .NET 4.xモードで動かす場合は必須

// シングルトンクラスとして実装
public sealed class FlagStream : MonoBehaviour
{
    private static FlagStream _singleInstance = new FlagStream();

    public static FlagStream GetInstance()
    {
        return _singleInstance;
    }

    // コンストラクタ
    private FlagStream() { }

    private void Start() 
    {
        // OnDestroyのタイミングでSubjectがdisposeされる
        scanFlagSubject.AddTo(this);
        connectFlagSubject.AddTo(this);
    }

    // イベントを発行するインスタンス
    private Subject<bool> scanFlagSubject = new Subject<bool>();
    private Subject<bool> connectFlagSubject = new Subject<bool>();

    // イベントの購読側を公開
    public IObservable<bool> OnScanFlagChanged
    {
        get { return scanFlagSubject; }
    }
    public IObservable<bool> OnConnectFlagChanged
    {
        get { return connectFlagSubject; }
    }

    // イベントを発行
    public void SetScanFlag(bool value) 
    {
        scanFlagSubject.OnNext(value);
    }
    public void SetConnectFlag(bool value) 
    {
        connectFlagSubject.OnNext(value);
    }
}