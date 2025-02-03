using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class AddColliderUpToPositionToCrashedWall : MonoBehaviour
{
    //-------------------------------------------------------------------------------
    // �ϐ�
    //-------------------------------------------------------------------------------
    BoxCollider boxCollider;                // �A�^�b�`����BoxCollider
    float       correctionValue = 0.49f;    // ���̃u���b�N���A�^�b�`����I�u�W�F�N�g�̑傫���̔����̒l-0.01(�אڂ����I�u�W�F�N�g��Ray�����蔲����o�O�A���̃X�N���v�g���A�^�b�`���Ă���I�u�W�F�N�g��Ray��������o�O���Ȃ�������)
    [SerializeField]
    bool        shouldAddOntTheTop = false; // Collider���I�u�W�F�N�g�̏�ɒǉ����邩(�������ɂ͒ǉ����Ȃ��Ȃ�)
    bool        shouldStopRayCast = false;  // RayCast���~�߂�BoxCollider�̃T�C�Y�ύX����߂邩

    //-------------------------------------------------------------------------------
    // �֐�
    //-------------------------------------------------------------------------------
    // Start is called before the first frame update
    void Start()
    {
        //BoxCollider���A�^�b�`����
        boxCollider = gameObject.AddComponent<BoxCollider>();

        //�A�^�b�`�����R���C�_�[��Trigger�ɂ���
        boxCollider.isTrigger = true;

        //Collider����ɂ���Ƃ�
        if(shouldAddOntTheTop)
        {
            //���BoxCollider�𐶐�����
            AddBoxCollider_Top();
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Collider���I�u�W�F�N�g�̉��ɒǉ������Ƃ�
        if(!shouldAddOntTheTop)
        {
            //�R���C�_�[�̃T�C�Y��Ray���������������܂ł̃T�C�Y�ɕύX����Ƃ�
            if(!shouldStopRayCast)
            {
                //Ray�̓��������ʒu�܂ł̒�����BoxCollider�𐶐�����
                AddBoxCollider_Horizontal();
            }
        }
    }

    /// <summary>
    /// ��������Ray���΂��A���������ʒu�܂ł̒�����BoxCollider�𐶐�����
    /// </summary>
    void AddBoxCollider_Horizontal()
    {
        //�ϐ��錾
        Vector3 startPoint = transform.position + transform.forward * correctionValue;  // Ray�̔��˒n�_
        Ray ray = new Ray(startPoint, transform.forward);                               // ��΂�Ray
        float maxDistance = 30f;                                                        // Ray�̒���
        float distanceToCollisionPoint = 0f;                                            // �Փ˓_�܂ł̋��� 

        //Ray���΂�
        if (Physics.Raycast(ray, out RaycastHit hit, maxDistance))
        {
            //�Փ˓_�܂ł̋������v�Z���擾����
            distanceToCollisionPoint = Vector3.Distance(startPoint, hit.point);
        }
        else 
        {
            distanceToCollisionPoint = maxDistance;
        }
        
        //�A�^�b�`����Ă���R���C�_�[�̐ݒ������
        if(boxCollider)
        {
            //Ray�ɏՓ˂����I�u�W�F�N�g��BoxCollider(Trigger)�ɐG���悤�ɂ���
            float boxColliderSize_Z = distanceToCollisionPoint + correctionValue * 2f;

            //�A�^�b�`�����R���C�_�[�̃T�C�Y�A�����̈ʒu��ύX����
            boxCollider.size = new Vector3(1, 1, boxColliderSize_Z);
            boxCollider.center = new Vector3(0, 0, boxColliderSize_Z / 2);
        }
    }

    /// <summary>
    /// �������BoxCollider�𐶐�����
    /// </summary>
    void AddBoxCollider_Top()
    {
        //�A�^�b�`�����R���C�_�[�̃T�C�Y�A�����̈ʒu��ݒ�
        Vector3 boxColliderSize = new Vector3(0.95f, 0.1f, 0.95f);
        float boxColliderCenter_Y = 0.55f;

        if(boxCollider)
        {
            //�A�^�b�`�����R���C�_�[�̃T�C�Y�A�����̈ʒu��ύX����
            boxCollider.size = boxColliderSize;
            boxCollider.center = new Vector3(0, boxColliderCenter_Y, 0);
        }
    }

    /// <summary>
    /// �A�^�b�`�����R���C�_�[�ɏՓ˔���������Ȃ��悤�ɂ���
    /// </summary>
    public void MakeAddColliderSizeToZero()
    {
        shouldStopRayCast = true;

        //�A�^�b�`�����R���C�_�[�̃T�C�Y���O�ɂ���
        if (boxCollider)
        {
            //�A�^�b�`�����R���C�_�[�̃T�C�Y�A�����̈ʒu��ύX����
            boxCollider.size = Vector3.zero;
            boxCollider.center = Vector3.zero;
        }
    }

    /// <summary>
    /// ������x�A�^�b�`�����R���C�_�[�ɏՓ˔����������悤�ɂ���
    /// </summary>
    public void RestartRaycast()
    {
        shouldStopRayCast = false;
    }
}
