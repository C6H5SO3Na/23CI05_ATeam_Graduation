using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerParticle : MonoBehaviour
{
    public GameObject moveParticle;
    public GameObject jumpAndLandParticle;
    public GameObject damageParticle;

    /// <summary>
    /// パーティクルを生成
    /// </summary>
    /// <param name="particle">パーティクルのGameObject</param>
    /// <param name="localPosition">相対座標</param>
    /// <param name="parent">親</param>
    public void PlayParticle(GameObject particle, Vector3 localPosition, Transform parent)
    {
        GameObject particleInstance = Instantiate(particle, transform.position, Quaternion.identity, parent);
        particleInstance.transform.localPosition = localPosition;
    }
}
