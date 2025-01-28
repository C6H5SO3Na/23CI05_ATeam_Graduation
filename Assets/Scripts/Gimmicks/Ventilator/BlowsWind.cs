using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlowsWind : GimmickBase, IStartedOperation
{
    //-------------------------------------------------------------------------------
    // 変数
    //-------------------------------------------------------------------------------
    float[] windPowers = {0.1f, 0.5f, 1f};  // オブジェクトを動かす風の力 左から弱、中、強の三種類
    int     windPowersIndex = 1;            // windPowersの要素アクセス用
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
                PlayerController player = other.GetComponent<PlayerController>();
                if(player)
                {
                    //他の風と同時に当たっている場合、風の数値が強い方を優先する

                    //風の吹く向きと強さを計算する
                    Vector3 windForce = directionBlowsWind * windPowers[windPowersIndex] * 1.5f;    // 1.5fはプレイヤーに風が当たるときの風の強さの補正値

                    //風の力の値を風で動くオブジェクトに渡す


                    //風でオブジェクトが動くようにする
                    player.WindMoveDirection = windForce;
                }
            }
            //プレイヤー以外が風に当たっている場合
            else
            {
                //風で動くオブジェクトか判定する
                ObjectsMoveByWind objectMoveByWind = other.GetComponent<ObjectsMoveByWind>();
                if(objectMoveByWind)
                {
                    //他の風と同時に当たっている場合、風の数値が強い方を優先する
                    if (objectMoveByWind.ReceivingWindPower < windPowers[windPowersIndex])
                    {
                        //風の吹く向きと強さを計算する
                        Vector3 windForce = directionBlowsWind * windPowers[windPowersIndex];

                        //風の力の値を風で動くオブジェクトに渡す
                        objectMoveByWind.ReceivingWindPower = windPowers[windPowersIndex];

                        //風でオブジェクトが動くようにする
                        objectMoveByWind.WindForce = windForce;
                    }                    
                }
            }
            
        }
    }

    void OnTriggerExit(Collider other)
    {
        //風に当たっていたのがプレイヤーの場合
        if (other.CompareTag("Player"))
        {
            PlayerController player = other.GetComponent<PlayerController>();
            if(player)
            {
                //受けている風の力の値をなくす

                //風でプレイヤーが動かないようにする
                player.WindMoveDirection = Vector3.zero;
            }
        }
        //プレイヤー以外が風に当たっていた場合
        else
        {
            //風で動くオブジェクトか判定する
            ObjectsMoveByWind objectMoveByWind = other.GetComponent<ObjectsMoveByWind>();
            if (objectMoveByWind)
            {
                //受けている風の力の値をなくす
                objectMoveByWind.ReceivingWindPower = 0f;

                //風でオブジェクトが動かないようにする
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
