using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassInstance : MonoBehaviour
{
    [SerializeField]
    private GameObject gameManagerInstance; // �Q�[���}�l�[�W���[�I�u�W�F�N�g�̃C���X�^���X(�R���|�[�l���g�̏��擾�p)

    /// <summary>
    /// ���̃N���X�ɃC���X�^���X��n��
    /// </summary>
    /// <param name="p1"> �v���C���[1�I�u�W�F�N�g�̃C���X�^���X </param>
    /// <param name="p2"> �v���C���[2�I�u�W�F�N�g�̃C���X�^���X </param>
    /// <param name="e"> �G�I�u�W�F�N�g�̃C���X�^���X </param>
    /// <param name="g"> �S�[���I�u�W�F�N�g�̃C���X�^���X </param>
    public void PassInstanceToOtherClass(GameObject p1, GameObject p2, GameObject e, GameObject g)
    {
        ////�G�ɓn��
        //if(p1 && p2 && e && gameManagerInstance)
        //{
        //    //Enemy�N���X�̎擾
        //    Enemy enemy = e.GetComponent<Enemy>();

        //    //�Q�[���}�l�[�W���[�̓G�����S���������󂯎��֐������������N���X���Z�b�g
        //    enemy.SetDependent(gameManagerInstance.GetComponent<IReceiveDeathInformation>(), p1.transform, p2.transform);
        //}
        
        //�S�[���ɓn��
        if(g && gameManagerInstance)
        {
            //StageClear�N���X�̎擾
            StageClear goal = g.GetComponent<StageClear>();

            //GameManager�N���X���擾����
            goal.SetInstance(gameManagerInstance.GetComponent<GameManager>());
        }


        //�v���C���[1�ɓn��
        if (p1 && gameManagerInstance)
        {
            //PlayerController�N���X�̎擾
            var player = p1.GetComponent<PlayerController>();

            //GameManager�N���X���擾����
            player.SetInstance(gameManagerInstance.GetComponent<GameManager>());
        }

        //�v���C���[2�ɓn��
        if (p2 && gameManagerInstance)
        {
            //PlayerController�N���X�̎擾
            var player = p2.GetComponent<PlayerController>();

            //GameManager�N���X���擾����
            player.SetInstance(gameManagerInstance.GetComponent<GameManager>());
        }
    }
}
