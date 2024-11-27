using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagingThrowingObject : MonoBehaviour
{
    //�ϐ�
    private int attackPower;    // �U����

    //�֐�
    // Start is called before the first frame update
    void Start()
    {
        attackPower = 1;
    }

    //�Ԃ���������Ƀ_���[�W��^����
    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Enemy"))
        {
            //HP������������������Ă��邩�m�F
            IReduceHP decreaseHPObject = collision.gameObject.GetComponent<IReduceHP>();
            if(decreaseHPObject != null)
            {
                decreaseHPObject.ReduceHP(attackPower);
            }
            else
            {
                Debug.LogWarning("HP��������������");
            }
        }
    }

    //setter�֐�
    public void SetAttackPower(int setValue)
    {
        if (setValue <= 0)
        {
            Debug.LogWarning("�U���͂�0�ȉ��ɂ͂Ȃ�Ȃ�");
            return;
        }

        attackPower = setValue;
    }
}