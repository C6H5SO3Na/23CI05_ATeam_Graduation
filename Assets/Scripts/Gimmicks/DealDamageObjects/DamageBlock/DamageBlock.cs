using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageBlock : DealDamageObjectBase, IStartedOperation
{
    //�ϐ�
    bool isCauseDamage = true; // �_���[�W��^���邩

    //�֐�
    // Start is called before the first frame update
    void Start()
    {
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

    }

    public void ProcessWhenStopped()
    {

    }

    //setter�֐�
    public void SetIsCauseDamaage(bool setValue)
    {
        isCauseDamage = setValue;
    }
}
