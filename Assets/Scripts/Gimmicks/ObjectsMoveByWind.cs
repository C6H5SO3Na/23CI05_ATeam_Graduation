using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsMoveByWind : MonoBehaviour
{
    Rigidbody rb;                // ���̃X�N���v�g���A�^�b�`���Ă���I�u�W�F�N�g��Rigidbody
    public Vector3 windForce;    // �󂯂Ă��镗�̋���

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
            rb.AddForce(windForce);
        }
    }
}
