using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageBlock : DealDamageObjectBase
{
    // Start is called before the first frame update
    void Start()
    {
        dealingDamageQuantity = 1;
    }

    //ダメージ床に乗っている間ダメージを受ける
    void OnCollisionStay(Collision collision)
    {
        //collisionがプレイヤーのものか判定する
        if (collision.gameObject.CompareTag("Player"))
        {
            //ダメージを与える
            DealDamage(collision);
        }
    }
}
