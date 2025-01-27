using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlowsWind : GimmickBase, IStartedOperation
{
    float[] windPowers = {0.1f, 0.5f, 1f};  // オブジェクトを動かす風の力 左から弱、中、強の三種類
    int     windPowersIndex = 0;            // windPowersの要素アクセス用
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
        //風が風で動くオブジェクトに当たっていたら、オブジェクトを動かす値を渡す
        if(shouldProcessGimmick)
        {
            //風に当たっているのがプレイヤーの場合
            if(other.CompareTag("Player"))
            {
                //他の風と同時に当たっている場合、風の数値が強い方を優先する

            }
            //プレイヤー以外が風に当たっている場合
            else
            {
                //風で動くオブジェクトか判定する
                ObjectsMoveByWind objectMoveByWind = other.GetComponent<ObjectsMoveByWind>();
                if(objectMoveByWind)
                {
                    //風の吹く向きと強さを計算する
                    Vector3 windForce = directionBlowsWind * windPowers[windPowersIndex];

                    //風でオブジェクトが動くようにする
                    objectMoveByWind.WindForce = windForce;
                }
            }
            
        }
    }

    void OnTriggerExit(Collider other)
    {
        //風に当たっていたのがプレイヤーの場合
        if (other.CompareTag("Player"))
        {
            //風でプレイヤーが動かないようにする
        }
        //プレイヤー以外が風に当たっていた場合
        else
        {
            //風で動くオブジェクトか判定する
            ObjectsMoveByWind objectMoveByWind = other.GetComponent<ObjectsMoveByWind>();
            if (objectMoveByWind)
            {
                //風でオブジェクトが動くようにする
                objectMoveByWind.WindForce = Vector3.zero;
            }
        }
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
            windPowersIndex = index;
        }
    }
}
