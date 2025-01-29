using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class AddColliderUpToPositionToCrashedWall : MonoBehaviour
{
    //-------------------------------------------------------------------------------
    // 変数
    //-------------------------------------------------------------------------------
    BoxCollider boxCollider;              // アタッチするBoxCollider
    float       correctionValue = 0.5f;   // このブロックをアタッチするオブジェクトの大きさの半分の値(Rayをオブジェクトの前から飛ばすため)

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
    }

    // Update is called once per frame
    void Update()
    {
        //Rayの当たった位置までの長さのBoxColliderを生成する
        AddBoxCollider();
    }

    /// <summary>
    /// Rayの当たった位置までの長さのBoxColliderを生成する
    /// </summary>
    void AddBoxCollider()
    {
        //変数宣言
        Vector3 startPoint = transform.position + transform.forward * correctionValue; // Rayの発射地点
        Ray ray = new Ray(startPoint, transform.forward);                   // 飛ばすRay
        float maxDistance = 30f;                                            // Rayの長さ
        float distanceToCollisionPoint = 0f;                                // 衝突点までの距離 

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

            //アタッチしたコライダーのサイズを変更する
            boxCollider.size = new Vector3(1, 1, boxColliderSize_Z);
            boxCollider.center = new Vector3(0, 0, boxColliderSize_Z / 2);
        }
    }
}
