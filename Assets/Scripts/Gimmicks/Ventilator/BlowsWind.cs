using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlowsWind : GimmickBase, IStartedOperation
{
    //-------------------------------------------------------------------------------
    // �ϐ�
    //-------------------------------------------------------------------------------
    float[] movePowers = {0.1f, 0.5f, 1f};      // �I�u�W�F�N�g�𓮂������̗� �������A���A���̎O���
    float[] jumpPowers = { 1f, 2f, 3f };        // �v���C���[�̃W�����v�͂𑝂₷���̗� �������A���A���̎O���
    int     windPowersIndex = 0;                // ���̗͂̋�������߂�l
    bool    shouldProcessGimmick = true;        // ���𐁂����邩
    Vector3 directionBlowsWind;                 // ������������
    [SerializeField] 
    bool    shouldIncreaseJumpPower = false;    // �v���C���[�̃W�����v�͂𑝂₷��(true�̏ꍇ�̓W�����v�͂��グ�����ɕ��ňړ����Ȃ��Ȃ�)

    //-------------------------------------------------------------------------------
    // �֐�
    //-------------------------------------------------------------------------------
    // Start is called before the first frame update
    void Start()
    {
        //���������������擾
        directionBlowsWind = transform.forward;
    }

    void OnTriggerStay(Collider other)
    {
        //�W�����v�͂��グ��ꍇ
        if (shouldIncreaseJumpPower)
        {

        }
        //�I�u�W�F�N�g���ړ�������ꍇ
        else
        {
            //�I�u�W�F�N�g�𓮂����l��"���œ����I�u�W�F�N�g"�ɓn��
            if (shouldProcessGimmick)
            {
                //����"���œ����I�u�W�F�N�g"�ɓ������Ă�����A�I�u�W�F�N�g���ړ������鏈��
                MoveObject(other);
            }
            //�I�u�W�F�N�g�����ɓ������Ă��鎞�ɕ��𐁂�����̂���߂��Ƃ��A�I�u�W�F�N�g���ړ����Ȃ��悤�ɂ���
            else
            {
                //����"���œ����I�u�W�F�N�g"�ɓ�����Ȃ��Ȃ�����A�I�u�W�F�N�g���ړ������Ȃ��悤�ɂ��鏈��
                StopMoveObject(other);
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        //�W�����v�͂��グ�Ă����ꍇ
        if (shouldIncreaseJumpPower)
        {

        }
        //�I�u�W�F�N�g���ړ������Ă����ꍇ
        else
        {
            //����"���œ����I�u�W�F�N�g"�ɓ�����Ȃ��Ȃ�����A�I�u�W�F�N�g���ړ������Ȃ��悤�ɂ��鏈��
            StopMoveObject(other);
        }
    }

    public void ProcessWhenPressed()
    {
        //��x���������犴���������������Ă���ꍇ�͉����̂���߂�܂ŏ������Ȃ��悤�ɂ���
        if (HasRunningOnce())
        {
            //�W�����v�͂��グ��ꍇ
            if (shouldIncreaseJumpPower)
            {
                //���̗͂�ω�������
                if (windPowersIndex < jumpPowers.Length - 1)
                {
                    windPowersIndex++;
                }
                else
                {
                    windPowersIndex = 0;
                }
            }
            //�I�u�W�F�N�g���ړ�������ꍇ
            else
            {
                //���ɓ��������I�u�W�F�N�g�𓮂����Ȃ��悤�ɂ���
                shouldProcessGimmick = false;
            }
        }
    }

    public void ProcessWhenStopped()
    {
        //�I�u�W�F�N�g���ړ�������ꍇ
        if (!shouldIncreaseJumpPower)
        {
            //�܂����𐁂�����
            shouldProcessGimmick = true;
        }
        
        //�܂������Ȃǂ���������M�~�b�N���N���ł���悤�ɂ���
        MakeToLaunchable();
    }

    //�I�u�W�F�N�g���ړ������鏈��
    void MoveObject(Collider other)
    {
        //���ɓ������Ă���̂��v���C���[�̏ꍇ
        if (other.CompareTag("Player"))
        {
            PlayerController player = other.GetComponent<PlayerController>();
            if (player)
            {
                //���̕��Ɠ����ɓ������Ă���ꍇ�A���̐��l����������D�悷��

                //���̐��������Ƌ������v�Z����
                Vector3 windForce = directionBlowsWind * movePowers[windPowersIndex] * 1.5f;    // 1.5f�̓v���C���[�ɕ���������Ƃ��̕��̋����̕␳�l

                //���̗͂̒l�𕗂œ����I�u�W�F�N�g�ɓn��


                //���ŃI�u�W�F�N�g�������悤�ɂ���
                player.WindMoveDirection = windForce;
            }
        }
        //�v���C���[�ȊO�����ɓ������Ă���ꍇ
        else
        {
            //���œ����I�u�W�F�N�g�����肷��
            ObjectsMoveByWind objectMoveByWind = other.GetComponent<ObjectsMoveByWind>();
            if (objectMoveByWind)
            {
                //���̕��Ɠ����ɓ������Ă���ꍇ�A���̐��l����������D�悷��
                if (objectMoveByWind.ReceivingWindPower <= movePowers[windPowersIndex])
                {
                    //���̐��������Ƌ������v�Z����
                    Vector3 windForce = directionBlowsWind * movePowers[windPowersIndex];

                    //���̗͂̒l�𕗂œ����I�u�W�F�N�g�ɓn��
                    objectMoveByWind.ReceivingWindPower = movePowers[windPowersIndex];

                    //���ŃI�u�W�F�N�g�������悤�ɂ���
                    objectMoveByWind.WindForce = windForce;
                }
            }
        }
    }

    //�I�u�W�F�N�g��Collider�͈̔͊O�ɏo���Ƃ��ɓ����Ȃ����鏈��
    void StopMoveObject(Collider other)
    {
        //���ɓ������Ă����̂��v���C���[�̏ꍇ
        if (other.CompareTag("Player"))
        {
            PlayerController player = other.GetComponent<PlayerController>();
            if (player)
            {
                //�󂯂Ă��镗�̗͂̒l���Ȃ���

                //���Ńv���C���[�������Ȃ��悤�ɂ���
                player.WindMoveDirection = Vector3.zero;
            }
        }
        //�v���C���[�ȊO�����ɓ������Ă����ꍇ
        else
        {
            //���œ����I�u�W�F�N�g�����肷��
            ObjectsMoveByWind objectMoveByWind = other.GetComponent<ObjectsMoveByWind>();
            if (objectMoveByWind)
            {
                //�󂯂Ă��镗�̗͂̒l���Ȃ���
                objectMoveByWind.ReceivingWindPower = 0f;

                //���ŃI�u�W�F�N�g�������Ȃ��悤�ɂ���
                objectMoveByWind.WindForce = Vector3.zero;
            }
        }
    }

    //�v���C���[�̃W�����v�͂𑝂₷����

    //�v���C���[�̃W�����v�͂����ɖ߂�����

    /// <summary>
    /// ���̋�����ݒ肷��
    /// </summary>
    /// <param name="index"> �����̐ݒ� 0 = ��A1 = ���A2 = �� </param>
    public void SetWindPowerIndex(int index)
    {
        //�W�����v�͂��グ��Ƃ�
        if(shouldIncreaseJumpPower)
        {
            //index��"0�`�W�����v�͂𑝂₷���̗͂̔z��̗v�f��-1"�͈̔͂̎��̂ݒl��ݒ肷��
            if (0 <= index && index <= jumpPowers.Length - 1)
            {
                windPowersIndex = index;
            }
        }
        //�I�u�W�F�N�g���ړ�������Ƃ�
        else
        {
            //index��"0�`�I�u�W�F�N�g�𓮂������̗͂̔z��̗v�f��-1"�͈̔͂̎��̂ݒl��ݒ肷��
            if(0 <= index && index <= movePowers.Length - 1)
            {
                windPowersIndex = index;
            }
        }
    }
}
