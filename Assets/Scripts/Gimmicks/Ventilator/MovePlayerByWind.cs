using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayerByWind : GimmickBase, IStartedOperation
{
    float[] windPower = {1f, 2f, 3f};       // プレイヤーを動かす風の力 左から弱、中、強の三種類
    int     windPowerIndex = 0;             // windPowerの要素アクセス用
    bool    shouldProcessGimmick = true;    // 風を吹かせるか
    Vector3 directionBlowsWind;             // 風が吹く方向

    // Start is called before the first frame update
    void Start()
    {
        //風が吹く方向を取得
        directionBlowsWind = transform.forward;
    }

    void OnTriggerStay(Collider other)
    {
        //風がプレイヤーに当たっていたら、プレイヤーを動かす値を渡す
        if(shouldProcessGimmick)
        {
            if(other.CompareTag("Player"))
            {
                //他の風と同時に当たっている場合、風の数値が強い方を優先する

            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        //風でプレイヤーが動かないようにする

    }

    public void ProcessWhenPressed()
    {
        //一度処理したら感圧板等を押し続けている場合は押すのをやめるまで処理しないようにする
        if (HasRunningOnce())
        {
            //風を吹かせるのをやめる
            shouldProcessGimmick = false;
        }
    }

    public void ProcessWhenStopped()
    {
        //また風を吹かせる
        shouldProcessGimmick = true;

        //また感圧板などを押したらギミックを起動できるようにする
        MakeToLaunchable();
    }

    /// <summary>
    /// 風の強さを設定する
    /// </summary>
    /// <param name="index"> 強さの設定 0 = 弱、1 = 中、2 = 強 </param>
    public void SetWindPowerIndex(int index)
    {
        //indexが0～2の範囲の時のみ値を設定する
        if(0 <= index && index <= 2)
        {
            windPowerIndex = index;
        }
    }
}
