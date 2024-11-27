using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReduceEnemyHP : MonoBehaviour, IReduceHP
{
    private EnemyData enemy;    // 敵の共通する値、処理を継承したスクリプトを保持する

    // Start is called before the first frame update
    void Start()
    {
        //敵の共通する値、処理を継承したスクリプトを取得する
        enemy = GetComponent<EnemyData>();
    }

    //HPを減らす
    public void ReduceHP(int damage)
    {
        if(enemy)
        {
            enemy.healthPoint -= damage;
        } 
    }
}
