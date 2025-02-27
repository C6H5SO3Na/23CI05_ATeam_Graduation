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
        //再生が終わったら削除
        if (particle.isStopped)
        {
            Destroy(gameObject);
        }
    }
}
