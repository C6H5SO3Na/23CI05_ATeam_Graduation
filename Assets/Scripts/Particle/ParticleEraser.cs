using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleEraser : MonoBehaviour
{
    ParticleSystem particle;
    void Start()
    {
        particle = GetComponent<ParticleSystem>();
    }

    void Update()
    {
        //�Đ����I�������폜
        if (particle.isStopped)
        {
            Destroy(gameObject);
        }
    }
}
