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
        //Ä¶‚ªI‚í‚Á‚½‚çíœ
        if (particle.isStopped)
        {
            Destroy(gameObject);
        }
    }
}
