using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearParticle : MonoBehaviour
{
    void Start()
    {
        //���ɍs���悤�ɂ���
        transform.position = transform.parent.position + transform.parent.forward * 10f;
    }
}
