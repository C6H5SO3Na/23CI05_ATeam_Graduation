using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GimmickBase : MonoBehaviour
{
    //変数
    public int id { get; private set; }         // インスタンス毎に持つid
    private bool isRunning = false;             // すでにギミックが起動しているか(感圧板等でギミックが一度だけ動く場合に使用)

    //関数
    /// <summary>
    /// idの設定
    /// </summary>
    /// <param name="id"> インスタンス毎に持つid </param>
    public void SetID(int id)
    {
        this.id = id;
    }

    /// <summary>
    /// 一度感圧板等でギミックが起動されているか判定する(これを使うとき、ギミックの起動をやめたときの処理にMakeToLaunchable()を書く)
    /// </summary>
    /// <returns></returns>
    protected bool HasRunningOnce()
    {
        //押された瞬間はギミックを起動させる
        if(!isRunning)
        {
            isRunning = true;
            return true;
        }

        //すでに押した後だったら押すのをやめるまでギミックを起動しないようにする
        return false;
    }

    /// <summary>
    /// 感圧板等で一度だけ起動するギミックを再び起動できるようにする
    /// </summary>
    protected void MakeToLaunchable()
    {
        //ギミックが起動されているか確認する
        if (isRunning)
        {
            isRunning = false;
        }
    }
}
