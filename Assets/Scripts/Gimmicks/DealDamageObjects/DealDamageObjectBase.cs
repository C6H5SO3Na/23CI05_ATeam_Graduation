using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DealDamageObjectBase : MonoBehaviour
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
    /// <param name="dealDamage"> 与えるダメージ </param>
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
}
