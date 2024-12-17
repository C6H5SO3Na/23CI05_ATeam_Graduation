using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageClear : MonoBehaviour
{
    [SerializeField]
    GameManager gameManager;    // ゲームマネージャーのインスタンス(Inspactorから取得)
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
                    if (player.playerNum == 1)
                    {
                        isOn_Player1 = true;
                    }
                    else if(player.playerNum == 2)
                    {
                        isOn_Player2 = true;
                    }
                    else
                    {
                        Debug.LogWarning("プレイヤー番号の設定を間違えています");
                    }
                }

                //プレイヤー1、2が乗ったらクリアにする
                if(isOn_Player1 && isOn_Player2)
                {
                    gameManager.ReceiveClearInformation();
                }
            }
            //シングルプレイの場合
            else
            {
                //クリアにする
                gameManager.ReceiveClearInformation();
            }
        }
    }
}
