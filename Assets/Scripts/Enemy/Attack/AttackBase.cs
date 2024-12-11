using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AttackBase
{
    //変数
    protected AttackNoticeObjectGenerater attackNoticeObjectGeneraterInstance;  // 攻撃予告オブジェクト生成クラスのインスタンス
    protected int attackCount;                                                  // Attack関数を行った関数をカウント
    protected Enemy attackOwner;                                                // 攻撃を行うクラス

    //関数
    /// <summary>
    /// 攻撃予告オブジェクト生成クラスのインスタンス設定
    /// </summary>
    /// /// <param name="instance"> 攻撃予告オブジェクト生成クラスのインスタンス </param>
    void SetAttackNoticeObjectGeneraterInstance(AttackNoticeObjectGenerater instance)
    {
        if(instance)
        {
            attackNoticeObjectGeneraterInstance = instance;
        }
        else
        {
            Debug.LogWarning("攻撃予告オブジェクト生成クラスのインスタンスが設定されていません");
        }
    }

    void SetAttackOwner(Enemy attackOwner)
    {
        if(attackOwner)
        {
            this.attackOwner = attackOwner;
        }
        else
        {
            Debug.LogWarning("攻撃を行うクラスが設定されていません");
        }
    }

    /// <summary>
    /// 攻撃処理の初期化
    /// </summary>
    /// <param name="instance"> 攻撃予告オブジェクト生成クラスのインスタンス </param>
    public void Initialize(AttackNoticeObjectGenerater instance, Enemy attackOwner)
    {
        //攻撃予告オブジェクト生成クラスのインスタンス設定
        SetAttackNoticeObjectGeneraterInstance(instance);

        //攻撃を行うクラス設定
        SetAttackOwner(attackOwner);

        //値の初期化
        attackCount = 0;
    }

    /// <summary>
    /// 攻撃処理
    /// </summary>
    public abstract bool Attack();
}
