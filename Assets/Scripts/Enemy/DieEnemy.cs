using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieEnemy : MonoBehaviour
{
    EnemyData enemy;    // �G�̋��ʂ���l�A�������p�������X�N���v�g��ێ�����

    // Start is called before the first frame update
    void Start()
    {
        //�G�̋��ʂ���l�A�������p�������X�N���v�g���擾����
        enemy = GetComponent<EnemyData>();
    }

    // Update is called once per frame
    void Update()
    {
        //HP�������Ȃ����玀�S(�j��)����
        if(enemy)
        {
            if(enemy.healthPoint <= 0)
            {
                Destroy(this.gameObject);
            }
        }
    }
}
