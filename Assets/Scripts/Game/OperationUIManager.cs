using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class OperationUIManager : MonoBehaviour
{
    TextMeshProUGUI text;
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        if (GameManager.isMultiPlay)
        {
            text.text = "B:持つ/投げる\nA:ジャンプ\nY:操作キャラ\n切り替え\nX:置く\n左スティック\n:移動\nSTART:ポーズ";
        }
        else
        {
            text.text = "B:持つ/投げる\nA:ジャンプ\nX:置く\n左スティック\n:移動\nSTART:ポーズ";
        }
    }
}
