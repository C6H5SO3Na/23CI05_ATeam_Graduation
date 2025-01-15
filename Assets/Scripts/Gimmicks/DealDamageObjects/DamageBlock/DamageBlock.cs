using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageBlock : DealDamageObjectBase, IStartedOperation
{
    //�ϐ�
    Material material;                  // ���̃X�N���v�g���A�^�b�`���Ă���I�u�W�F�N�g��Material��ێ�����
    bool isCauseDamage = true;          // �_���[�W��^���邩
    Color[] blockColor = new Color[]{   // �u���b�N�̕ς��F
                                      new Color( 0.6f, 0f, 1f), // �_���[�W�����鎞�̐F
                                      new Color( 1f, 1f, 1f)    // �_���[�W���󂯂Ȃ����̐F
                                    };

    //�֐�
    // Start is called before the first frame update
    void Start()
    {
        //Renderer���擾
        Renderer renderer = GetComponent<Renderer>();
        
        //Material���擾
        if (renderer)
        {
            material = renderer.material;
        }

        //�^����_���[�W�̐ݒ�
        SetDealingDamageQuantity(1);
    }

    //�_���[�W���ɏ���Ă���ԃ_���[�W���󂯂�
    void OnCollisionStay(Collision collision)
    {
        if(isCauseDamage)
        {
            //collision���v���C���[�̂��̂����肷��
            if (collision.gameObject.CompareTag("Player"))
            {
                //�_���[�W��^����
                DealDamage(collision);
            }
        }
    }

    public void ProcessWhenPressed()
    {
        //��x���������犴���������������Ă���ꍇ�͉����̂���߂�܂ŏ������Ȃ�
        if (HasRunningOnce())
        {
            //�_���[�W���󂯂Ȃ��悤�ɂ���
            isCauseDamage = false;

            //�}�e���A���̐F�ύX
            if (material)
            {
                material.color = blockColor[1];
            }
        }
    }

    public void ProcessWhenStopped()
    {
        //�_���[�W���󂯂�悤�ɂ���
        isCauseDamage = true;

        //�}�e���A���̐F�ύX
        if (material)
        {
            material.color = blockColor[0];
        }

        //�܂������Ȃǂ���������M�~�b�N���N���ł���悤�ɂ���
        MakeToLaunchable();
    }

    //setter�֐�
    public void SetIsCauseDamaage(bool setValue)
    {
        isCauseDamage = setValue;
    }
}
