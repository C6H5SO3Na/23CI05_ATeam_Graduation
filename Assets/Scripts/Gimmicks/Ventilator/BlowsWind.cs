using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlowsWind : GimmickBase, IStartedOperation
{
    float[] windPowers = {0.1f, 0.5f, 1f};  // �I�u�W�F�N�g�𓮂������̗� �������A���A���̎O���
    int     windPowersIndex = 0;            // windPowers�̗v�f�A�N�Z�X�p
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
                //���̕��Ɠ����ɓ������Ă���ꍇ�A���̐��l����������D�悷��

            }
            //�v���C���[�ȊO�����ɓ������Ă���ꍇ
            else
            {
                //���œ����I�u�W�F�N�g�����肷��
                ObjectsMoveByWind objectMoveByWind = other.GetComponent<ObjectsMoveByWind>();
                if(objectMoveByWind)
                {
                    //���̐��������Ƌ������v�Z����
                    Vector3 windForce = directionBlowsWind * windPowers[windPowersIndex];

                    //���ŃI�u�W�F�N�g�������悤�ɂ���
                    objectMoveByWind.WindForce = windForce;
                }
            }
            
        }
    }

    void OnTriggerExit(Collider other)
    {
        //���ɓ������Ă����̂��v���C���[�̏ꍇ
        if (other.CompareTag("Player"))
        {
            //���Ńv���C���[�������Ȃ��悤�ɂ���
        }
        //�v���C���[�ȊO�����ɓ������Ă����ꍇ
        else
        {
            //���œ����I�u�W�F�N�g�����肷��
            ObjectsMoveByWind objectMoveByWind = other.GetComponent<ObjectsMoveByWind>();
            if (objectMoveByWind)
            {
                //���ŃI�u�W�F�N�g�������悤�ɂ���
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
