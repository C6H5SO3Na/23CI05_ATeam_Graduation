using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsMoveByWind : MonoBehaviour
{
    //-------------------------------------------------------------------------------
    // �ϐ�
    //-------------------------------------------------------------------------------
    Rigidbody rb;                               // ���̃X�N���v�g���A�^�b�`���Ă���I�u�W�F�N�g��Rigidbody
    private float receivingWindPower = 0f;      // �󂯂Ă��镗�̗�
    public float ReceivingWindPower
    {
        set { receivingWindPower = value; }
        get { return receivingWindPower; }
    }
    private Vector3 windForce = Vector3.zero;   // �󂯂Ă��镗�̋���
    public Vector3 WindForce
    {
        set { windForce = value; }
        get { return windForce; }
    }

    // Start is called before the first frame update
    void Start()
    {
        //���̃X�N���v�g���A�^�b�`���Ă���Rigidbody���擾
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if(rb)
        {
            //���ɓ������Ă����瓮��
            rb.MovePosition(rb.position + WindForce * Time.fixedDeltaTime);
        }
    }
}
