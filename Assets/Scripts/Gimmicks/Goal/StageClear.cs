using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageClear : MonoBehaviour
{
    GameManager gameManager;    // ゲームマネージャーのインスタンス(Set関数から取得)
    bool isOn_Player1;          // プレイヤー1が乗っているか
    bool isOn_Player2;          // プレイヤー2が乗っているか

    void Start()
    {
        isOn_Player1 = false;
        isOn_Player2 = false;
    }

    //ゴールブロックの上に乗ったらクリアする
    void OnTriggerEnter(Collider other)
    {
        //乗ったオブジェクトがプレイヤーか判定
        if (other.gameObject.CompareTag("Player"))
        {
            //マルチプレイの場合
            if (GameManager.isMultiPlay)
            {
                //プレイヤーのインスタンスを取得する
                PlayerController　player = other.GetComponent<PlayerController>();
                if(player)
                {
                    //ゴールに乗ったプレイヤーが1か2か判定する
                    if (player.PlayerNum == 1)
                    {
                        isOn_Player1 = true;
                    }
                    else if(player.PlayerNum == 2)
                    {
                        isOn_Player2 = true;
                    }
                    else
                    {
                        Debug.LogWarning("プレイヤー番号の設定を間違えています");
                    }
                }

                //プレイヤー1、2が乗ったらステージをクリアする
                if(isOn_Player1 && isOn_Player2)
                {
                    if (gameManager)
                    {
                        gameManager.ReceiveClearInformation();
                    }                    
                }
            }
            //シングルプレイの場合
            else
            {
                //ステージをクリアする
                if (gameManager)
                {
                    gameManager.ReceiveClearInformation();
                }
                else
                {
                    Debug.LogWarning("GameManagerクラスが設定されていません");
                }
            }
        }
    }

    /// <summary>
    /// 他オブジェクトのインスタンスを取得する
    /// </summary>
    /// <param name="gameManager"> GameManagerコンポーネント </param>
    public void SetInstance(GameManager gameManager)
    {
        this.gameManager = gameManager;
    }
}
