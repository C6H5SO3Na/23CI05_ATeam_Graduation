using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Break : MonoBehaviour
{
    //�v���C���[�A�������͓G�ɂԂ������ꍇ����
    void OnCollisionEnter(Collision collision)
    {
        Destroy(this.gameObject);
    }
}
