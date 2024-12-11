using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyData : MonoBehaviour
{
    //�X�e�[�^�X�֌W-----------------------------------------------------------------
    //�ϐ�
    public int healthPoint = 2;     // �̗�
    public int attackPower = 1;     // �U����

    //�s���֌W-----------------------------------------------------------------------
    //�ϐ�
    public EnemyDied enemyDied { get; private set; }                            // �G���S���̍s��
    public Dictionary<string, AttackBase> enemyAttacks { get; private set; }    // �G�U���̍s�����X�g
    public List<string> attackTypes { get; private set; }                       // �G���s���U���s��

    //�֐�
    void Awake()
    {
        //�G���S���̍s���ݒ�
        enemyDied = new EnemyDied();

        //�G�U���̍s�����X�g�ݒ�
        enemyAttacks = new Dictionary<string, AttackBase>();
        enemyAttacks["ShockWave"] = new EnemyAttack_ShockWave();
        enemyAttacks["CreateDamageFloor"] = new EnemyAttack_CreateDamageFloor();
        enemyAttacks["ShowerBall"] = new EnemyAttack_ShowerBall();

        //�G���s���U���̎�ޖ������߂�
        attackTypes = new List<string>();
        attackTypes.Add("CreateDamageFloor");
        attackTypes.Add("ShowerBall");
    }
}