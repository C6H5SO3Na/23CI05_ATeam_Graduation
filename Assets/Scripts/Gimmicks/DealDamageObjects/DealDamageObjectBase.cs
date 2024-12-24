using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DealDamageObjectBase : GimmickBase
{
    //�ϐ�
    /// <summary>
    /// �^����_���[�W��
    /// </summary>
    protected int dealingDamageQuantity;

    //�֐�
    /// <summary>
    /// �_���[�W��^���鏈��
    /// </summary>
    /// <param name="collision"> �_���[�W���󂯂�I�u�W�F�N�g������Collision </param>
    protected void DealDamage(Collision collision)
    {
        //�_���[�W���󂯂�I�u�W�F�N�g���_���[�W���󂯂鏈�����������Ă��邩�m�F����
        IReduceHP objectTakeDamage = collision.gameObject.GetComponent<IReduceHP>();
        if (objectTakeDamage != null)
        {
            //�������Ă���_���[�W���󂯂鏈�����s��
            objectTakeDamage.ReduceHP(dealingDamageQuantity);
        }
    }
    /// <summary>
    /// �_���[�W��^���鏈��
    /// </summary>
    /// <param name="hit"> �_���[�W���󂯂�I�u�W�F�N�g������Collider </param>
    protected void DealDamage(Collider other)
    {
        //�_���[�W���󂯂�I�u�W�F�N�g���_���[�W���󂯂鏈�����������Ă��邩�m�F����
        IReduceHP objectTakeDamage = other.GetComponent<IReduceHP>();
        if (objectTakeDamage != null)
        {
            //�������Ă���_���[�W���󂯂鏈�����s��
            objectTakeDamage.ReduceHP(dealingDamageQuantity);
        }
    }

    //setter�֐�
    /// <summary>
    /// �^����_���[�W�ʂ����߂�
    /// </summary>
    /// <param name="setValue"> �^����_���[�W </param>
    protected void SetDealingDamageQuantity(int setValue)
    {
        if (setValue <= 0)
        {
            dealingDamageQuantity = 1;
            Debug.LogWarning("�U���͂�0�ȉ��ɂ͂Ȃ�Ȃ�");
            return;
        }

        dealingDamageQuantity = setValue;
    }
}
