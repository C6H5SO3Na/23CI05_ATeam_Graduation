using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DealDamageObjectBase : GimmickBase
{
    //変数
    /// <summary>
    /// 与えるダメージ量
    /// </summary>
    protected int dealingDamageQuantity;

    //関数
    /// <summary>
    /// ダメージを与える処理
    /// </summary>
    /// <param name="collision"> ダメージを受けるオブジェクトが持つCollision </param>
    protected void DealDamage(Collision collision)
    {
        //ダメージを受けるオブジェクトがダメージを受ける処理を実装しているか確認する
        IReduceHP objectTakeDamage = collision.gameObject.GetComponent<IReduceHP>();
        if (objectTakeDamage != null)
        {
            //実装しているダメージを受ける処理を行う
            objectTakeDamage.ReduceHP(dealingDamageQuantity);
        }
    }
    /// <summary>
    /// ダメージを与える処理
    /// </summary>
    /// <param name="hit"> ダメージを受けるオブジェクトが持つCollider </param>
    protected void DealDamage(Collider other)
    {
        //ダメージを受けるオブジェクトがダメージを受ける処理を実装しているか確認する
        IReduceHP objectTakeDamage = other.GetComponent<IReduceHP>();
        if (objectTakeDamage != null)
        {
            //実装しているダメージを受ける処理を行う
            objectTakeDamage.ReduceHP(dealingDamageQuantity);
        }
    }

    //setter関数
    /// <summary>
    /// 与えるダメージ量を決める
    /// </summary>
    /// <param name="setValue"> 与えるダメージ </param>
    protected void SetDealingDamageQuantity(int setValue)
    {
        if (setValue <= 0)
        {
            dealingDamageQuantity = 1;
            Debug.LogWarning("攻撃力は0以下にはならない");
            return;
        }

        dealingDamageQuantity = setValue;
    }
}
