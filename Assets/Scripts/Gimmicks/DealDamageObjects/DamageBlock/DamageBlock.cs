using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageBlock : DealDamageObjectBase, IStartedOperation
{
    //変数
    Material material;                  // このスクリプトをアタッチしているオブジェクトのMaterialを保持する
    bool isCauseDamage = true;          // ダメージを与えるか
    Color[] blockColor = new Color[]{   // ブロックの変わる色
                                      new Color( 0.6f, 0f, 1f), // ダメージがある時の色
                                      new Color( 1f, 1f, 1f)    // ダメージを受けない時の色
                                    };

    //関数
    // Start is called before the first frame update
    void Start()
    {
        //Rendererを取得
        Renderer renderer = GetComponent<Renderer>();
        
        //Materialを取得
        if (renderer)
        {
            material = renderer.material;
        }

        //与えるダメージの設定
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
        //一度処理したら感圧板等を押し続けている場合は押すのをやめるまで処理しない
        if (HasRunningOnce())
        {
            //ダメージを受けないようにする
            isCauseDamage = false;

            //マテリアルの色変更
            if (material)
            {
                material.color = blockColor[1];
            }
        }
    }

    public void ProcessWhenStopped()
    {
        //ダメージを受けるようにする
        isCauseDamage = true;

        //マテリアルの色変更
        if (material)
        {
            material.color = blockColor[0];
        }

        //また感圧板などを押したらギミックを起動できるようにする
        MakeToLaunchable();
    }

    //setter関数
    public void SetIsCauseDamaage(bool setValue)
    {
        isCauseDamage = setValue;
    }
}
