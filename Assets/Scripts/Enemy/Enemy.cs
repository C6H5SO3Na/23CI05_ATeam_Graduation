using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    //�X�e�[�^�X�֌W-----------------------------------------------------------------
    //�\����
    /// <summary>
    /// �G���ʂ̃X�e�[�^�X
    /// </summary>
    struct Status
    {
        int healthPoint;    // �̗�
        int attackPower;    // �U����
    }
}