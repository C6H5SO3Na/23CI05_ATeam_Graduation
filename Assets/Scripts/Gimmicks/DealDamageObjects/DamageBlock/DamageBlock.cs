using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageBlock : DealDamageObjectBase, IStartedOperation
{
    //変数
    bool isCauseDamage = true; // ダメージを与えるか

    //関数
    // Start is called before the first frame update
    void Start()
    {
        SetDealingDamageQuantity(1);
    }

    //ダメージ床に乗っている間ダメージを受ける
    void OnCollisionStay(Collision collision)
    {
        if(isCauseDamage)
        {
            //collisionがプレイヤーのものか判定する
            if (collision.gameObject.CompareTag("Player"))
            {
                //ダメージを与える
                DealDamage(collision);
            }
        }
    }

    public void ProcessWhenPressed()
    {

    }

    public void ProcessWhenStopped()
    {

    }

    //setter関数
    public void SetIsCauseDamaage(bool setValue)
    {
        isCauseDamage = setValue;
    }
}
