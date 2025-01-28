using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlowsWind : GimmickBase, IStartedOperation
{
    //-------------------------------------------------------------------------------
    // �ϐ�
    //-------------------------------------------------------------------------------
    float[] windPowers = {0.1f, 0.5f, 1f};  // �I�u�W�F�N�g�𓮂������̗� �������A���A���̎O���
    int     windPowersIndex = 1;            // windPowers�̗v�f�A�N�Z�X�p
    bool    shouldProcessGimmick = true;    // ���𐁂����邩
    Vector3 directionBlowsWind;             // ������������

    // Start is called before the first frame update
    void Start()
    {
        //���������������擾
        directionBlowsWind = transform.forward;
    }

    void OnTriggerStay(Collider other)
    {
        //�������œ����I�u�W�F�N�g�ɓ������Ă�����A�I�u�W�F�N�g�𓮂����l��n��
        if(shouldProcessGimmick)
        {
            //���ɓ������Ă���̂��v���C���[�̏ꍇ
            if(other.CompareTag("Player"))
            {
                PlayerController player = other.GetComponent<PlayerController>();
                if(player)
                {
                    //���̕��Ɠ����ɓ������Ă���ꍇ�A���̐��l����������D�悷��

                    //���̐��������Ƌ������v�Z����
                    Vector3 windForce = directionBlowsWind * windPowers[windPowersIndex] * 1.5f;    // 1.5f�̓v���C���[�ɕ���������Ƃ��̕��̋����̕␳�l

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
                if(objectMoveByWind)
                {
                    //���̕��Ɠ����ɓ������Ă���ꍇ�A���̐��l����������D�悷��
                    if (objectMoveByWind.ReceivingWindPower < windPowers[windPowersIndex])
                    {
                        //���̐��������Ƌ������v�Z����
                        Vector3 windForce = directionBlowsWind * windPowers[windPowersIndex];

                        //���̗͂̒l�𕗂œ����I�u�W�F�N�g�ɓn��
                        objectMoveByWind.ReceivingWindPower = windPowers[windPowersIndex];

                        //���ŃI�u�W�F�N�g�������悤�ɂ���
                        objectMoveByWind.WindForce = windForce;
                    }                    
                }
            }
            
        }
    }

    void OnTriggerExit(Collider other)
    {
        //���ɓ������Ă����̂��v���C���[�̏ꍇ
        if (other.CompareTag("Player"))
        {
            PlayerController player = other.GetComponent<PlayerController>();
            if(player)
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

    public void ProcessWhenPressed()
    {
        //��x���������犴���������������Ă���ꍇ�͉����̂���߂�܂ŏ������Ȃ��悤�ɂ���
        if (HasRunningOnce())
        {
            //���𐁂�����̂���߂�
            shouldProcessGimmick = false;
        }
    }

    public void ProcessWhenStopped()
    {
        //�܂����𐁂�����
        shouldProcessGimmick = true;

        //�܂������Ȃǂ���������M�~�b�N���N���ł���悤�ɂ���
        MakeToLaunchable();
    }

    /// <summary>
    /// ���̋�����ݒ肷��
    /// </summary>
    /// <param name="index"> �����̐ݒ� 0 = ��A1 = ���A2 = �� </param>
    public void SetWindPowerIndex(int index)
    {
        //index��0�`2�͈̔͂̎��̂ݒl��ݒ肷��
        if(0 <= index && index <= 2)
        {
            windPowersIndex = index;
        }
    }
}
