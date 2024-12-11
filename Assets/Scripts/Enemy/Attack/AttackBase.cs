using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AttackBase
{
    //�ϐ�
    protected AttackNoticeObjectGenerater attackNoticeObjectGeneraterInstance;  // �U���\���I�u�W�F�N�g�����N���X�̃C���X�^���X
    protected int attackCount;                                                  // Attack�֐����s�����֐����J�E���g
    protected Enemy attackOwner;                                                // �U�����s���N���X

    //�֐�
    /// <summary>
    /// �U���\���I�u�W�F�N�g�����N���X�̃C���X�^���X�ݒ�
    /// </summary>
    /// /// <param name="instance"> �U���\���I�u�W�F�N�g�����N���X�̃C���X�^���X </param>
    void SetAttackNoticeObjectGeneraterInstance(AttackNoticeObjectGenerater instance)
    {
        if(instance)
        {
            attackNoticeObjectGeneraterInstance = instance;
        }
        else
        {
            Debug.LogWarning("�U���\���I�u�W�F�N�g�����N���X�̃C���X�^���X���ݒ肳��Ă��܂���");
        }
    }

    void SetAttackOwner(Enemy attackOwner)
    {
        if(attackOwner)
        {
            this.attackOwner = attackOwner;
        }
        else
        {
            Debug.LogWarning("�U�����s���N���X���ݒ肳��Ă��܂���");
        }
    }

    /// <summary>
    /// �U�������̏�����
    /// </summary>
    /// <param name="instance"> �U���\���I�u�W�F�N�g�����N���X�̃C���X�^���X </param>
    public void Initialize(AttackNoticeObjectGenerater instance, Enemy attackOwner)
    {
        //�U���\���I�u�W�F�N�g�����N���X�̃C���X�^���X�ݒ�
        SetAttackNoticeObjectGeneraterInstance(instance);

        //�U�����s���N���X�ݒ�
        SetAttackOwner(attackOwner);

        //�l�̏�����
        attackCount = 0;
    }

    /// <summary>
    /// �U������
    /// </summary>
    public abstract bool Attack();
}
