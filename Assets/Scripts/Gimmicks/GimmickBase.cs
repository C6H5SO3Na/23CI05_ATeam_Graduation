using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GimmickBase : MonoBehaviour
{
    //�ϐ�
    public int id { get; private set; }         // �C���X�^���X���Ɏ���id
    private bool isRunning = false;             // ���łɃM�~�b�N���N�����Ă��邩(�������ŃM�~�b�N����x���������ꍇ�Ɏg�p)

    //�֐�
    /// <summary>
    /// id�̐ݒ�
    /// </summary>
    /// <param name="id"> �C���X�^���X���Ɏ���id </param>
    public void SetID(int id)
    {
        this.id = id;
    }

    /// <summary>
    /// ��x�������ŃM�~�b�N���N������Ă��邩���肷��(������g���Ƃ��A�M�~�b�N�̋N������߂��Ƃ��̏�����MakeToLaunchable()������)
    /// </summary>
    /// <returns></returns>
    protected bool HasRunningOnce()
    {
        //�����ꂽ�u�Ԃ̓M�~�b�N���N��������
        if(!isRunning)
        {
            isRunning = true;
            return true;
        }

        //���łɉ������ゾ�����牟���̂���߂�܂ŃM�~�b�N���N�����Ȃ��悤�ɂ���
        return false;
    }

    /// <summary>
    /// �������ň�x�����N������M�~�b�N���ĂыN���ł���悤�ɂ���
    /// </summary>
    protected void MakeToLaunchable()
    {
        //�M�~�b�N���N������Ă��邩�m�F����
        if (isRunning)
        {
            isRunning = false;
        }
    }
}
