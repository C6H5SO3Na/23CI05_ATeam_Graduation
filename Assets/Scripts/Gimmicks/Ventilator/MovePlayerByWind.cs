using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayerByWind : GimmickBase, IStartedOperation
{
    float[] windPower = {1f, 2f, 3f};       // �v���C���[�𓮂������̗� �������A���A���̎O���
    int     windPowerIndex = 0;             // windPower�̗v�f�A�N�Z�X�p
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
        //�����v���C���[�ɓ������Ă�����A�v���C���[�𓮂����l��n��
        if(shouldProcessGimmick)
        {
            if(other.CompareTag("Player"))
            {
                //���̕��Ɠ����ɓ������Ă���ꍇ�A���̐��l����������D�悷��

            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        //���Ńv���C���[�������Ȃ��悤�ɂ���

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
            windPowerIndex = index;
        }
    }
}
