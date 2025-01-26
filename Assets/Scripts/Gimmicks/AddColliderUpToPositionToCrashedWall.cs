using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class AddColliderUpToPositionToCrashedWall : MonoBehaviour
{
    //-------------------------------------------------------------------------------
    // �ϐ�
    //-------------------------------------------------------------------------------
    BoxCollider boxCollider;    // �A�^�b�`����BoxCollider

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
        Transform startPoint = transform;                           // Ray�̔��˒n�_
        Ray ray = new Ray(startPoint.position, startPoint.forward); // ��΂�Ray
        float maxDistance = 30f;                                    // Ray�̒���
        float distanceToCollisionPoint = 0f;                        // �Փ˓_�܂ł̋��� 

        //Ray���΂�
        if (Physics.Raycast(ray, out RaycastHit hit, maxDistance))
        {
            //�Փ˓_�܂ł̋������v�Z���擾����
            distanceToCollisionPoint = Vector3.Distance(startPoint.position, hit.point);
        }
        else 
        {
            distanceToCollisionPoint = maxDistance;
        }
        
        //�A�^�b�`����Ă���R���C�_�[�̐ݒ������
        if(boxCollider)
        {
            //�A�^�b�`�����R���C�_�[�̃T�C�Y��ύX����
            boxCollider.size = new Vector3(1, 1, distanceToCollisionPoint);
            boxCollider.center = new Vector3(0, 0, distanceToCollisionPoint / 2);
        }
    }
}
