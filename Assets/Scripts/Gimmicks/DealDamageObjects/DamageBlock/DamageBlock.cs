using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageBlock : DealDamageObjectBase
{
    // Start is called before the first frame update
    void Start()
    {
        dealingDamageQuantity = 1;
    }

    //�_���[�W���ɏ���Ă���ԃ_���[�W���󂯂�
    void OnCollisionStay(Collision collision)
    {
        //collision���v���C���[�̂��̂����肷��
        if (collision.gameObject.CompareTag("Player"))
        {
            //�_���[�W��^����
            DealDamage(collision);
        }
    }
}
