using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassInstance : MonoBehaviour
{
    [SerializeField]
    private GameObject GameManagerInstance; // ゲームマネージャーオブジェクトのインスタンス(コンポーネントの情報取得用)

    /// <summary>
    /// 他のクラスにインスタンスを渡す
    /// </summary>
    /// <param name="p1"> プレイヤー1オブジェクトのインスタンス </param>
    /// <param name="p2"> プレイヤー2オブジェクトのインスタンス </param>
    /// <param name="e"> 敵オブジェクトのインスタンス </param>
    void PassInstanceToOtherClass(GameObject p1, GameObject p2, GameObject e)
    {
        //☆敵に渡す
        //Enemyクラスのインスタンス取得
        Enemy enemy = e.GetComponent<Enemy>();

        //ゲームマネージャーの敵が死亡した情報を受け取る関数を実装したクラスをセット
        enemy.SetDependent(GameManagerInstance.GetComponent<IReceiveDeathInformation>(), p1.transform, p2.transform);
    }
}
