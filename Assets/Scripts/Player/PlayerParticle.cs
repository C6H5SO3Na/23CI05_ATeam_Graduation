using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerParticle : MonoBehaviour
{
    public GameObject moveParticle;
    public GameObject jumpAndLandParticle;
    public GameObject damageParticle;

    /// <summary>
    /// �p�[�e�B�N���𐶐�
    /// </summary>
    /// <param name="particle">�p�[�e�B�N����GameObject</param>
    /// <param name="localPosition">���΍��W</param>
    /// <param name="parent">�e</param>
    public void PlayParticle(GameObject particle, Vector3 localPosition, Transform parent)
    {
        GameObject particleInstance = Instantiate(particle, transform.position, Quaternion.identity, parent);
        particleInstance.transform.localPosition = localPosition;
    }
}
