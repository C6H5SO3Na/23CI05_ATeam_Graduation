using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagingThrowingObject : DealDamageObjectBase
{
    //�ϐ�
    private int attackPower;    // �U����

    //�֐�
    // Start is called before the first frame update
    void Start()
    {
        SetDealingDamageQuantity(1);
    }

    //�Ԃ���������Ƀ_���[�W��^����
    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Enemy"))
        {
            //�_���[�W��^����
            DealDamage(collision);
        }
    }
}