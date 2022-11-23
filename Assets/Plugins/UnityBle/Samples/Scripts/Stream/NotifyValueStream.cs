using System; // .NET 4.xモードで動かす場合は必須
using System.Collections;
using UnityEngine;
using UniRx;

// シングルトンクラスで実装
public class NotifyValueStream : MonoBehaviour
{
    private static NotifyValueStream _singleInstance = new NotifyValueStream();

    public static NotifyValueStream GetInstance() 
    { return _singleInstance; }

    private NotifyValueStream() {}

    // イベントを発行するインスタンス
    private Subject<string> notifyValueSubject = new Subject<string>();
    
    // 意図的に破棄するときに使用
    public static IDisposable iDisposable;

    void Start()
    {
        // OnDestroyのタイミングでSubjectがdisposeされる
        notifyValueSubject.AddTo(this);
    }

    // イベントの購読側を公開
    public IObservable<string> OnValueChanged
    { get { return notifyValueSubject; } }

    // イベントを発行
    public void SetValue(string value)
    { notifyValueSubject.OnNext(value); }

    // ストップ後の更新を防ぐために廃棄する
    public void OnDispose() {
        iDisposable.Dispose();
    }
}
