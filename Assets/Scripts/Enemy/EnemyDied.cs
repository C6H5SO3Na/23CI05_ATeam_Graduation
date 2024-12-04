using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieEnemy : MonoBehaviour
{
    EnemyData enemy;    // 敵の共通する値、処理を継承したスクリプトを保持する

    // Start is called before the first frame update
    void Start()
    {
        //敵の共通する値、処理を継承したスクリプトを取得する
        enemy = GetComponent<EnemyData>();
    }

    // Update is called once per frame
    void Update()
    {
        //HPが無くなったら死亡(破棄)する
        if(enemy)
        {
            if(enemy.healthPoint <= 0)
            {
                Destroy(this.gameObject);
            }
        }
    }
}
