using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassInstance : MonoBehaviour
{
    [SerializeField]
    private GameObject GameManagerInstance; // �Q�[���}�l�[�W���[�I�u�W�F�N�g�̃C���X�^���X(�R���|�[�l���g�̏��擾�p)

    /// <summary>
    /// ���̃N���X�ɃC���X�^���X��n��
    /// </summary>
    /// <param name="p1"> �v���C���[1�I�u�W�F�N�g�̃C���X�^���X </param>
    /// <param name="p2"> �v���C���[2�I�u�W�F�N�g�̃C���X�^���X </param>
    /// <param name="e"> �G�I�u�W�F�N�g�̃C���X�^���X </param>
    void PassInstanceToOtherClass(GameObject p1, GameObject p2, GameObject e)
    {
        //���G�ɓn��
        //Enemy�N���X�̃C���X�^���X�擾
        Enemy enemy = e.GetComponent<Enemy>();

        //�Q�[���}�l�[�W���[�̓G�����S���������󂯎��֐������������N���X���Z�b�g
        enemy.SetDependent(GameManagerInstance.GetComponent<IReceiveDeathInformation>(), p1.transform, p2.transform);
    }
}
