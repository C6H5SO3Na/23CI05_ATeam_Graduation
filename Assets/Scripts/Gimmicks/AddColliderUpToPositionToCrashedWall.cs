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

        //BoxCollider���A�^�b�`����
        boxCollider = gameObject.AddComponent<BoxCollider>();
        
        if(boxCollider)
        {
            //�R���C�_�[��Trigger�ɂ���
            boxCollider.isTrigger = true;

            //�A�^�b�`�����R���C�_�[�̃T�C�Y��ύX����
            boxCollider.size = new Vector3(1, 1, distanceToCollisionPoint);
            boxCollider.center = new Vector3(0, 0, distanceToCollisionPoint / 2);

            //���̃X�N���v�g���A�^�b�`�����I�u�W�F�N�g��z�����������ʂɂȂ�ꍇ
            //if (transform.rotation.eulerAngles.y == 0 || transform.rotation.eulerAngles.y == 180)
            //{
            //    boxCollider.size = new Vector3(1, 1, distanceToCollisionPoint);
            //    boxCollider.center = new Vector3(0, 0, distanceToCollisionPoint / 2);
            //}
            ////���̃X�N���v�g���A�^�b�`�����I�u�W�F�N�g��x�����������ʂɂȂ�ꍇ
            //else if (transform.rotation.eulerAngles.y == 90 || transform.rotation.eulerAngles.y == 270)
            //{
            //    boxCollider.size = new Vector3(distanceToCollisionPoint, 1, 1);
            //    boxCollider.center = new Vector3(distanceToCollisionPoint / 2, 0, 0);
            //}
        }
    }
}
