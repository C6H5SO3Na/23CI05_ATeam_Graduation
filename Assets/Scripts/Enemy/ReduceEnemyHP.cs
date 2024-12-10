using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReduceEnemyHP : MonoBehaviour, IReduceHP
{
    private EnemyData enemy;    // �G�̋��ʂ���l�A�������p�������X�N���v�g��ێ�����

    // Start is called before the first frame update
    void Start()
    {
        //�G�̋��ʂ���l�A�������p�������X�N���v�g���擾����
        enemy = GetComponent<EnemyData>();
    }

    //HP�����炷
    public void ReduceHP(int damage)
    {
        if(enemy)
        {
            enemy.healthPoint -= damage;
        } 
    }
}
