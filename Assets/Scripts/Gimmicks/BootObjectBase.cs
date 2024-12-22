using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BootObjectBase : MonoBehaviour
{
    //変数
    public int id { get; private set; }   // インスタンス毎に持つid

    //関数
    /// <summary>
    /// idの設定
    /// </summary>
    /// <param name="id"> インスタンス毎に持つid </param>
    public void SetID(int id)
    {
        this.id = id;
    }
}
