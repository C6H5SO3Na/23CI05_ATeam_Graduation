using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISetGimmickInstance
{
    /// <summary>
    /// 起動させるオブジェクトのインスタンス設定
    /// </summary>
    /// <param name="targetObject"> 起動させるオブジェクト </param>
    void SetGimmickInstance(GameObject targetObject);
}
