using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IStartedOperation
{
    /// <summary>
    /// ボタン等を押したときに呼ばれる処理
    /// </summary>
    void ProcessWhenPressed();

    /// <summary>
    /// ボタン等を押すのをやめたときに呼ばれる処理
    /// </summary>
    void ProcessWhenStopped();
}
