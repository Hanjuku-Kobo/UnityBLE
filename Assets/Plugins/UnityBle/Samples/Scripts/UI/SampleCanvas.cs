using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SampleCanvas : MonoBehaviour
{
    private static Canvas _canvas;
    void Start()
    {   
        // コンポーネントを保持
        _canvas = GetComponent<Canvas>();
    }

    // Canvas内要素のTextを変更する
    public static void SetText(string name, string value) 
    {
        // 子の要素をたどる
        foreach(Transform child in _canvas.transform) {
            // 名前が一致
            if (child.name == name) {
                Text text = child.GetComponent<Text>();
                text.text = value;

                return;
            }
        }
        // 見つからなかった場合はログをはく
        Debug.LogWarning("Not found objname:"+name);
    }

    // Canvas内要素のButtonの有効・無効を変更する
    public static void SetInteractive(string name, bool value) 
    {
        // この要素をたどる
        foreach(Transform child in _canvas.transform) {
            // 名前が一致
            if (child.name == name) {
               Button button =  child.GetComponent<Button>();
               // 有効・無効フラグを設定する
               button.interactable = value;
               return;
            }
        }
        // 見つからなかった場合はログをはく
        Debug.LogWarning("Not found objname:"+name);
    }
}
