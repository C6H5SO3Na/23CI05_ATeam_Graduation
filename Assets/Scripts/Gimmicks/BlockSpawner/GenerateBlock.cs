using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GenerateBlock : GimmickBase, IStartedOperation
{
    [SerializeField]
    private GameObject generatingBlockPrefab;                           // ��������u���b�N
    private List<GameObject> generatedBlocks = new List<GameObject>();  // ���������u���b�N�̃��X�g
    private int maxObjects;                                             // �����ł���u���b�N�̐�

    void Start()
    {
        maxObjects = 1;
    }

    //�u���b�N��������΃u���b�N�𐶐�����
    public void ProcessWhenPressed()
    {
        if (generatingBlockPrefab)
        {
            //���������u���b�N�����ł��Ă����烊�X�g����폜���Ă���
            generatedBlocks.RemoveAll(block => block == null);

            //�u���b�N����������Ă��Ȃ������琶������
            if(generatedBlocks.Count < maxObjects)
            {
                //�����ʒu�����߂�
                Vector3 position = transform.position;

                //�u���b�N�̐���
                GameObject newBlock = Instantiate(generatingBlockPrefab, position, Quaternion.identity);

                //���������u���b�N�����X�g�ɓo�^
                generatedBlocks.Add(newBlock);
            }
            else
            {
                Debug.LogWarning("���ɐ�������Ă���");
            }
        }
        else
        {
            Debug.LogWarning("��������I�u�W�F�N�g���ݒ肳��Ă��Ȃ�");
        }
    }

    public void ProcessWhenStopped()
    {
        //�����Ȃ�
    }

    //void Update()
    //{
    //    Debug.Log("maxObjects:" + maxObjects);
    //}
}