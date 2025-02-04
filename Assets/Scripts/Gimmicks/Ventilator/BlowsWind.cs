using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlowsWind : GimmickBase, IStartedOperation
{
    //-------------------------------------------------------------------------------
    // 変数
    //-------------------------------------------------------------------------------
    float[] movePowers = {0.1f, 0.5f, 1f};              // オブジェクトを動かす風の力 左から弱、中、強の三種類
    float[] jumpPowers = { 1f, 2f, 3f };                // プレイヤーのジャンプ力を増やす風の力 左から弱、中、強の三種類
    int     windPowersIndex = 0;                        // 風の力の強弱を決める値
    bool    shouldProcessGimmick;                       // 風を吹かせるか
    Vector3 directionBlowsWind;                         // 風が吹く方向
    float   playerAddedMoveForceCorrectionValue = 1.5f; // プレイヤーに風が当たるときの風の強さの補正値
    [SerializeField] 
    bool    shouldIncreaseJumpPower;                    // プレイヤーのジャンプ力を増やすか(trueの場合はジャンプ力を上げる代わりに風で移動しなくなる)
    [SerializeField]
    bool    isBlowsWindToFirst;                         // 最初から風を吹かせるか
    ParticleSystem particle;
    AudioSource audioSource;
    VentilatorSE SE;
    //-------------------------------------------------------------------------------
    // 関数
    //-------------------------------------------------------------------------------
    void Awake()
    {
        particle = transform.GetChild(0).GetComponent<ParticleSystem>();
        audioSource = GetComponent<AudioSource>();
        SE = GetComponent<VentilatorSE>();
    }
    
    // Start is called before the first frame update
    void Start()
    {
        //最初から風を吹かせる
        if(isBlowsWindToFirst)
        {
            shouldProcessGimmick = true;
        }
        //最初から風を吹かせない
        else
        {
            shouldProcessGimmick = false;
        }

        //風が吹く方向を取得
        directionBlowsWind = transform.forward;
    }

    void OnTriggerStay(Collider other)
    {
        //ジャンプ力を増やす場合
        if(shouldIncreaseJumpPower)
        {
            AddPlayerJumpPower(other);
        }
        //オブジェクトを動かす場合
        else
        {
            //オブジェクトを動かす値を"風で動くオブジェクト"に渡す
            if (shouldProcessGimmick)
            {
                //風が"風で動くオブジェクト"に当たっていたら、オブジェクトを移動させる処理
                MoveObject(other);
            }
            else
            {
                //風の当たり判定を消す
                AddColliderUpToPositionToCrashedWall windCollider = GetComponent<AddColliderUpToPositionToCrashedWall>();
                if(windCollider)
                {
                    //風の当たり判定を消す
                    windCollider.MakeAddColliderSizeToZero();

                    //エフェクトを止める
                    //particle.Stop();
                }
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        //ジャンプ力を増やす場合
        if(shouldIncreaseJumpPower)
        {
            RestorePlayerJumpPower(other);
        }
        //オブジェクトを動かす場合
        else
        {
            //風が"風で動くオブジェクト"に当たらなくなったら、オブジェクトを移動させないようにする処理
            StopMoveObject(other);
        }
    }

    public void ProcessWhenPressed()
    {
        //一度処理したら感圧板等を押し続けている場合は押すのをやめるまで処理しないようにする
        if (HasRunningOnce())
        {
            //ジャンプ力を上げる場合
            if (shouldIncreaseJumpPower)
            {
                //風の力を変化させる
                if (windPowersIndex < jumpPowers.Length - 1)
                {
                    windPowersIndex++;
                }
                else
                {
                    windPowersIndex = 0;
                }
            }
            //オブジェクトを移動させる場合
            else
            {
                //最初に風が吹いている場合、風が吹かないようにする(風に当たったオブジェクトを動かさないようにする)
                if (isBlowsWindToFirst)
                {
                    shouldProcessGimmick = false;
                }
                //最初に風が吹いていない場合、風が吹くようにする(風に当たったオブジェクトを動くようにする)
                else
                {
                    shouldProcessGimmick = true;

                    //風の当たり判定を再生成する
                    AddColliderUpToPositionToCrashedWall windCollider = GetComponent<AddColliderUpToPositionToCrashedWall>();
                    if (windCollider)
                    {
                        windCollider.RestartRaycast();
                    }
                }
            }
        }
    }

    public void ProcessWhenStopped()
    {
        //オブジェクトを移動させる場合
        if (!shouldIncreaseJumpPower)
        {
            //最初に風が吹いていた場合、また風が吹くようにする(風に当たったオブジェクトを動くようにする)
            if(isBlowsWindToFirst)
            {
                shouldProcessGimmick = true;

                //風の当たり判定を再生成する
                AddColliderUpToPositionToCrashedWall windCollider = GetComponent<AddColliderUpToPositionToCrashedWall>();
                if (windCollider)
                {
                    windCollider.RestartRaycast();
                }
            }
            //最初に風が吹いていない場合、また風が吹かないようにする(風に当たったオブジェクトを動かさないようにする)
            else
            {
                shouldProcessGimmick = false;
            }
        }
        
        //また感圧板などを押したらギミックを起動できるようにする
        MakeToLaunchable();
    }

    /// <summary>
    /// オブジェクトを風で移動させる処理
    /// </summary>
    /// <param name="other"> 風に当たっているオブジェクト </param>
    void MoveObject(Collider other)
    {
        //風に当たっているのがプレイヤーの場合
        if (other.CompareTag("Player"))
        {
            PlayerController player = other.GetComponent<PlayerController>();
            if (player)
            {
                //他の風と同時に当たっている場合、風の数値が強い方を優先する
                if (player.windPower.Max <= windPowersIndex)
                {
                    //風の吹く向きと強さを計算する
                    Vector3 windForce = directionBlowsWind * movePowers[windPowersIndex] * playerAddedMoveForceCorrectionValue;

                    //風の力の値を風で動くオブジェクトに渡す
                    player.windPower.Max = windPowersIndex;

                    //風でオブジェクトが動くようにする
                    player.WindMoveDirection = windForce;
                }
            }
        }
        //プレイヤー以外が風に当たっている場合
        else
        {
            //風で動くオブジェクトか判定する
            ObjectsMoveByWind objectMoveByWind = other.GetComponent<ObjectsMoveByWind>();
            if (objectMoveByWind)
            {
                //他の風と同時に当たっている場合、風の数値が強い方を優先する
                if (objectMoveByWind.ReceivingWindPower <= windPowersIndex)
                {
                    //風の吹く向きと強さを計算する
                    Vector3 windForce = directionBlowsWind * movePowers[windPowersIndex];

                    //風の力の値を風で動くオブジェクトに渡す
                    objectMoveByWind.ReceivingWindPower = windPowersIndex;

                    //風でオブジェクトが動くようにする
                    objectMoveByWind.WindForce = windForce;
                }
            }
        }
    }

    /// <summary>
    /// オブジェクトを風で移動させなくする処理
    /// </summary>
    /// <param name="other"> 風に当たっているオブジェクト </param>
    void StopMoveObject(Collider other)
    {
        //風に当たっていたのがプレイヤーの場合
        if (other.CompareTag("Player"))
        {
            PlayerController player = other.GetComponent<PlayerController>();
            if (player)
            {
                //受けている風の力の値をなくす
                player.windPower.Max = 0;

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
                objectMoveByWind.ReceivingWindPower = 0;

                //風でオブジェクトが動かないようにする
                objectMoveByWind.WindForce = Vector3.zero;
            }
        }
    }

    /// <summary>
    /// プレイヤーのジャンプ力を増やす処理
    /// </summary>
    void AddPlayerJumpPower(Collider other)
    {
        //触れているオブジェクトがプレイヤーかどうか判定する
        if(other.CompareTag("Player"))
        {
            PlayerController player = other.GetComponent<PlayerController>();
            if (player)
            {
                //ジャンプ力を上げる
                player.windPower.Received = jumpPowers[windPowersIndex];
            }
        }
    }
    /// <summary>
    /// プレイヤーのジャンプ力を元に戻す処理
    /// </summary>
    /// <param name="other"></param>
    void RestorePlayerJumpPower(Collider other)
    {
        //触れているオブジェクトがプレイヤーかどうか判定する
        if(other.CompareTag("Player"))
        {
            PlayerController player = other.GetComponent<PlayerController>();
            if(player)
            {
                //ジャンプ力を元に戻す
                player.windPower.Received = 0;
            }
        }
    }

    /// <summary>
    /// 風の強さを設定する
    /// </summary>
    /// <param name="index"> 強さの設定 0 = 弱、1 = 中、2 = 強 </param>
    public void SetWindPowerIndex(int index)
    {
        //ジャンプ力を上げるとき
        if(shouldIncreaseJumpPower)
        {
            //indexが"0～ジャンプ力を増やす風の力の配列の要素数-1"の範囲の時のみ値を設定する
            if (0 <= index && index <= jumpPowers.Length - 1)
            {
                windPowersIndex = index;

                //パーティクルの設定
                var main = particle.main;
                main.startSpeed = 10 + index * 8;
                particle.Play();

                //SE再生
                audioSource.clip = SE.ventilatorSE[index];
                audioSource.Play();
            }
        }
        //オブジェクトを移動させるとき
        else
        {
            //indexが"0～オブジェクトを動かす風の力の配列の要素数-1"の範囲の時のみ値を設定する
            if(0 <= index && index <= movePowers.Length - 1)
            {
                windPowersIndex = index;

                //パーティクルの設定
                var main = particle.main;
                main.startSpeed = 10 + index * 8;
                particle.Play();

                //SE再生
                audioSource.clip = SE.ventilatorSE[index];
                audioSource.Play();
            }
        }
    }
}
