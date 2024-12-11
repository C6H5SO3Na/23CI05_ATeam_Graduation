using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyAttack_ShockWave : AttackBase
{
    //�R���X�g���N�^----------------------------------------------------------
    public EnemyAttack_ShockWave()
    {
        boxSize = 4.0f;
    }

    //�ϐ�-------------------------------------------------------------------
    float       boxSize; // �l�p��size
    BoxCollider box;    // BoxCollider�̃C���X�^���X

    //�֐�-------------------------------------------------------------------
    //�U������
    public override bool Attack ()
    {
        attackCount++;

        //�U���\���I�u�W�F�N�g����(���ڂ̌Ăяo���ł̂ݍs��)
        if (attackCount == 1)
        {
            attackNoticeObjectGeneraterInstance.shockWaveNoticeObjectGeneration(boxSize);
        }

        //�U���\���������A�_���[�W����(3�b��ɍU�����A20f�c��)
        if (attackCount == GameManager.gameFPS * 3)
        {
            //�U���\��������
            attackNoticeObjectGeneraterInstance.DestroyAttackNoticeObject();

            //�_���[�W��������
            if(box == null)
            {
                box = attackOwner.AddComponent<BoxCollider>();
                box.isTrigger = true;
                box.size = new Vector3(boxSize / attackOwner.enemyTransform.localScale.x, boxSize / attackOwner.enemyTransform.localScale.y, boxSize / attackOwner.enemyTransform.localScale.z);
            }
        }
        //�_���[�W���������
        if (attackCount == GameManager.gameFPS * 3 + 20)
        {
            //�_���[�W���������
            if (box != null)
            {
                Object.Destroy(box);
            }

            //�ҋ@��ԂɑJ�ڂ���
            return true;
        }

        return false;
    }
}