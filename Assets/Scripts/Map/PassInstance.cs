using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
    }

    /// <summary>
    /// �N�������M�~�b�N�̃C���X�^���X���M�~�b�N���N��������I�u�W�F�N�g�ɓn��
    /// </summary>
    /// <param name="startUpInstances"> �M�~�b�N���N��������I�u�W�F�N�g�̃C���X�^���X </param>
    /// <param name="startGimmickInstances"> �N��������M�~�b�N�̃C���X�^���X </param>
    public void PassInstanceStartGimmick(List<GameObject> startUpInstances, List<GameObject> startGimmickInstances)
    {
        for(int i = 0; i < startUpInstances.Count; ++i)
        {
            //�M�~�b�N�̃C���X�^���X���󂯎�鏈�������邩�m�F
            ISetGimmickInstance instanceReceiveGimmick = startUpInstances[i].GetComponent<ISetGimmickInstance>();
            if(instanceReceiveGimmick != null)
            {
                //�N�������M�~�b�N�����邩�m�F
                if(i < startGimmickInstances.Count)
                {
                    //�N��������M�~�b�N��ݒ肷��
                    instanceReceiveGimmick.SetGimmickInstance(startGimmickInstances[i]);
                }
            }
        }
    }
}
