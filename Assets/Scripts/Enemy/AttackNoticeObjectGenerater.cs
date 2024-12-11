using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackNoticeObjectGenerater : MonoBehaviour
{
    //�ϐ�
    [SerializeField]
    GameObject attackNoticePrefab;      // �U���\���p�I�u�W�F�N�g�̃v���n�u
    GameObject attackNoticeInstance;    // �U���\���p�I�u�W�F�N�g�̃C���X�^���X

    //�֐�
    /// <summary>
    /// �Ռ��g�̗\���I�u�W�F�N�g����
    /// </summary>
    /// <param name="boxSize"> �\���I�u�W�F�N�g�̃T�C�Y </param>
    public void shockWaveNoticeObjectGeneration(float boxSize)
    {
        if (attackNoticePrefab)
        {
            //�I�u�W�F�N�g����
            attackNoticeInstance = Instantiate(attackNoticePrefab);

            //���a�Ɋ�Â��ăX�P�[���ύX
            attackNoticeInstance.transform.localScale = new Vector3(boxSize, 0.01f, boxSize);

            //�����ɕ\������
            attackNoticeInstance.transform.position = new Vector3(transform.position.x, transform.position.y - 0.5f, transform.position.z);
        }
    }

    /// <summary>
    /// �U���\���I�u�W�F�N�g�̏���
    /// </summary>
    public void DestroyAttackNoticeObject()
    {
        if(attackNoticeInstance)
        {
            Destroy(attackNoticeInstance);
        }
    }
}
