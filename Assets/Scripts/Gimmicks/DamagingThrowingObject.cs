using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagingThrowingObject : DealDamageObjectBase
{
    //変数
    private int attackPower;    // 攻撃力

    //関数
    // Start is called before the first frame update
    void Start()
    {
        SetDealingDamageQuantity(1);
    }

    //ぶつかった相手にダメージを与える
    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Enemy"))
        {
            //ダメージを与える
            DealDamage(collision);
        }
    }
}