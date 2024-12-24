using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DealDamageObjectBase : MonoBehaviour
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
    /// <param name="dealDamage"> �^����_���[�W </param>
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
}
