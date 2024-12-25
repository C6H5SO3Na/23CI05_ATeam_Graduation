using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class LaserBlock : DealDamageObjectBase, IStartedOperation
{
    //変数
    float maxDistance = 100f;                   // レーザーの最大距離
    [SerializeField] LayerMask collisionMask;   // 衝突するレイヤーマスク
    LineRenderer lineRenderer;                  // レーザーの見た目変更用
    bool shouldToPutLaser = true;               // レーザーを出すか
    Vector3 laserStartPoint;                    // レーザーの開始地点
    Vector3 laserDirection;                     // レーザーの発射方向

    // Start is called before the first frame update
    void Start()
    {
        //LineRendererの取得
        lineRenderer = GetComponent<LineRenderer>();
        //rayの見た目変更
        if(lineRenderer)
        {
            lineRenderer.startWidth = 0.5f;
            lineRenderer.endWidth = 0.5f;
        }

        //与えるダメージ量の設定
        SetDealingDamageQuantity(3);

        //レーザーの開始地点と方向を設定
        laserStartPoint = transform.position;
        laserDirection = transform.forward;
    }

    // Update is called once per frame
    void Update()
    {
        //レーザーを出すか判定
        if(shouldToPutLaser)
        {
            FireLaser();
        }
    }

    //レーザーの発射
    void FireLaser()
    {
        //Raycastで衝突判定をする
        RaycastHit hit;
        if(Physics.Raycast(laserStartPoint, laserDirection, out hit, maxDistance, collisionMask))
        {
            //何かに衝突した場合、衝突した場所までレーザーを描画する
            if (lineRenderer)
            {
                lineRenderer.SetPosition(0, laserStartPoint);
                lineRenderer.SetPosition(1, hit.point);
            }

            //衝突したのがプレイヤーだったらダメージを与える
            if (hit.collider.CompareTag("Player"))
            {
                //ダメージを与える
                DealDamage(hit.collider);
            }
        }
        else
        {
            //衝突が無かったら最大距離までのレーザーを描画
            if (lineRenderer)
            {
                lineRenderer.SetPosition(0, laserStartPoint);
                lineRenderer.SetPosition(1, laserStartPoint + laserDirection * maxDistance);
            }
        }
    }

    public void ProcessWhenPressed()
    {
        //レーザーの発射をやめる
        shouldToPutLaser = false;

        if(lineRenderer)
        {
            lineRenderer.enabled = false;
        }
    }

    public void ProcessWhenStopped()
    {
        //レーザーを発射する
        shouldToPutLaser = true;

        if (lineRenderer)
        {
            lineRenderer.enabled = true;
        }
    }
}
