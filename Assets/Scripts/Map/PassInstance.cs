using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassInstance : MonoBehaviour
{
    [SerializeField]
    private GameObject gameManagerInstance; // ゲームマネージャーオブジェクトのインスタンス(コンポーネントの情報取得用)

    /// <summary>
    /// 他のクラスにインスタンスを渡す
    /// </summary>
    /// <param name="p1"> プレイヤー1オブジェクトのインスタンス </param>
    /// <param name="p2"> プレイヤー2オブジェクトのインスタンス </param>
    /// <param name="e"> 敵オブジェクトのインスタンス </param>
    /// <param name="g"> ゴールオブジェクトのインスタンス </param>
    public void PassInstanceToOtherClass(GameObject p1, GameObject p2, GameObject e, GameObject g)
    {
        ////敵に渡す
        //if(p1 && p2 && e && gameManagerInstance)
        //{
        //    //Enemyクラスの取得
        //    Enemy enemy = e.GetComponent<Enemy>();

        //    //ゲームマネージャーの敵が死亡した情報を受け取る関数を実装したクラスをセット
        //    enemy.SetDependent(gameManagerInstance.GetComponent<IReceiveDeathInformation>(), p1.transform, p2.transform);
        //}
        
        //ゴールに渡す
        if(g && gameManagerInstance)
        {
            //StageClearクラスの取得
            StageClear goal = g.GetComponent<StageClear>();

            //GameManagerクラスを取得する
            goal.SetInstance(gameManagerInstance.GetComponent<GameManager>());
        }


        //プレイヤー1に渡す
        if (p1 && gameManagerInstance)
        {
            //PlayerControllerクラスの取得
            var player = p1.GetComponent<PlayerController>();

            //GameManagerクラスを取得する
            player.SetInstance(gameManagerInstance.GetComponent<GameManager>());
        }

        //プレイヤー2に渡す
        if (p2 && gameManagerInstance)
        {
            //PlayerControllerクラスの取得
            var player = p2.GetComponent<PlayerController>();

            //GameManagerクラスを取得する
            player.SetInstance(gameManagerInstance.GetComponent<GameManager>());
        }
    }
}
