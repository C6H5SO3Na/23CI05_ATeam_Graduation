using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BootObjectBase : MonoBehaviour
{
    //�ϐ�
    public int id { get; private set; }   // �C���X�^���X���Ɏ���id

    //�֐�
    /// <summary>
    /// id�̐ݒ�
    /// </summary>
    /// <param name="id"> �C���X�^���X���Ɏ���id </param>
    public void SetID(int id)
    {
        this.id = id;
    }
}
