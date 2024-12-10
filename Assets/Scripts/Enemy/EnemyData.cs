using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyData : MonoBehaviour
{
    //�X�e�[�^�X�֌W-----------------------------------------------------------------
    //�ϐ�
    public int healthPoint = 2;     // �̗�
    public int attackPower = 1;     // �U����

    //�s���֌W
    public EnemyDied enemyDied;     // �G���S���̍s��

    private void Awake()
    {
        enemyDied = new EnemyDied();
    }
}