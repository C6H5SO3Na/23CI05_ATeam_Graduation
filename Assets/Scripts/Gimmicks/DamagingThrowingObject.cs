using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagingThrowingObject : MonoBehaviour
{
    //変数
    private int attackPower;    // 攻撃力

    //関数
    // Start is called before the first frame update
    void Start()
    {
        attackPower = 1;
    }

    //ぶつかった相手にダメージを与える
    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Enemy"))
        {
            //HP減少処理が実装されているか確認
            IReduceHP decreaseHPObject = collision.gameObject.GetComponent<IReduceHP>();
            if(decreaseHPObject != null)
            {
                decreaseHPObject.ReduceHP(attackPower);
            }
            else
            {
                Debug.LogWarning("HP減少処理未実装");
            }
        }
    }

    //setter関数
    public void SetAttackPower(int setValue)
    {
        if (setValue <= 0)
        {
            Debug.LogWarning("攻撃力は0以下にはならない");
            return;
        }

        attackPower = setValue;
    }
}