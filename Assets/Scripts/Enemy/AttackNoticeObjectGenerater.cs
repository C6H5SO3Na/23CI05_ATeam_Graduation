using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackNoticeObjectGenerater : MonoBehaviour
{
    //変数
    [SerializeField]
    GameObject attackNoticePrefab;      // 攻撃予告用オブジェクトのプレハブ
    GameObject attackNoticeInstance;    // 攻撃予告用オブジェクトのインスタンス

    //関数
    /// <summary>
    /// 衝撃波の予告オブジェクト生成
    /// </summary>
    /// <param name="boxSize"> 予告オブジェクトのサイズ </param>
    public void shockWaveNoticeObjectGeneration(float boxSize)
    {
        if (attackNoticePrefab)
        {
            //オブジェクト生成
            attackNoticeInstance = Instantiate(attackNoticePrefab);

            //半径に基づいてスケール変更
            attackNoticeInstance.transform.localScale = new Vector3(boxSize, 0.01f, boxSize);

            //足元に表示する
            attackNoticeInstance.transform.position = new Vector3(transform.position.x, transform.position.y - 0.5f, transform.position.z);
        }
    }

    /// <summary>
    /// 攻撃予告オブジェクトの消去
    /// </summary>
    public void DestroyAttackNoticeObject()
    {
        if(attackNoticeInstance)
        {
            Destroy(attackNoticeInstance);
        }
    }
}
