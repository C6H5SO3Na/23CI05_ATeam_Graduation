using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Break : MonoBehaviour
{
    //�v���C���[�A�������͓G�ɂԂ������ꍇ����
    void OnCollisionEnter(Collision collision)
    {
        //if(collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Enemy"))
        if(collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(this.gameObject);
        }
    }
}
