using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class AddColliderUpToPositionToCrashedWall : MonoBehaviour
{
    //-------------------------------------------------------------------------------
    // 変数
    //-------------------------------------------------------------------------------
    BoxCollider boxCollider;                // アタッチするBoxCollider
    float       correctionValue = 0.49f;    // このブロックをアタッチするオブジェクトの大きさの半分の値-0.01(隣接したオブジェクトにRayがすり抜けるバグ、このスクリプトをアタッチしているオブジェクトにRayが当たるバグをなくすため)
    [SerializeField]
    bool        shouldAddOntTheTop = false; // Colliderをオブジェクトの上に追加するか(横方向には追加しなくなる)

    //-------------------------------------------------------------------------------
    // 関数
    //-------------------------------------------------------------------------------
    // Start is called before the first frame update
    void Start()
    {
        //BoxColliderをアタッチする
        boxCollider = gameObject.AddComponent<BoxCollider>();

        //アタッチしたコライダーをTriggerにする
        boxCollider.isTrigger = true;

        //Colliderを上につけるとき
        if(shouldAddOntTheTop)
        {
            //上にBoxColliderを生成する
            AddBoxCollider_Top();
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Colliderがオブジェクトの横に追加されるとき
        if(!shouldAddOntTheTop)
        {
            //Rayの当たった位置までの長さのBoxColliderを生成する
            AddBoxCollider_Horizontal();
        }
    }

    /// <summary>
    /// 横方向にRayを飛ばし、当たった位置までの長さのBoxColliderを生成する
    /// </summary>
    void AddBoxCollider_Horizontal()
    {
        //変数宣言
        Vector3 startPoint = transform.position + transform.forward * correctionValue;  // Rayの発射地点
        Ray ray = new Ray(startPoint, transform.forward);                               // 飛ばすRay
        float maxDistance = 30f;                                                        // Rayの長さ
        float distanceToCollisionPoint = 0f;                                            // 衝突点までの距離 

        //Rayを飛ばす
        if (Physics.Raycast(ray, out RaycastHit hit, maxDistance))
        {
            //衝突点までの距離を計算し取得する
            distanceToCollisionPoint = Vector3.Distance(startPoint, hit.point);
        }
        else 
        {
            distanceToCollisionPoint = maxDistance;
        }
        
        //アタッチされているコライダーの設定をする
        if(boxCollider)
        {
            //Rayに衝突したオブジェクトをBoxCollider(Trigger)に触れるようにする
            float boxColliderSize_Z = distanceToCollisionPoint + correctionValue * 2f;

            //アタッチしたコライダーのサイズ、中央の位置を変更する
            boxCollider.size = new Vector3(1, 1, boxColliderSize_Z);
            boxCollider.center = new Vector3(0, 0, boxColliderSize_Z / 2);
        }
    }

    /// <summary>
    /// 上方向にBoxColliderを生成する
    /// </summary>
    void AddBoxCollider_Top()
    {
        //アタッチしたコライダーのサイズ、中央の位置を設定
        Vector3 boxColliderSize = new Vector3(0.95f, 0.1f, 0.95f);
        float boxColliderCenter_Y = 0.55f;

        //アタッチしたコライダーのサイズ、中央の位置を変更する
        boxCollider.size = boxColliderSize;
        boxCollider.center = new Vector3(0, boxColliderCenter_Y, 0);
    }
}
