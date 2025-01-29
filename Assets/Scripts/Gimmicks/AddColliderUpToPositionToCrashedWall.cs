using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class AddColliderUpToPositionToCrashedWall : MonoBehaviour
{
    //-------------------------------------------------------------------------------
    // �ϐ�
    //-------------------------------------------------------------------------------
    BoxCollider boxCollider;              // �A�^�b�`����BoxCollider
    float       correctionValue = 0.5f;   // ���̃u���b�N���A�^�b�`����I�u�W�F�N�g�̑傫���̔����̒l(Ray���I�u�W�F�N�g�̑O�����΂�����)

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
    }

    // Update is called once per frame
    void Update()
    {
        //Ray�̓��������ʒu�܂ł̒�����BoxCollider�𐶐�����
        AddBoxCollider();
    }

    /// <summary>
    /// Ray�̓��������ʒu�܂ł̒�����BoxCollider�𐶐�����
    /// </summary>
    void AddBoxCollider()
    {
        //�ϐ��錾
        Vector3 startPoint = transform.position + transform.forward * correctionValue; // Ray�̔��˒n�_
        Ray ray = new Ray(startPoint, transform.forward);                   // ��΂�Ray
        float maxDistance = 30f;                                            // Ray�̒���
        float distanceToCollisionPoint = 0f;                                // �Փ˓_�܂ł̋��� 

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

            //�A�^�b�`�����R���C�_�[�̃T�C�Y��ύX����
            boxCollider.size = new Vector3(1, 1, boxColliderSize_Z);
            boxCollider.center = new Vector3(0, 0, boxColliderSize_Z / 2);
        }
    }
}
