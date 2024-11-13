using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack_ShockWave : MonoBehaviour
{
    EnemyBase enemy;    // �G�̋��ʂ���l�A�������p�������X�N���v�g��ێ�����

    //�Ռ��g�֌W-------------------------------------------------------------------
    int         damage;                 // �_���[�W��
    float       expandSpeed = 5f;       // �g��X�s�[�h
    float       maxRadius = 10f;        // �ő唼�a
    int         arcPointsCount = 12;    // �~����̃|�C���g��
    GameObject  damagePointPrefab;      // �_���[�W����p�v���n�u

    // Start is called before the first frame update
    void Start()
    {
        //�G�̋��ʂ���l�A�������p�������X�N���v�g���擾����
        enemy = GetComponent<EnemyBase>();

        //�l�̐ݒ�
        if(enemy)
        {
            damage = enemy.attackPower;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
