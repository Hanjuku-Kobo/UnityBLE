using System; // .NET 4.xモードで動かす場合は必須
using System.Collections;
using UnityEngine;
using UniRx;

// シングルトンクラスとして実装
public sealed class ReadValueStream : MonoBehaviour
{
    private static ReadValueStream _singleInstance = new ReadValueStream();

    public static ReadValueStream GetInstance()
    { return _singleInstance; }

    // コンストラクタ
    private ReadValueStream() {}

    // イベントを発行するインスタンス
    private Subject<string> readValueSubject = new Subject<string>();

    private void Start() 
    {
        // OnDestroyのタイミングでSubjectがdisposeされる
        readValueSubject.AddTo(this);
    }

    // イベントの購読側を公開
    public IObservable<string> OnValueChanged
    { get { return readValueSubject; } } 

    // イベントを発行
    public void SetValue(string value) 
    { readValueSubject.OnNext(value); }
}